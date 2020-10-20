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

        public List<string> VideoClipFileNames { get; private set; } = new List<string>();

        public SensorTypeEnum SensorType { get; set; } = SensorTypeEnum.Unknown;

        public fFrameGrabber(string filePath)
        {
            InitializeComponent();
            _Reader = new VideoFileReader();
            _Reader.Open(filePath);

            float videoLength_s = (float)(_Reader.FrameCount) / (float)(_Reader.FrameRate);

            _VideoName = Path.GetFileNameWithoutExtension(filePath);

            pcbFrame.Image = _Reader.ReadVideoFrame(0);
            tkbScan.Maximum = (int)_Reader.FrameCount - 1;
            cpsClipSelector.RegisterNewVideo((int)_Reader.FrameCount, (float)_Reader.FrameRate.Value/1000);
            _Timer = new System.Timers.Timer(100);
            _Timer.Elapsed += _Timer_Elapsed;
            _Timer.Start();

            cpsClipSelector.ClipAdded += ClipSelector1_ClipAdded;
            cpsClipSelector.ClipDeleted += ClipSelector1_ClipDeleted;
            cmbSensorType.DataSource = Enum.GetValues(typeof(SensorTypeEnum));
            cmbSensorType.SelectedItem = SensorTypeEnum.Unknown;
            cmbSensorType.SelectedIndexChanged += CmbSensorType_SelectedIndexChanged;
        }

        private void CmbSensorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SensorType = (SensorTypeEnum)cmbSensorType.SelectedItem;
        }

        private void ClipSelector1_ClipDeleted(ClipSelector sender, int index)
        {
            lblClipCount.Text = "Clips: " + cpsClipSelector.Clips.Count.ToString();
        }

        private void ClipSelector1_ClipAdded(ClipSelector sender, ClipInfo clip)
        {
            lblClipCount.Text = "Clips: " + cpsClipSelector.Clips.Count.ToString();
        }

        private void _Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_FrameRefreshRequired)
            {
                _FrameRefreshRequired = false;
                _Timer.Stop();
                _Frame = _Reader.ReadVideoFrame(_LastFrameIndex);
                this.BeginInvoke(new Action(() => { pcbFrame.Image = _Frame; }));
                _Timer.Start();
            }
        }

        private void fFrameGrabber_Load(object sender, EventArgs e)
        {

        }

        private void tkbScan_ValueChanged(object sender, EventArgs e)
        {
            _FrameRefreshRequired = true;
            _LastFrameIndex = tkbScan.Value;
        }

        private void btnSaveFrame_Click(object sender, EventArgs e)
        {
            Image img = pcbFrame.Image.GetThumbnailImage(imgFrames.ImageSize.Width, imgFrames.ImageSize.Height, null, IntPtr.Zero);            
            ListViewItem newThumb = new ListViewItem();
            
            imgFrames.Images.Add(img);
            
            newThumb.Text = _LastFrameIndex.ToString();
            newThumb.ImageIndex = imgFrames.Images.Count - 1;
            newThumb.Tag = pcbFrame.Image.Clone();

            ltvThumbnails.Items.Add(newThumb);
            lblImageCount.Text = "Images: " + ltvThumbnails.Items.Count.ToString();
        }

        private void ltvThumbnails_KeyDown(object sender, KeyEventArgs e)
        {
            if (ltvThumbnails.SelectedItems.Count > 0)
            {
                int index = ltvThumbnails.SelectedItems[0].Index;
                ltvThumbnails.Items.RemoveAt(index);
                lblImageCount.Text = "Images: " + ltvThumbnails.Items.Count.ToString();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
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

            this.DialogResult = DialogResult.OK;
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
                        VideoFileWriter writer = new VideoFileWriter();
                        string fileName = videoName + " (" + c.StartFrame.ToString() + "_" + c.EndFrame.ToString() + ").mp4";
                        string fullFilePath = Path.Combine(workingDir, fileName);
                        writer.Open(fullFilePath, _Reader.Width, _Reader.Height, _Reader.FrameRate, VideoCodec.MPEG4, _Reader.BitRate);

                        for (int i = c.StartFrame; i <= c.EndFrame; i++)
                        {
                            await Task.Run(() => { writer.WriteVideoFrame(_Reader.ReadVideoFrame(i)); });

                            if (progress.Cancelled) { break; }
                            
                            float percent = 100 * (i - c.StartFrame) / c.Length;
                            progress.UpdateProgress(percent, "Exporting clip " + clipIndex.ToString() + " of " + clips.Count);
                        }
                        writer.Close();

                        newVideoFilePaths.Add(fullFilePath);
                    }
                    catch { }
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
