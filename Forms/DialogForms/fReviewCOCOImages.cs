using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using LabellingDB;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fReviewCOCOImages : Form
    {
        List<string> _COCOFilePaths = new List<string>();
        int _CurrentIndex = 0;
        Bitmap _CurrentImage = null;
        LabelledImage _CurrentLabelledImage = null;
        COCO_JSON _CurrentJSON = null;

        List<DictEntry> _Dictionary = new List<DictEntry>();

        public fReviewCOCOImages(string[] filePaths)
        {
            InitializeComponent();

            _COCOFilePaths = filePaths.ToList();

            lblProgress.Text = (_CurrentIndex + 1).ToString() + "/" + _COCOFilePaths.Count.ToString();

            LoadFile(_COCOFilePaths[_CurrentIndex]);
        }

        private void LoadFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonString = File.ReadAllText(filePath);
                    _CurrentJSON = JsonConvert.DeserializeObject<COCO_JSON>(jsonString);

                    if (_CurrentJSON?.image[0]?.file_name != null)
                    {
                        string imgPath = Path.Combine(Path.GetDirectoryName(filePath), _CurrentJSON.image[0].file_name);

                        if (File.Exists(imgPath))
                        {
                            _CurrentImage = new Bitmap(imgPath);
                            _CurrentLabelledImage = new LabelledImage(new Size(_CurrentJSON.image[0].width, _CurrentJSON.image[0].height));
                            _CurrentLabelledImage.Tags = "#coco";
                            
                            for (int i = 0; i < _CurrentJSON.annotation.Length; i++)
                            {
                                
                                LabelledROI lroi = new LabelledROI(-1, -1, ConvertJSONBBoxArrayToRectangle(_CurrentJSON.annotation[i].bbox));
                                int dictEntryIndex = CheckDictionaryForCOCOCategory(_CurrentJSON.annotation[i].category_id);
                                if (dictEntryIndex > -1)
                                {
                                    lroi.LabelID = _Dictionary[dictEntryIndex].OWEID;
                                    lroi.LabelName = _Dictionary[dictEntryIndex].Name;
                                }
                                _CurrentLabelledImage.LabelledROIs.Add(lroi);
                            }

                            List<ROIObject> rois = new List<ROIObject>();

                            foreach (LabelledROI lroi in _CurrentLabelledImage.LabelledROIs)
                            {
                                rois.Add(new ROIObject(lroi.ROI, 1, lroi.LabelName));
                            }

                            roiSelector1.LinkToLabelledImage(rois, _CurrentImage);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        private Rectangle ConvertJSONBBoxArrayToRectangle(float[] r)
        {
            return new Rectangle((int)r[0], (int)r[1], (int)r[2], (int)r[3]);
        }

        private int CheckDictionaryForCOCOCategory(int cocoID)
        {
            int index = -1;

            for (int i = 0; i < _Dictionary.Count; i++)
            {
                if (_Dictionary[i].CocoID == cocoID)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_CurrentIndex > 0) { _CurrentIndex--; }
            lblProgress.Text = (_CurrentIndex + 1).ToString() + "/" + _COCOFilePaths.Count.ToString();
            LoadFile(_COCOFilePaths[_CurrentIndex]);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            
            if (_CurrentIndex < (_COCOFilePaths.Count - 1)) { _CurrentIndex++; }
            lblProgress.Text = (_CurrentIndex + 1).ToString() + "/" + _COCOFilePaths.Count.ToString();
            LoadFile(_COCOFilePaths[_CurrentIndex]);
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            File.Delete(_COCOFilePaths[_CurrentIndex]);
            _COCOFilePaths.RemoveAt(_CurrentIndex);
            _CurrentIndex = Math.Min(_CurrentIndex, _COCOFilePaths.Count - 1);
            lblProgress.Text = (_CurrentIndex + 1).ToString() + "/" + _COCOFilePaths.Count.ToString();
            LoadFile(_COCOFilePaths[_CurrentIndex]);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string newFileName = Path.GetFileNameWithoutExtension(_CurrentJSON.image[0].file_name) + ".png";
            LabelledImage temp = Program.ImageDatabase.Images_SearchByFileName(Path.Combine("%", newFileName) + "%");

            if (temp == null)
            {
                LabelledImage lImg = await Program.ImageDatabase.Images_Add(_CurrentImage, newFileName, SensorTypeEnum.Daylight, FindMostCommonLabelID(_CurrentLabelledImage), "#coco");
                if (lImg != null)
                {
                    foreach (var lroi in _CurrentLabelledImage.LabelledROIs)
                    {
                        lroi.ImageID = lImg.ID;
                        Program.ImageDatabase.BBoxLabels_AddLabel(lImg.ID, lroi.ROI, lroi.LabelID);
                    }
                }

                File.Delete(_COCOFilePaths[_CurrentIndex]);
                _COCOFilePaths.RemoveAt(_CurrentIndex);
                _CurrentIndex = Math.Min(_CurrentIndex, _COCOFilePaths.Count - 1);
                lblProgress.Text = (_CurrentIndex + 1).ToString() + "/" + _COCOFilePaths.Count.ToString();
                LoadFile(_COCOFilePaths[_CurrentIndex]);
            }
            else
            {
                MessageBox.Show("Image already exists in database.");
            }
        }

        private void roiSelector1_DoubleClick(object sender, EventArgs e)
        {
            if (roiSelector1.SelectedROIIndex >= 0)
            {
                fLabelSelector labelSelector = new fLabelSelector();
                int index = roiSelector1.SelectedROIIndex;
                LabelledROI lroi = _CurrentLabelledImage.LabelledROIs[index];
                if (labelSelector.ShowDialog() == DialogResult.OK)
                {
                    if (lroi.LabelID == -1)
                    {
                        _Dictionary.Add(new DictEntry(_CurrentJSON.annotation[index].category_id, labelSelector.SelectedLabel.ID, labelSelector.SelectedLabel.Name));
                    }

                    lroi.LabelID = labelSelector.SelectedLabel.ID;
                    lroi.LabelName = labelSelector.SelectedLabel.Name;

                    roiSelector1.UpdateROIName(index, lroi.LabelName);
                }
            }
        }

        private int FindMostCommonLabelID(LabelledImage lImg)
        {
            int id = -1;
            List<int[]> frequency = new List<int[]>();

            foreach (LabelledROI lroi in lImg.LabelledROIs)
            {
                if (lroi.LabelID >= 0)
                {
                    bool found = false;

                    foreach (var f in frequency)
                    {
                        if (f[0] == lroi.LabelID)
                        {
                            f[1]++;
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        int[] f = { lroi.LabelID, 1 };
                        frequency.Add(f);
                    }
                }
            }

            int maxCount = 0;
            foreach (var f in frequency)
            {
                if (f[1] > maxCount)
                {
                    id = f[0];
                }
            }

            return id;
        }

        private class DictEntry
        {
            public int CocoID;
            public int OWEID;
            public string Name;

            public DictEntry(int cocoID, int oweID, string name)
            {
                CocoID = cocoID;
                OWEID = oweID;
                Name = name;
            }
        }
    }



    public class COCO_JSON
    {
        public class COCOImageInfo
        {
            public int id;
            public int width;
            public int height;
            public string file_name;
            public int license;
            public string flickr_url;
            public string coco_url;
            public string date_captured;
        }

        public class COCOBBoxInfo
        {
            public int id;
            public int image_id;
            public int category_id;
            public object segmentation;
            public float[] bbox;
            public float area;
            public int iscrowd;
        }

        public class COCOBBox
        {
            public int x;
            public int y;
            public int width;
            public int height;
        }

        public COCOImageInfo[] image;
        public COCOBBoxInfo[] annotation;
    }

}
