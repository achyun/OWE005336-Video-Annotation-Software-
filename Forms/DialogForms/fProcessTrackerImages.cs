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
using System.Text.RegularExpressions;
using Accord;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fProcessTrackerImages : Form
    {
        private PaintLocker _PaintDataInProgress = new PaintLocker();
        private string _CurrentFilePath;
        private Bitmap _CurrentImage;
        List<ROIObject> _CurrentROIs = new List<ROIObject>();

        public Dictionary<int, string> Detector_LabelMap = new Dictionary<int, string>() { { 0, "ROTARY_WING" }, { 1, "FIXED_WING" }, { 2, "BIRD" }, { 3, "TREE" } };
        public int LabelID { get; set; } = -1;
        public string Tags { get; set; } = "";
        public SensorTypeEnum SensorType { get; set; } = SensorTypeEnum.Daylight;

        public int AddedCount = 0;

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
                dgvImages.Rows.Add(new object[] { null, Path.GetFileName(s), s, "" });
                _PaintDataInProgress.Unlock();
            }

            dgvImages_SelectionChanged(null, null);

            for (int i = 0; i < filePaths.Count; i++)
            {
                LoadThumbnail(i);
            }

            cmbSensorType.DataSource = Enum.GetValues(typeof(SensorTypeEnum));
            cmbSensorType.SelectedItem = SensorType;
            cmbSensorType.SelectedIndexChanged += CmbSensorType_SelectedIndexChanged;
            tgbTags.TagsChanged += TgbTags_TagsChanged;
        }

        private void TgbTags_TagsChanged(TagBox sender, EventArgs e)
        {
            Tags = sender.ToString();
        }

        private void CmbSensorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SensorType = (SensorTypeEnum)cmbSensorType.SelectedItem;
        }

        private async void LoadThumbnail(int row_index)
        {
            var cell = dgvImages.Rows[row_index].Cells[0];
            string filepath = (string)dgvImages.Rows[row_index].Cells[2].Value;

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
                    _CurrentFilePath = (string)dgvImages.SelectedRows[0].Cells[2].Value;
                    _CurrentImage = (Bitmap)LoadImage(_CurrentFilePath);
                    string txtfilepath = Path.ChangeExtension(_CurrentFilePath, ".txt");

                    _CurrentROIs.Clear();

                    if (File.Exists(txtfilepath))
                    {
                        dgvImages.SelectedRows[0].Cells[3].Value = txtfilepath;
                        var lines = File.ReadAllLines(txtfilepath);
                        foreach(var line in lines)
                        {
                            string[] str_values = line.Split(' ');

                            try
                            {
                                if (str_values.Length < 5)
                                    continue;

                                LabelNode label = new LabelNode(-1, "<Unknown>", -1, "");
                                var labelTextID = str_values[0].ToUpperInvariant();
                                var topLeft_x = float.Parse(str_values[1]);
                                var topLeft_y = float.Parse(str_values[2]);
                                var width = float.Parse(str_values[3]);
                                var height = float.Parse(str_values[4]);
                                var confidence = str_values.Length >= 6 ? float.Parse(str_values[5]) : 1.0;

                                //If label is a number assume it is a detector output ID, convert to text
                                if (int.TryParse(labelTextID, out int dectectorClassID))
                                {
                                    labelTextID = Detector_LabelMap[dectectorClassID];
                                }
                                //Try and get the label info for the given text ID, otherwise fall back to our unknown label
                                label = Program.ImageDatabase.LabelTree_LoadByTextID(labelTextID) ?? label;

                                _CurrentROIs.Add(new ROIObject(new RectangleF(topLeft_x, topLeft_y, width, height), 1, label.Name) { Tag = label });
                            }
                            catch (FormatException)
                            {
                                // The line was incorrectly formatted, ignore
                                continue;
                            }
                        }
                    }

                    roiSelector.LinkToLabelledImage(_CurrentROIs, _CurrentImage);
                }
                else
                {
                    _CurrentImage = null;
                    _CurrentROIs.Clear();
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

        protected void RemoveImageFromList(int index, bool deleteFile = true)
        {
            if (deleteFile)
            {
                //Delete the image file and any associated metadata file.
                string filePath = (string)dgvImages.Rows[index].Cells[2].Value;
                if (File.Exists(filePath))
                    File.Delete(filePath);

                string metadataFilePath = (string)dgvImages.Rows[index].Cells[3].Value;
                if (!string.IsNullOrEmpty(metadataFilePath) && File.Exists(metadataFilePath))
                    File.Delete(metadataFilePath);
            }

            _PaintDataInProgress.Lock();
            dgvImages.Rows.RemoveAt(index);
            _PaintDataInProgress.Unlock();

            dgvImages_SelectionChanged(dgvImages, new EventArgs());
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = dgvImages.SelectedRows[0];
            string newFileName = (string)selectedRow.Cells[1].Value;
            newFileName = Path.ChangeExtension(newFileName, ".png");
            Tags = tgbTags.ToString();

            LabelledImage temp = Program.ImageDatabase.Images_SearchByFileName(Path.Combine("%", newFileName) + "%");
            if (temp == null)
            {
                LabelledImage lImg = await Program.ImageDatabase.Images_Add(_CurrentImage, newFileName, SensorType, LabelID, Tags);

                if (lImg != null)
                {
                    foreach (var roi in _CurrentROIs)
                    {
                        RectangleF rectf = roi.GetROI();
                        Rectangle rect = new Rectangle((int)rectf.X, (int)rectf.Y, (int)rectf.Width, (int)rectf.Height);
                        LabelNode label = roi.Tag as LabelNode;
                        Program.ImageDatabase.BBoxLabels_AddLabel(lImg.ID, rect, label?.ID ?? -1);
                    }
                }
                int selectedIndex = selectedRow.Index;
                RemoveImageFromList(selectedIndex);
                if (dgvImages.Rows.Count > selectedIndex)
                    dgvImages.Rows[selectedIndex].Selected = true;

                AddedCount += 1;
                lblAddedCount.Text = AddedCount.ToString();
            }
            else
            {
                MessageBox.Show("Image already exists in database.");
            }
        }

        private void btnSelectLabel_Click(object sender, EventArgs e)
        {
            fLabelSelector labelSelector = new fLabelSelector();

            if (labelSelector.ShowDialog() == DialogResult.OK)
            {
                if (labelSelector.SelectedLabel.ParentID > -1)
                {
                    LabelID = labelSelector.SelectedLabel.ID;
                    txtLabel.Text = labelSelector.SelectedLabel.Name;
                }
            }
        }

        private void txtLabel_DoubleClick(object sender, EventArgs e)
        {
            btnSelectLabel_Click(sender, e);
        }

        private void dgvImages_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete && dgvImages.SelectedRows.Count > 0)
            {
                int firstSelectedIndex = dgvImages.SelectedRows[0].Index;
                foreach (DataGridViewRow row in dgvImages.SelectedRows)
                {
                    RemoveImageFromList(row.Index, true);
                }
                if (dgvImages.Rows.Count > firstSelectedIndex)
                    dgvImages.Rows[firstSelectedIndex].Selected = true;
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                var rows = dgvImages.SelectedRows;
                btnSave_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dgvImages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                //Handle this event to stop the automatic behaviour of moving to the next row
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dgvImages_KeyUp(sender, new KeyEventArgs(Keys.Delete));
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
