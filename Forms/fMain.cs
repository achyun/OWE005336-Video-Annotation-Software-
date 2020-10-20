using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using LabellingDB;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fMain : Form
    {
        private Size _THUMBNAIL_SIZE;
        private bool _PaintDataInProgress = false;

        public fMain()
        {
            InitializeComponent();
            _THUMBNAIL_SIZE = new Size(50, 50);

            trvLabels.PopulateLabels();

            string imageDirPath = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_IMAGE_DIR);
            txtImageDir.Text = imageDirPath;
            txtVideoArchiveDir.Text = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_VIDEO_ARCHIVE_DIR);

            LoadImages();

            dgvImages.DragEnter += dgvImages_DragEnter;
            dgvImages.DragDrop += dgvImages_DragDrop;
            dgvImages.SelectionChanged += dgvImages_SelectionChanged;
            dgvImages.CellValueChanged += dgvImages_CellValueChanged;
            
        }

        private void dgvImages_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LabelledImage lImg = (LabelledImage)dgvImages.Rows[e.RowIndex].Tag;

            if (e.ColumnIndex == dgvImages.Columns["Complete"].Index)
            {
                if (!_PaintDataInProgress)
                {
                    lImg.Completed = (bool)dgvImages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    Program.ImageDatabase.Images_Update(lImg);
                }
            }
        }

        #region "GUI Events"
        private void mniOpen_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.DefaultFileLocation;
                //openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                //openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    Properties.Settings.Default.DefaultFileLocation = System.IO.Path.GetDirectoryName(filePath);
                    LoadFile(filePath);
                }
            }
        }

        private void btnSetImageDir_Click(object sender, EventArgs e)
        {
            string dirPath = string.Empty;
            var fileContent = string.Empty;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImageDir.Text = folderBrowserDialog.SelectedPath;
                    TestAndSaveDirectoryRef(folderBrowserDialog.SelectedPath, ImageDatabaseAccess.SETTING_IMAGE_DIR);
                    
                }
            }
        }

        private void btnSetVideoArchiveDir_Click(object sender, EventArgs e)
        {
            string dirPath = string.Empty;
            var fileContent = string.Empty;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtVideoArchiveDir.Text = folderBrowserDialog.SelectedPath;
                    TestAndSaveDirectoryRef(folderBrowserDialog.SelectedPath, ImageDatabaseAccess.SETTING_VIDEO_ARCHIVE_DIR);
                }
            }
        }

        private void dgvImages_SelectionChanged(object sender, EventArgs e)
        {
            if (!_PaintDataInProgress)
            {
                if (dgvImages.SelectedRows.Count > 0)
                {
                    LabelledImage img = (LabelledImage)dgvImages.SelectedRows[0].Tag;
                    img.LabelledROIs = Program.ImageDatabase.BBoxLabels_LoadByImageID(img.ID);

                    lbiImage.SetLabelledImage(img);
                }
                else
                {
                    lbiImage.SetLabelledImage(null);
                }
            }
        }

        private void dgvImages_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePathsArray = (string[])e.Data.GetData(DataFormats.FileDrop);
            ImportImages(filePathsArray);            
        }

        private void dgvImages_DragEnter(object sender, DragEventArgs e)
        {
            String[] allFormats = e.Data.GetFormats();

            if (allFormats.Contains("FileName"))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void dgvImages_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvImages.SelectedRows.Count > 0)
            {
                List<DataGridViewRow> toBeDeleted = new List<DataGridViewRow>();
                foreach (DataGridViewRow dr in dgvImages.SelectedRows)
                {
                    LabelledImage img = (LabelledImage)dr.Tag;
                    if (Program.ImageDatabase.Images_DeleteImage(img))
                    {
                        toBeDeleted.Add(dr);
                    }
                    
                }

                foreach (DataGridViewRow dr in toBeDeleted)
                {
                    dgvImages.Rows.Remove(dr);
                }
            }
        }

        private void LoadFile(string filePath)
        {
            var reader = new Accord.Video.FFMPEG.VideoFileReader();
            reader.Open(filePath);
            //lbiImage.Frame = reader.ReadVideoFrame(1);
            //pcbEditFrame.Image = bitmap;
        }
        private void btnOpenImages_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.DefaultFileLocation;
                openFileDialog.Filter = "Image Files(*.bmp;*.jpg;*.ong)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string[] filePathsArray = openFileDialog.FileNames;
                    ImportImages(filePathsArray);
                }
            }
        }

        private void btnOpenVideo_Click(object sender, EventArgs e)
        {
            string filePath = string.Empty;
            var fileContent = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Properties.Settings.Default.DefaultFileLocation;
                openFileDialog.Filter = "Video Files(*.mp4;*.avi)|*.mp4;*.avi;|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    Properties.Settings.Default.DefaultFileLocation = Path.GetDirectoryName(filePath);
                    fFrameGrabber grabber = new fFrameGrabber(filePath);
                    if (grabber.ShowDialog() == DialogResult.OK)
                    {
                        if (grabber.Frames != null)
                        {
                            ImportImages(grabber.Frames, grabber.FrameNames, grabber.SensorType, grabber.LabelID, grabber.Tags);
                        }

                        if (grabber.VideoClipFileNames.Count > 0)
                        {
                            ImportVideos(grabber.VideoClipFileNames.ToArray(), grabber.SensorType, grabber.LabelID, grabber.Tags);
                        }
                    }
                }
            }
        }
        #endregion

        #region "Helper Functions"
        private void ImportImages(string[] sourceFilePathsArray)
        {
            List<string> destFilePaths = new List<string>();
            string todaysFolderName = DateTime.Now.ToString("yyyy-MM-dd");
            string imgDir = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_IMAGE_DIR);
            string fullDirPath = Path.Combine(imgDir, todaysFolderName);
            
            Directory.CreateDirectory(fullDirPath); // This includes a check to see if it already exists

            for (int i = 0; i < sourceFilePathsArray.Length; i++)
            {
                string ext = Path.GetExtension(sourceFilePathsArray[i]);

                if (ext == ".png" || ext == ".bmp" || ext == ".jpg")
                {
                    string fullFilePath = Path.Combine(todaysFolderName, Path.GetFileName(sourceFilePathsArray[i]));
                    destFilePaths.Add(fullFilePath);
                }
            }

            SaveImages(sourceFilePathsArray, destFilePaths.ToArray());
        }

        private void ImportImages(Image[] images, string[] names, SensorTypeEnum sensorType, int labelID, string tags)
        {
            List<string> destFilePaths = new List<string>();
            string todaysFolderName = DateTime.Now.ToString("yyyy-MM-dd");
            string imgDir = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_IMAGE_DIR);
            string fullDirPath = Path.Combine(imgDir, todaysFolderName);

            Directory.CreateDirectory(fullDirPath); // This includes a check to see if it already exists

            for (int i = 0; i < names.Length; i++)
            {
                string fullFilePath = Path.Combine(todaysFolderName, names[i] + ".png");
                destFilePaths.Add(fullFilePath);
            }

            SaveImages(images, destFilePaths.ToArray(), sensorType, labelID, tags);
        }
        private async void SaveImages(string[] sourcefilePathsArray, string[] destFilePathsArray)
        {
            List<string> failedCopies = new List<string>();

            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("0/" + sourcefilePathsArray.Length.ToString());
            stsStatus.Items.Add(statusLabel);

            for (int i = 0; i < sourcefilePathsArray.Length; i++)
            {
                LabelledImage lImg = await Program.ImageDatabase.Images_Add(sourcefilePathsArray[i], destFilePathsArray[i]);
                if (lImg == null)
                {
                    failedCopies.Add(sourcefilePathsArray[i]);
                }
                else
                {
                    AddImageToGrid(lImg);
                    LoadThumbnail(lImg, _THUMBNAIL_SIZE, dgvImages.Rows.Count - 1);
                }
                statusLabel.Text = (i + 1).ToString() + "/" + sourcefilePathsArray.Length.ToString();
            }

            statusLabel.Text = sourcefilePathsArray.Length.ToString() + "/" + sourcefilePathsArray.Length.ToString();
            if (failedCopies.Count > 0)
            {
                string failedFiles = "";
                for (int i = 0; i < failedCopies.Count; i++)
                {
                    failedFiles += "\r\n\t" + failedCopies[i];
                }
                MessageBox.Show("Failed to import the following files:" + failedFiles, "Image Import Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            await Task.Delay(750);
            stsStatus.Items.Remove(statusLabel);
        }

        private async void SaveImages(Image[] images, string[] destFilePathsArray, SensorTypeEnum sensorType, int labelID, string tags)
        {
            List<string> failedCopies = new List<string>();
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("0/" + destFilePathsArray.Length.ToString());
            stsStatus.Items.Add(statusLabel);

            for (int i = 0; i < destFilePathsArray.Length; i++)
            {
                LabelledImage lImg = await Program.ImageDatabase.Images_Add(images[i], destFilePathsArray[i], sensorType, labelID, tags);
                if (lImg == null)
                {
                    failedCopies.Add(destFilePathsArray[i]);
                }
                else
                {
                    AddImageToGrid(lImg);
                    LoadThumbnail(lImg, _THUMBNAIL_SIZE, dgvImages.Rows.Count - 1);
                }
                statusLabel.Text = (i + 1).ToString() + "/" + destFilePathsArray.Length.ToString();
            }

            statusLabel.Text = destFilePathsArray.Length.ToString() + "/" + destFilePathsArray.Length.ToString();
            if (failedCopies.Count > 0)
            {
                string failedFiles = "";
                for (int i = 0; i < failedCopies.Count; i++)
                {
                    failedFiles += "\r\n\t" + failedCopies[i];
                }
                MessageBox.Show("Failed to import the following files:" + failedFiles, "Image Import Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            await Task.Delay(750);
            stsStatus.Items.Remove(statusLabel);
        }

        private void ImportVideos(string[] sourceFilePathsArray, SensorTypeEnum sensorType, int labelID, string tags)
        {
            List<string> destFilePaths = new List<string>();
            string todaysFolderName = DateTime.Now.ToString("yyyy-MM-dd");
            string imgDir = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_VIDEO_ARCHIVE_DIR);
            string fullDirPath = Path.Combine(imgDir, todaysFolderName);

            Directory.CreateDirectory(fullDirPath); // This includes a check to see if it already exists

            for (int i = 0; i < sourceFilePathsArray.Length; i++)
            {
                string fullFilePath = Path.Combine(todaysFolderName, Path.GetFileName(sourceFilePathsArray[i]));
                destFilePaths.Add(fullFilePath);
            }

            SaveVideos(sourceFilePathsArray, destFilePaths.ToArray(), sensorType, labelID, tags);
        }

        private async void SaveVideos(string[] sourcefilePathsArray, string[] destFilePathsArray, SensorTypeEnum sensorType, int labelID, string tags)
        {
            List<string> failedCopies = new List<string>();

            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("0/" + sourcefilePathsArray.Length.ToString());
            stsStatus.Items.Add(statusLabel);

            for (int i = 0; i < sourcefilePathsArray.Length; i++)
            {
                Video video = await Program.ImageDatabase.Videos_Add(sourcefilePathsArray[i], destFilePathsArray[i], sensorType, labelID, tags);
                if (video == null)
                {
                    failedCopies.Add(sourcefilePathsArray[i]);
                }
                else
                {
                    // Code here to add the video to a GUI list.
                }
                statusLabel.Text = (i + 1).ToString() + "/" + sourcefilePathsArray.Length.ToString();
            }

            statusLabel.Text = sourcefilePathsArray.Length.ToString() + "/" + sourcefilePathsArray.Length.ToString();
            if (failedCopies.Count > 0)
            {
                string failedFiles = "";
                for (int i = 0; i < failedCopies.Count; i++)
                {
                    failedFiles += "\r\n\t" + failedCopies[i];
                }
                MessageBox.Show("Failed to import the following files:" + failedFiles, "Image Import Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }

            await Task.Delay(750);
            stsStatus.Items.Remove(statusLabel);

            try
            {
                for (int i = 0; i < sourcefilePathsArray.Length; i++)
                {
                    File.Delete(sourcefilePathsArray[i]);
                }
            }
            catch { }
        }
        private void LoadImages()
        {
            List<LabelledImage> lImages = Program.ImageDatabase.Images_Get(ckbFilterForIncomplete.Checked, ckbFilterForNoLabels.Checked);

            dgvImages.Rows.Clear();
            foreach (LabelledImage limg in lImages)
            {
                AddImageToGrid(limg);
            }

            dgvImages_SelectionChanged(null, null);


            for (int i = 0; i < lImages.Count; i++)
            {
                LoadThumbnail(lImages[i], _THUMBNAIL_SIZE, i);
            }
        }

        private void AddImageToGrid(LabelledImage limg)
        {
            _PaintDataInProgress = true;
            dgvImages.Rows.Add(new object[] { null, limg.Completed, limg.Filepath }); //ID, FileName, Thumbnail, Complete, Labels
            dgvImages.Rows[dgvImages.Rows.Count - 1].Tag = limg;

            _PaintDataInProgress = false;
        }

        private async void LoadThumbnail(LabelledImage limg, Size thumbnailSize, int rowIndex)
        {
            Image img = await Program.ImageDatabase.Images_LoadImageThumbnail(limg, thumbnailSize);
            if (img != null)
            {
                _PaintDataInProgress = true;
                dgvImages.Rows[rowIndex].Cells[0].Value = img;
                _PaintDataInProgress = false;
            }
        }
        #endregion

        private void ckbFilterForIncomplete_CheckedChanged(object sender, EventArgs e)
        {
            LoadImages();
        }

        private void ckbFilterForNoLabels_CheckedChanged(object sender, EventArgs e)
        {
            LoadImages();
        }

        private void txtImageDir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                TestAndSaveDirectoryRef(txtImageDir.Text, ImageDatabaseAccess.SETTING_IMAGE_DIR);
            }
        }

        private void txtVideoArchiveDir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                TestAndSaveDirectoryRef(txtVideoArchiveDir.Text, ImageDatabaseAccess.SETTING_VIDEO_ARCHIVE_DIR);
            }
        }

        private bool TestAndSaveDirectoryRef(string dirPath, string settingName)
        {
            bool success = false;

            try
            {
                if (Directory.Exists(dirPath))
                {
                    Program.ImageDatabase.Settings_Set(settingName, dirPath);
                    success = true;
                }
                else
                {
                    MessageBox.Show("Invalid Directory");
                }
                
            }
            catch
            {
                MessageBox.Show("Invalid Directory");
            }

            return success;
        }
    }
}
