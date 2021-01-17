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
using LabellingDB;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fReviewTestResults : Form
    {
        string _DirPath;

        public fReviewTestResults(string dirPath)
        {
            InitializeComponent();

            _DirPath = dirPath;
        }

        private void LoadTestResults(string dirPath, ToolStripStatusLabel statusLabel)
        {
            string[] files = Directory.GetFiles(Path.Combine(dirPath, "Labels"), "*.txt");
            statusLabel.Text = "Loading 0/" + files.Length;

            for (int i = 0; i < files.Length; i++)
            {
                LabelledImage lImg = Program.ImageDatabase.Images_SearchByFileName("%\\" + Path.GetFileNameWithoutExtension(files[i]) + ".%");
                if (lImg != null)
                {
                    List<string> lines = File.ReadAllLines(files[i]).ToList();

                    foreach (string l in lines)
                    {
                        string[] fields = l.Split(' ');
                        float x, y, width, height;
                        if (float.TryParse(fields[1], out x) && float.TryParse(fields[2], out y) && float.TryParse(fields[3], out width) && float.TryParse(fields[4], out height))
                        {
                            x *= lImg.ImageSize.Width;
                            y *= lImg.ImageSize.Height;
                            width *= lImg.ImageSize.Width;
                            height *= lImg.ImageSize.Height;
                            x -= width / 2;
                            y -= height / 2;
                            RectangleF rect = new RectangleF(x, y, width, height);
                            LabelledROI roi = new LabelledROI(0, 0, Rectangle.Round(rect));
                            roi.LabelName = fields[0]; // this is the class ID
                            if (fields.Length == 6)
                            {
                                float conf;
                                if (float.TryParse(fields[5], out conf)) { roi.LabelName += ":" + conf.ToString("0.000"); }
                            }
                            lImg.LabelledROIs.Add(roi);
                        }
                    }

                    ListViewItem newThumb = new ListViewItem();

                    //imgFrames.Images.Add(img);

                    newThumb.Text = lImg.Filepath;
                    newThumb.ImageIndex = imgFrames.Images.Count - 1;
                    newThumb.Tag = lImg;

                    this.BeginInvoke((Action)(() =>
                    {
                        float minConf = GetMinConf(lImg.LabelledROIs);
                        float maxConf = GetMaxConf(lImg.LabelledROIs);
                        dgvImages.Rows.Add(new object[] { lImg.LabelledROIs.Count, maxConf, minConf, lImg.Filepath });
                        dgvImages.Rows[dgvImages.Rows.Count - 1].Tag = lImg;
                        statusLabel.Text = "Loading " + i.ToString() + "/" + files.Length;
                    }));
                    
                }
            }
        }

        private float GetMaxConf(List<LabelledROI> lrois)
        {
            float max = 0;
            foreach (var lr in lrois)
            {
                string[] s = lr.LabelName.Split(':');
                if (s.Length == 2)
                {
                    float conf;
                    float.TryParse(s[1], out conf);
                    if (conf > max) { max = conf; }
                }
                
            }

            return max;
        }

        private float GetMinConf(List<LabelledROI> lrois)
        {
            float min = float.MaxValue;
            foreach (var lr in lrois)
            {
                string[] s = lr.LabelName.Split(':');
                if (s.Length == 2)
                {
                    float conf;
                    float.TryParse(s[1], out conf);
                    if (conf < min) { min = conf; }
                }

            }

            if (min == float.MaxValue) { min = 0; }
            return min;
        }

        private async void fReviewTestResults_Load(object sender, EventArgs e)
        {
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("Loading 0/0");
            stsStatus.Items.Add(statusLabel);

            await Task.Run(() => LoadTestResults(_DirPath, statusLabel));

            statusLabel.Text = "Load complete...";
            await Task.Delay(750);

            stsStatus.Items.Remove(statusLabel);
        }

        private async void dgvImages_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvImages.SelectedRows.Count > 0)
            {
                LabelledImage lImg = (LabelledImage)dgvImages.SelectedRows[0].Tag;
                Bitmap frame = (Bitmap)(await Program.ImageDatabase.Images_LoadImageFile(lImg));

                List<ROIObject> rois = new List<ROIObject>();

                foreach (LabelledROI lroi in lImg.LabelledROIs)
                {
                    if (lroi.LabelName.Contains(':'))
                    {
                        string[] s = lroi.LabelName.Split(':');
                        decimal conf = 0;
                        decimal.TryParse(s[1], out conf);
                        if (conf >= nudConfidence.Value)
                        {
                            rois.Add(new ROIObject(lroi.ROI, 1, lroi.LabelName));
                        }
                    }
                    else
                    {
                        
                    }
                    
                }

                roiSelector1.LinkToLabelledImage(rois, frame);
            }
        }
    }
}
