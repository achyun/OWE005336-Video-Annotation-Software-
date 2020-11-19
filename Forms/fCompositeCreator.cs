using System;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabellingDB;
using System.IO;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fCompositeCreator : Form
    {
        private const float PI = 3.14159265359f;
        CompositeCreatorSettings _Settings = new CompositeCreatorSettings();
        Random _Rand = new Random();
        string[] _Backgrounds = null;
        string[] _Objects = null;
        Image _Image = null;
        RectangleF _ROI;

        public fCompositeCreator()
        {
            InitializeComponent();
            
            if (SetBackgroundsDir(_Settings.BackgroundsDirectory, _Settings.IncludeSubDirsBackground))
            { txtBackgroundImagesDir.Text = _Settings.BackgroundsDirectory; }

            if (SetObjectsDir(_Settings.ObjectsDirectory, _Settings.IncludeSubDirsObjects))
            { txtObjectImagesDir.Text = _Settings.ObjectsDirectory; }

            nudNumberVariations.Value = _Settings.NumberOfVariations;
            nudSizeMin.Value = (decimal)_Settings.ScaleMin;
            nudSizeMax.Value = (decimal)_Settings.ScaleMax;
            nudRotationRange.Value = (decimal)_Settings.RotationRange_deg;
            ckbMinimiseBBox.Checked = _Settings.MinimiseBBoxWithRotation;
            ckbIncludeSubDirsBackground.Checked = _Settings.IncludeSubDirsBackground;
            ckbIncludeSubDirsObjects.Checked = _Settings.IncludeSubDirsObjects;

            cmbSensorType.DataSource = Enum.GetValues(typeof(SensorTypeEnum));
            cmbSensorType.SelectedItem = SensorTypeEnum.Daylight;
        }

        private void btnBackgroundDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = _Settings.BackgroundsDirectory;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtBackgroundImagesDir.Text = folderBrowserDialog.SelectedPath;
                    SetBackgroundsDir(folderBrowserDialog.SelectedPath, ckbIncludeSubDirsBackground.Checked);
                }
            }
        }

        private void btnObjectDirectory_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.SelectedPath = _Settings.ObjectsDirectory;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtObjectImagesDir.Text = folderBrowserDialog.SelectedPath;
                    SetObjectsDir(folderBrowserDialog.SelectedPath, ckbIncludeSubDirsObjects.Checked);
                }
            }
        }

        private void btnSelectLabel_Click(object sender, EventArgs e)
        {
            fLabelSelector labelSelector = new fLabelSelector();
            if (labelSelector.ShowDialog() == DialogResult.OK)
            {
                LabelNode l = labelSelector.SelectedLabel;
                if (l.ParentID > -1)
                {
                    txtObjectLabel.Tag = l.ID;
                    txtObjectLabel.Text = l.Name;
                    btnRefresh_Click(null, null);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (_Backgrounds != null && _Objects != null)// && txtObjectLabel.Text != "")
            {
                GenerageCompositeImage(_Backgrounds[_Rand.Next(_Backgrounds.Length)], _Objects[_Rand.Next(_Objects.Length)], (float)nudSizeMin.Value, (float)nudSizeMax.Value, (float)nudRotationRange.Value);

                List<ROIObject> rois = new List<ROIObject>();
                rois.Add(new ROIObject(_ROI, 1, txtObjectLabel.Text));

                roiSelector.LinkToLabelledImage(rois, (Bitmap)_Image);
            }
        }

        private void fCompositeCreator_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Settings.NumberOfVariations = (int)nudNumberVariations.Value;
            _Settings.ScaleMin = (float)nudSizeMin.Value;
            _Settings.ScaleMax = (float)nudSizeMax.Value;
            _Settings.RotationRange_deg = (float)nudRotationRange.Value;
            _Settings.MinimiseBBoxWithRotation = ckbMinimiseBBox.Checked;
            _Settings.IncludeSubDirsBackground = ckbIncludeSubDirsBackground.Checked;
            _Settings.IncludeSubDirsObjects = ckbIncludeSubDirsObjects.Checked;

            _Settings.Save();
        }

        private async void btnSaveSingle_Click(object sender, EventArgs e)
        {
            if (_Image != null)
            {
                ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("Saving 0/1");
                stsStatus.Items.Add(statusLabel);

                await SaveCompositeImage(_Image, CreateRandomFileName(), (SensorTypeEnum)cmbSensorType.SelectedItem, tagBox.ToString(), Rectangle.Round(_ROI), (int)(txtObjectLabel.Tag));

                statusLabel.Text = "Save Complete";
                await Task.Delay(750);
                stsStatus.Items.Remove(statusLabel);
            }
            else
            {
                MessageBox.Show("No image to save");
            }
        }

        private async void btnSaveAll_Click(object sender, EventArgs e)
        {
            if (_Image != null)
            {
                for (int i = 0; i < _Objects.Length; i++)
                {
                    for (int j = 0; j < nudNumberVariations.Value; j++)
                    {
                        GenerageCompositeImage(_Backgrounds[_Rand.Next(_Backgrounds.Length)], _Objects[i], (float)nudSizeMin.Value, (float)nudSizeMax.Value, (float)nudRotationRange.Value);
                        await SaveCompositeImage(_Image, CreateRandomFileName(), (SensorTypeEnum)cmbSensorType.SelectedItem, tagBox.ToString(), Rectangle.Round(_ROI), (int)(txtObjectLabel.Tag));
                    }
                }
            }
        }

        private bool SetBackgroundsDir(string dir, bool includeSubDirs)
        {
            _Settings.BackgroundsDirectory = dir;
            _Backgrounds = null;// new string[] { };
            List<string> backgrounds = new List<string>();

            string[] temp = GetFiles(dir, includeSubDirs);

            for (int i = 0; i < temp.Length; i++)
            {
                string ext = Path.GetExtension(temp[i]);
                if (ext == ".png" || ext == ".jpg")
                {
                    backgrounds.Add(temp[i]);
                }
            }

            if (backgrounds.Count > 0) { _Backgrounds = backgrounds.ToArray(); }

            btnRefresh_Click(null, null);

            return (_Backgrounds != null);
        }

        private bool SetObjectsDir(string dir, bool includeSubDirs)
        {
            _Settings.ObjectsDirectory = dir;
            _Objects = null;// new string[] { };
            List<string> objects = new List<string>();

            string[] temp = GetFiles(dir, includeSubDirs);

            for (int i = 0; i < temp.Length; i++)
            {
                string ext = Path.GetExtension(temp[i]);
                if (ext == ".png")
                {
                    objects.Add(temp[i]);
                }
            }

            if (objects.Count > 0) { _Objects = objects.ToArray(); }

            btnRefresh_Click(null, null);

            return (_Objects != null);
        }

        private string[] GetFiles(string dirPath, bool includeSubDirs)
        {
            List<string> filePaths = new List<string>();
            string[] tempFiles = Directory.GetFiles(dirPath);

            filePaths.AddRange(tempFiles);

            if (includeSubDirs)
            {
                var tempDirs = Directory.GetDirectories(dirPath);

                foreach (string dir in tempDirs)
                {
                    filePaths.AddRange(GetFiles(dir, true));
                }
            }

            return filePaths.ToArray();
        }

        private void GenerageCompositeImage(string backgroundFileName, string objectFileName, float scaleMin, float scaleMax, float rotationRange_deg)
        {
            Bitmap background = new Bitmap(backgroundFileName);
            
            Graphics g = Graphics.FromImage(background);
            float ang_deg = (((float)_Rand.NextDouble() * 2) - 1) * rotationRange_deg;
            float ang_rad = ang_deg * PI / 180.0f;
            Bitmap obj = RotateAndResizeObjectImage(new Bitmap(objectFileName), ang_rad);
            float scale = ((float)_Rand.NextDouble()) * (scaleMax - scaleMin) + scaleMin;
            float maxWidth = background.Width * scale;
            float maxHeight = background.Height * scale;
            float objScale = Math.Min(maxWidth / obj.Width, maxHeight / obj.Height);
            SizeF scaledObjSize = new SizeF(obj.Width * objScale, obj.Height * objScale);
            float px = (background.Width - scaledObjSize.Width - 1) * ((float)_Rand.NextDouble());
            float py = (background.Height - scaledObjSize.Height - 1) * ((float)_Rand.NextDouble());

            RectangleF rect = new RectangleF(px, py, scaledObjSize.Width, scaledObjSize.Height);

            obj.SetResolution(g.DpiX, g.DpiY);
            g.TranslateTransform(px, py);
            g.ScaleTransform(objScale, objScale);
            g.DrawImage(obj, new PointF(0, 0));

            _Image = background;
            _ROI = rect;
        }

        private Task<bool> SaveCompositeImage(Image img, string fileName, SensorTypeEnum sensorType, string tags, Rectangle roi, int labelID)
        {
            return Task.Run(async () =>
            {
                bool success = false;

                LabelledImage lImg = await Program.ImageDatabase.Images_Add(img, fileName, sensorType, labelID);

                if (lImg != null)
                {
                    LabelledROI newLabelledROI = Program.ImageDatabase.BBoxLabels_AddLabel(lImg.ID, roi, labelID);

                    lImg.Tags = tags;
                    Program.ImageDatabase.Images_Update(lImg);

                    success = (newLabelledROI != null);
                }

                return success;
            });
        }

        private Bitmap RotateAndResizeObjectImage(Bitmap obj, float ang_rad)
        {
            float[] h = new float[4];
            float[] w = new float[4];
            float cos = (float)Math.Cos(ang_rad);
            float sin = (float)Math.Sin(ang_rad);
            Rectangle rect = new Rectangle();
            bool stop = false;
            h[0] = obj.Width * sin + obj.Height * cos;
            h[1] = obj.Width * sin - obj.Height * cos;
            h[2] = -obj.Width * sin + obj.Height * cos;
            h[3] = -obj.Width * sin - obj.Height * cos;
            w[0] = obj.Width * cos + obj.Height * sin;
            w[1] = obj.Width * cos - obj.Height * sin;
            w[2] = -obj.Width * cos + obj.Height * sin;
            w[3] = -obj.Width * cos - obj.Height * sin;

            SizeF rotatedObjSize = new SizeF(w.Max(), h.Max());

            Bitmap newObj = new Bitmap((int)w.Max(), (int)h.Max());
            Graphics g = Graphics.FromImage(newObj);

            g.TranslateTransform(newObj.Width / 2, newObj.Height / 2);
            g.RotateTransform(ang_rad * 180 / PI);
            g.DrawImage(obj, -obj.Width / 2, -obj.Height / 2);

            // Find Left of image
            stop = false;
            for (int x = 0; x < newObj.Width; x++)
            {
                for (int y = 0; y < newObj.Height; y++)
                {
                    Color pixel = newObj.GetPixel(x, y);
                    if (pixel.A != 0x00) { rect.X = x; stop = true; break; }
                }
                if (stop) { break; }
            }

            // Find Top of image
            stop = false;
            for (int y = 0; y < newObj.Height; y++)
            {
                for (int x = 0; x < newObj.Width; x++)
                {
                    Color pixel = newObj.GetPixel(x, y);
                    if (pixel.A != 0x00) { rect.Y = y; stop = true; break; }
                }
                if (stop) { break; }
            }

            // Find Right of image
            stop = false;
            for (int x = newObj.Width - 1; x >= 0 ; x--)
            {
                for (int y = 0; y < newObj.Height; y++)
                {
                    Color pixel = newObj.GetPixel(x, y);
                    if (pixel.A != 0x00) { rect.Width = x - rect.X; stop = true; break; }
                }
                if (stop) { break; }
            }

            // Find Bottom of image
            stop = false;
            for (int y = newObj.Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < newObj.Width; x++)
                {
                    Color pixel = newObj.GetPixel(x, y);
                    if (pixel.A != 0x00) { rect.Height = y - rect.Y; stop = true; break; }
                }
                if (stop) { break; }
            }

            Bitmap finalBmp = new Bitmap(rect.Width, rect.Height);
            g = Graphics.FromImage(finalBmp);
            g.DrawRectangle(new Pen(Brushes.White), new Rectangle(0, 0, rect.Width, rect.Height));
            g.DrawImage(newObj, 0, 0, rect, GraphicsUnit.Pixel);
            g.DrawRectangle(new Pen(Brushes.White), new Rectangle(0, 0, rect.Width, rect.Height));

            return finalBmp;
        }

        private string CreateRandomFileName()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[8];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string randString = new String(stringChars);
            randString += DateTime.Now.ToString("hhmmss");

            return randString;
        }
    }

    sealed class CompositeCreatorSettings : ApplicationSettingsBase
    {
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("C:\\\\")]
        public string BackgroundsDirectory
        {
            get { return (string)this["BackgroundsDirectory"]; }
            set { this["BackgroundsDirectory"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("C:\\\\")]
        public string ObjectsDirectory
        {
            get { return (string)this["ObjectsDirectory"]; }
            set { this["ObjectsDirectory"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("0.1")]
        public float ScaleMin
        {
            get { return (float)this["ScaleMin"]; }
            set { this["ScaleMin"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("0.5")]
        public float ScaleMax
        {
            get { return (float)this["ScaleMax"]; }
            set { this["ScaleMax"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("0.0")]
        public float RotationRange_deg
        {
            get { return (float)this["RotationRange_deg"]; }
            set { this["RotationRange_deg"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("10")]
        public int NumberOfVariations
        {
            get { return (int)this["NumberOfVariations"]; }
            set { this["NumberOfVariations"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool MinimiseBBoxWithRotation
        {
            get { return (bool)this["MinimiseBBoxWithRotation"]; }
            set { this["MinimiseBBoxWithRotation"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool IncludeSubDirsBackground
        {
            get { return (bool)this["IncludeSubDirsBackground"]; }
            set { this["IncludeSubDirsBackground"] = value; }
        }

        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool IncludeSubDirsObjects
        {
            get { return (bool)this["IncludeSubDirsObjects"]; }
            set { this["IncludeSubDirsObjects"] = value; }
        }
    }
}
