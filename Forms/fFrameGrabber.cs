using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Video.FFMPEG;
using System.IO;
using LabellingDB;
using System.Drawing.Imaging;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fFrameGrabber : Form
    {
        VideoFileReader _Reader;
        int _LastFrameIndex = 0;
        bool _FrameRefreshRequired = true;
        Image _Frame;
        System.Timers.Timer _Timer;
        string _VideoName;
        public Image[] Frames { get; set; } = null;
        public string[] FrameNames { get; set; } = null;
        public int LabelID { get; set; } = -1;
        public string Tags { get; set; } = "";
        public Size VideoFrameSize { get; set; }

        public List<string> VideoClipFileNames { get; private set; } = new List<string>();

        public SensorTypeEnum SensorType { get; set; } = SensorTypeEnum.Unknown;

        public fFrameGrabber(string filePath)
        {
            InitializeComponent();
            _Reader = new VideoFileReader();
            _Reader.Open(filePath);

            VideoFrameSize = new Size(_Reader.Width, _Reader.Height);

            ckbArchiveVideo.Checked = Properties.Settings.Default.ArchiveVideos;
            float videoLength_s = (float)(_Reader.FrameCount) / (float)(_Reader.FrameRate);

            _VideoName = Path.GetFileNameWithoutExtension(filePath);

            _Frame = _Reader.ReadVideoFrame(0);
            pcbFrame.BackgroundImage = _Frame;
            tkbScan.Maximum = (int)_Reader.FrameCount - 1;
            cpsClipSelector.RegisterNewVideo((int)_Reader.FrameCount, 1 / (float)_Reader.FrameRate.Value);
            _Timer = new System.Timers.Timer(100);
            _Timer.Elapsed += _Timer_Elapsed;
            _Timer.Start();

            Text = filePath;
            cpsClipSelector.ClipAdded += cpsClipSelector_ClipAdded;
            cpsClipSelector.ClipDeleted += cpsClipSelector_ClipDeleted;
            cmbSensorType.DataSource = Enum.GetValues(typeof(SensorTypeEnum));
            cmbSensorType.SelectedItem = SensorTypeEnum.Unknown;
            cmbSensorType.SelectedIndexChanged += CmbSensorType_SelectedIndexChanged;
            tkbScan.MouseUp += TkbScan_MouseUp;
            tkbScan.MouseLeave += TkbScan_MouseLeave;
        }

        private void TkbScan_MouseLeave(object sender, EventArgs e)
        {
            if (_LastFrameIndex != tkbScan.Value)
            {
                _FrameRefreshRequired = true;
                _LastFrameIndex = tkbScan.Value;
            }
        }

        private void TkbScan_MouseUp(object sender, MouseEventArgs e)
        {
            if (_LastFrameIndex != tkbScan.Value)
            {
                _FrameRefreshRequired = true;
                _LastFrameIndex = tkbScan.Value;
            }
        }

        private void CmbSensorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SensorType = (SensorTypeEnum)cmbSensorType.SelectedItem;
        }

        private void cpsClipSelector_ClipDeleted(ClipSelector sender, int index)
        {
            lblClipCount.Text = "Clips: " + cpsClipSelector.Clips.Count.ToString();
        }

        private void cpsClipSelector_ClipAdded(ClipSelector sender, ClipInfo clip)
        {
            lblClipCount.Text = "Clips: " + cpsClipSelector.Clips.Count.ToString();
        }

        private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_FrameRefreshRequired)
            {
                _FrameRefreshRequired = false;
                _Timer.Stop();
                this.UseWaitCursor = true;
                _Frame = _Reader.ReadVideoFrame(_LastFrameIndex);
                this.BeginInvoke(new Action(() => { pcbFrame.BackgroundImage = _Frame; }));
                this.UseWaitCursor = false;
                _Timer.Start();
            }
        }

        private void tkbScan_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void btnSaveFrame_Click(object sender, EventArgs e)
        {
            Image img = _Frame.GetThumbnailImage(_Frame.Width, _Frame.Height, null, IntPtr.Zero);            
            ListViewItem newThumb = new ListViewItem();
            
            imgFrames.Images.Add(img);
            
            newThumb.Text = _LastFrameIndex.ToString();
            newThumb.ImageIndex = imgFrames.Images.Count - 1;
            newThumb.Tag = _Frame.Clone();

            ltvThumbnails.Items.Add(newThumb);
            lblImageCount.Text = "Images: " + ltvThumbnails.Items.Count.ToString();
        }

        private void ltvThumbnails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && ltvThumbnails.SelectedItems.Count > 0)
            {
                int index = ltvThumbnails.SelectedItems[0].Index;
                ltvThumbnails.Items.RemoveAt(index);
                lblImageCount.Text = "Images: " + ltvThumbnails.Items.Count.ToString();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            bool save = true;

            if ((SensorTypeEnum)cmbSensorType.SelectedItem == SensorTypeEnum.Unknown || tgbTags.ToString() == "" || LabelID == 0)
            {
                if (MessageBox.Show("File attributes are incomplete.\r\n\r\nWould you like to continue?", "Incomplete attributes", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    save = false;
                }
            }

            if (save)
            {
                Properties.Settings.Default.ArchiveVideos = ckbArchiveVideo.Checked;

                if (ltvThumbnails.Items.Count > 0)
                {
                    Frames = new Image[ltvThumbnails.Items.Count];
                    FrameNames = new string[ltvThumbnails.Items.Count];

                    for (int i = 0; i < Frames.Length; i++)
                    {
                        Frames[i] = (Image)ltvThumbnails.Items[i].Tag;
                        FrameNames[i] = _VideoName + "_" + ltvThumbnails.Items[i].Text;
                    }
                }

                Tags = tgbTags.ToString();

                VideoClipFileNames = await ExportVideoClips(cpsClipSelector.Clips, _VideoName);

                _Reader.Close();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
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

        #region "Helper Functions"
        private async Task<List<string>> ExportVideoClips(List<ClipInfo> clips, string videoName)
        {
            List<string> newVideoFilePaths = new List<string>();

            if (clips.Count > 0)
            {
                int clipIndex = 1;
                string workingDir = AppDomain.CurrentDomain.BaseDirectory;
                workingDir = Path.Combine(workingDir, "TempFiles");
                Directory.CreateDirectory(workingDir);

                fProgressBar progress = new fProgressBar("Exporting Clips", "Exporting clip 1 of " + clips.Count);
                progress.Show();

                foreach (ClipInfo c in clips)
                {
                    try
                    {
                        string fileName = videoName + " (" + c.StartFrame.ToString() + "_" + c.EndFrame.ToString() + ").mp4";
                        string fullFilePath = Path.Combine(workingDir, fileName);

                        await Task.Run(() =>
                        {
                            using (var writer = new VideoFileWriter())
                            {
                                writer.Open(fullFilePath, _Reader.Width, _Reader.Height, _Reader.FrameRate, VideoCodec.MPEG4, _Reader.BitRate);

                                //See https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.bitmapdata?view=dotnet-plat-ext-3.1
                                Bitmap bmp = _Reader.ReadVideoFrame(0); //Read the first frame to get the correct height, width of the bitmap, will use this to store the frame as we transfer it
                                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                                BitmapData storageFrame = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);

                                for (int i = c.StartFrame; i <= c.EndFrame; i++)
                                {
                                    _Reader.ReadVideoFrame(i, storageFrame);
                                    writer.WriteVideoFrame(storageFrame);

                                    if (progress.Cancelled) { break; }

                                    float percent = 100 * (i - c.StartFrame) / c.Length;
                                    progress.BeginInvoke((Action)(() => progress.UpdateProgress(percent, "Exporting clip " + clipIndex.ToString() + " of " + clips.Count)));
                                }
                                writer.Close();
                            }
                        });
                        newVideoFilePaths.Add(fullFilePath);

                    }
                    catch (Exception ex) {
                        MessageBox.Show("Error exporting video clip " + videoName + "\n" + ex.Message);
                    }
                    clipIndex += 1;

                    if (progress.Cancelled) { break; }
                }

                progress.Close();
            }

            return newVideoFilePaths;
        }
        #endregion
    }
}
