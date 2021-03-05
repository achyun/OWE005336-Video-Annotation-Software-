using LabellingDB;
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

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fProcessTrackerImages : Form
    {
        private PaintLocker _PaintDataInProgress = new PaintLocker();
        private string _CurrentFilePath;
        private Bitmap _CurrentImage;
        List<ROIObject> _CurrentROIs = new List<ROIObject>();

        public fProcessTrackerImages(string dirpath)
        {
            InitializeComponent();

            string[] extensions = { ".png", ".jpg" };
            List<string> filePaths = Directory
                .GetFiles(dirpath)
                .Where(file => extensions.Any(file.ToLower().EndsWith))
                .ToList();

            foreach (string s in filePaths)
            {
                _PaintDataInProgress.Lock();
                dgvImages.Rows.Add(new object[] { null, s });
                _PaintDataInProgress.Unlock();
            }

            dgvImages_SelectionChanged(null, null);

            for (int i = 0; i < filePaths.Count; i++)
            {
                LoadThumbnail(i);
            }
        }
        private async void LoadThumbnail(int row_index)
        {
            var cell = dgvImages.Rows[row_index].Cells[0];
            string filepath = (string)dgvImages.Rows[row_index].Cells[1].Value;

            Image img = await Task.Run(() =>
            {
                Image thumbnail = LoadImage(filepath).GetThumbnailImage(50, 50, null, IntPtr.Zero);
                return thumbnail;
            });

            if (img != null)
            {
                _PaintDataInProgress.Lock();
                cell.Value = img;
                _PaintDataInProgress.Unlock();
            }
        }

        private void dgvImages_SelectionChanged(object sender, EventArgs e)
        {
            if (!_PaintDataInProgress.Locked)
            {
                if (dgvImages.SelectedRows.Count > 0)
                {
                    _CurrentFilePath = (string)dgvImages.SelectedRows[0].Cells[1].Value;
                    _CurrentImage = (Bitmap)LoadImage(_CurrentFilePath);
                    string txtfilepath = Path.ChangeExtension(_CurrentFilePath, ".txt");

                    _CurrentROIs.Clear();

                    if (File.Exists(txtfilepath))
                    {
                        StreamReader fs = new StreamReader(txtfilepath);
                        string line = fs.ReadLine();
                        while (line != null)
                        {
                            string[] str_values = line.Split(' ');
                            if (str_values.Length >= 5)
                            {
                                float[] values = new float[5];
                                float val;
                                bool success = true;

                                for (int i=0; i < 5; i++)
                                {
                                    if (float.TryParse(str_values[i], out val))
                                    {
                                        values[i] = val;
                                    }
                                    else
                                    {
                                        success = false;
                                        break;
                                    }
                                }

                                if (success)
                                {
                                    string description = (str_values.Length >= 6) ? str_values[5]: "";
                                    ROIObject roi = new ROIObject(new RectangleF(values[1], values[2], values[3], values[4]), 1, description);
                                    _CurrentROIs.Add(roi);
                                }
                            }
                            line = fs.ReadLine();
                        }
                    }

                    roiSelector.LinkToLabelledImage(_CurrentROIs, _CurrentImage);
                }
                else
                {
                    roiSelector.LinkToLabelledImage(new List<ROIObject>(), null);
                }
            }
        }

        private Image LoadImage(string filepath)
        {
            byte[] b = File.ReadAllBytes(filepath);
            MemoryStream ms = new MemoryStream(b);

            return Image.FromStream(ms);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string newFileName = (string)dgvImages.SelectedRows[0].Cells[1].Value;
            newFileName = Path.ChangeExtension(newFileName, ".png");

            LabelledImage temp = Program.ImageDatabase.Images_SearchByFileName(Path.Combine("%", newFileName) + "%");

            if (temp == null)
            {
                LabelledImage lImg = await Program.ImageDatabase.Images_Add(_CurrentImage, newFileName, SensorTypeEnum.Daylight);

                if (lImg != null)
                {
                    foreach (var roi in _CurrentROIs)
                    {
                        RectangleF rectf = roi.GetROI();
                        Rectangle rect = new Rectangle((int)rectf.X, (int)rectf.Y, (int)rectf.Width, (int)rectf.Height);
                        Program.ImageDatabase.BBoxLabels_AddLabel(lImg.ID, rect, -1);
                    }
                }
            }
            else
            {
                MessageBox.Show("Image already exists in database.");
            }
        }
    }
}
