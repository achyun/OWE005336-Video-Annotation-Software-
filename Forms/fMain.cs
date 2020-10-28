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
        private PaintLocker _PaintDataInProgress = new PaintLocker();

        public fMain()
        {
            InitializeComponent();
            _THUMBNAIL_SIZE = new Size(50, 50);

            trvLabels.PopulateLabels();

            string imageDirPath = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_IMAGE_DIR);
            txtImageDir.Text = imageDirPath;
            txtVideoArchiveDir.Text = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_VIDEO_ARCHIVE_DIR);
            txtProcessedFilesDir.Text = Program.ImageDatabase.Settings_Get(ImageDatabaseAccess.SETTING_PROCESSED_FILE_ARCHIVE_DIR);

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
                if (!_PaintDataInProgress.Locked)
                {
                    bool completeValue = (bool)dgvImages.CurrentCell.Value;

                    bool imageDataIsValid = false;

                    // if we're trying to mark the image as complete then check that the data is actually complete
                    if (completeValue) { imageDataIsValid = CheckImageDataComplete(lImg); }
                    
                    if (!completeValue || imageDataIsValid)
                    {
                        lImg.Completed = (bool)dgvImages.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        Program.ImageDatabase.Images_Update(lImg);
                    }
                    else
                    {
                        dgvImages.RefreshEdit();
                    }
                    
                }
            }
        }

        private bool CheckImageDataComplete(LabelledImage lImage)
        {
            bool complete = true;

            if (lImage.LabelID == -1)
            {
                complete = false;
                MessageBox.Show("Missing label", "Missing Label", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (lImage.SensorType == SensorTypeEnum.Unknown)
            {
                complete = false;
                MessageBox.Show("Please select a sensor type", "Sensory Type Unknown", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (lImage.Tags == "")
            {
                if (MessageBox.Show("No tags have been provided.\r\n\r\nWould you like to continue?", "Incomplete attributes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    complete = false;
                }
            }

            if (lImage.LabelledROIs.Count == 0)
            {
                complete = false;
                MessageBox.Show("There are no labelled regions of interest\r\n\r\nPlease add at least one region of interest", "No Regions of Interest", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                foreach (LabelledROI lroi in lImage.LabelledROIs)
                {
                    if (!CheckLabelDataComplete(lroi, lImage.ImageSize))
                    {
                        complete = false;
                        break;
                    }
                }
            }

            return complete;
        }

        private bool CheckLabelDataComplete(LabelledROI lroi, Size imageSize)
        {
            bool complete = true;
            if (lroi.LabelID == -1)
            {
                complete = false;
                MessageBox.Show("Missing label", "Missing Label", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (lroi.ROI.Left < 0 || lroi.ROI.Top < 0 || lroi.ROI.Right > imageSize.Width - 1 || lroi.ROI.Bottom > imageSize.Height - 1)
            {
                complete = false;
                MessageBox.Show("Region is out of bounds", "Region Out of Bounds", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return complete;
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

        private void btnSelectProcessedFileArchiveDir_Click(object sender, EventArgs e)
        {
            string dirPath = string.Empty;
            var fileContent = string.Empty;

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    txtProcessedFilesDir.Text = folderBrowserDialog.SelectedPath;
                    TestAndSaveDirectoryRef(folderBrowserDialog.SelectedPath, ImageDatabaseAccess.SETTING_PROCESSED_FILE_ARCHIVE_DIR);
                }
            }
        }

        private void dgvImages_SelectionChanged(object sender, EventArgs e)
        {
            if (!_PaintDataInProgress.Locked)
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

            bool archiveFiles = false;
            if (MessageBox.Show("Would you like to move the selected files to the archive folder?", "Archive Files?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { archiveFiles = true; }

            ImportImages(filePathsArray, archiveFiles);            
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
                    bool archiveFiles = false;
                    if (MessageBox.Show("Would you like to move the selected files to the archive folder?", "Archive Files?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) { archiveFiles = true; }
                    ImportImages(filePathsArray, archiveFiles);
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
                            ImportVideos(grabber.VideoClipFileNames.ToArray(), grabber.VideoFrameSize, grabber.SensorType, grabber.LabelID, grabber.Tags);
                        }

                        bool archiveFile = Properties.Settings.Default.ArchiveVideos;
                        if (archiveFile) { Program.ImageDatabase.MoveFileToArchive(filePath); }
                    }
                }
            }
        }

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

        private void txtProcessedFilesDir_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                TestAndSaveDirectoryRef(txtProcessedFilesDir.Text, ImageDatabaseAccess.SETTING_PROCESSED_FILE_ARCHIVE_DIR);
            }
        }
        #endregion

        #region "Helper Functions"
        private void ImportImages(string[] sourceFilePathsArray, bool archiveSourceFile)
        {
            List<string> sourceFilePaths = new List<string>();

            for (int i = 0; i < sourceFilePathsArray.Length; i++)
            {
                string ext = Path.GetExtension(sourceFilePathsArray[i]);

                if (ext == ".png" || ext == ".bmp" || ext == ".jpg")
                {
                    sourceFilePaths.Add(sourceFilePathsArray[i]);
                }
            }

            SaveImages(sourceFilePathsArray, archiveSourceFile);
        }

        private void ImportImages(Image[] images, string[] names, SensorTypeEnum sensorType, int labelID, string tags)
        {
            List<string> destFileNames = new List<string>();

            for (int i = 0; i < names.Length; i++)
            {
                destFileNames.Add(names[i] + ".png");
            }

            SaveImages(images, destFileNames.ToArray(), sensorType, labelID, tags);
        }
        private async void SaveImages(string[] sourcefilePathsArray, bool archiveSourceFile)
        {
            List<string> failedCopies = new List<string>();

            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("0/" + sourcefilePathsArray.Length.ToString());
            stsStatus.Items.Add(statusLabel);

            for (int i = 0; i < sourcefilePathsArray.Length; i++)
            {
                LabelledImage lImg = await Program.ImageDatabase.Images_Add(sourcefilePathsArray[i], archiveSourceFile);
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

        private void ImportVideos(string[] sourceFilePathsArray, Size videoSize, SensorTypeEnum sensorType, int labelID, string tags)
        {
            SaveVideos(sourceFilePathsArray, videoSize, sensorType, labelID, tags);
        }

        private async void SaveVideos(string[] sourcefilePathsArray, Size videoSize, SensorTypeEnum sensorType, int labelID, string tags)
        {
            List<string> failedCopies = new List<string>();

            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("0/" + sourcefilePathsArray.Length.ToString());
            stsStatus.Items.Add(statusLabel);

            for (int i = 0; i < sourcefilePathsArray.Length; i++)
            {
                Video video = await Program.ImageDatabase.Videos_AddToVideoArchive(sourcefilePathsArray[i], videoSize, sensorType, labelID, tags);
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
            _PaintDataInProgress.Lock();
            dgvImages.Rows.Add(new object[] { null, limg.Completed, limg.Filepath }); //ID, FileName, Thumbnail, Complete, Labels
            dgvImages.Rows[dgvImages.Rows.Count - 1].Tag = limg;

            _PaintDataInProgress.Unlock();
        }

        private async void LoadThumbnail(LabelledImage limg, Size thumbnailSize, int rowIndex)
        {
            var cell = dgvImages.Rows[rowIndex].Cells[0];
            Image img = await Program.ImageDatabase.Images_LoadImageThumbnail(limg, thumbnailSize);
            if (img != null)
            {
                _PaintDataInProgress.Lock();
                cell.Value = img;
                _PaintDataInProgress.Unlock();
            }
        }
        #endregion

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
