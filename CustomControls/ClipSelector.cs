using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class ClipSelector : Control
    {
        private DragActionEnum _DragAction = DragActionEnum.None;
        private GrabPositionEnum _GrabPosition = GrabPositionEnum.None;
        private int _LastMouseFrameIndex;
        private int _NumberOfFrames;
        private float _FrameRate_Hz;
        private const int SELECT_OFFSET = 5;

        public delegate void ClipAddedEventHandler(ClipSelector sender, ClipInfo clip);
        public delegate void ClipDeletedEventHandler(ClipSelector sender, int index);
        public event ClipAddedEventHandler ClipAdded;
        public event ClipDeletedEventHandler ClipDeleted;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<ClipInfo> Clips { get; set; } = null;
        public int SelectedClipIndex { get; set; }
        public int BorderWidth { get; set; }
        public int NumberOfFrames { get { return _NumberOfFrames; } }
        public int HighlightedClipIndex { get; set; }
        public ClipSelector()
        {
            InitializeComponent();
            Clips = new List<ClipInfo>();
            _NumberOfFrames = 100;
            BorderWidth = 5;
            SelectedClipIndex = -1;
            HighlightedClipIndex = -1;

            this.DoubleClick += ClipSelector_DoubleClick;
            this.MouseDown += ClipSelector_MouseDown;
            this.MouseMove += ClipSelector_MouseMove;
            this.MouseUp += ClipSelector_MouseUp;
            this.KeyDown += ClipSelector_KeyDown;
        }

        private void ClipSelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (SelectedClipIndex >= 0 && e.KeyCode == Keys.Delete)
            {
                Clips.RemoveAt(SelectedClipIndex);
                ClipDeleted?.Invoke(this, SelectedClipIndex);
                SelectedClipIndex = -1;
                this.Refresh();
            }
        }

        public void RegisterNewVideo(int numberOfFrames, float frameRate_Hz)
        {
            _NumberOfFrames = numberOfFrames;
            _FrameRate_Hz = frameRate_Hz;
        }

        private void ClipSelector_DoubleClick(object sender, EventArgs e)
        {
            if (_GrabPosition == GrabPositionEnum.None)
            {
                ClipInfo ci = new ClipInfo();
                int clipHalfLength = NumberOfFrames / 20;
                ci.StartFrame = Math.Max(_LastMouseFrameIndex - clipHalfLength, 0);
                ci.EndFrame = Math.Min(_LastMouseFrameIndex + clipHalfLength, NumberOfFrames - 1);
                Clips.Add(ci);

                ClipAdded?.Invoke(this, ci);

                this.Refresh();
            }
        }

        private void ClipSelector_MouseDown(object sender, MouseEventArgs e)
        {
            if (_GrabPosition != GrabPositionEnum.None)
            {
                SelectedClipIndex = HighlightedClipIndex;
                if (_GrabPosition == GrabPositionEnum.StartFrame) { _DragAction = DragActionEnum.MoveStart; }
                if (_GrabPosition == GrabPositionEnum.EndFrame) { _DragAction = DragActionEnum.MoveEnd; }
                if (_GrabPosition == GrabPositionEnum.Body) { _DragAction = DragActionEnum.Move; }
            }

            this.Focus();
        }

        private void ClipSelector_MouseMove(object sender, MouseEventArgs e)
        {
            int fIndex = GetFrameIndexFromPoint(e.X);
            ClipInfo ci;

            switch (_DragAction)
            {
                case DragActionEnum.MoveStart:
                    ci = Clips[SelectedClipIndex];
                    if (fIndex < ci.EndFrame) { ci.StartFrame = fIndex; this.Refresh(); }
                    break;
                case DragActionEnum.MoveEnd:
                    ci = Clips[SelectedClipIndex];
                    if (fIndex > ci.StartFrame) { ci.EndFrame = fIndex; this.Refresh(); }
                    break;
                case DragActionEnum.Move:
                    ci = Clips[SelectedClipIndex];
                    int delta = fIndex - _LastMouseFrameIndex;
                    int sf = ci.StartFrame + delta;
                    int ef = ci.EndFrame + delta;
                    
                    if (sf >= 0 && ef <= NumberOfFrames)
                    {
                        ci.StartFrame = sf;
                        ci.EndFrame = ef;
                    }
                    break;
                case DragActionEnum.None:
                    _GrabPosition = GrabPositionEnum.None;
                    HighlightedClipIndex = -1;

                    // Check clip resize
                    for (int i = 0; i < Clips.Count; i++)
                    {
                        if (fIndex > (Clips[i].StartFrame - SELECT_OFFSET) && fIndex < (Clips[i].StartFrame + SELECT_OFFSET))
                        {
                            _GrabPosition = GrabPositionEnum.StartFrame;
                            Cursor.Current = Cursors.SizeWE;
                            HighlightedClipIndex = i;
                            break;
                        }
                        else if (fIndex > (Clips[i].EndFrame - SELECT_OFFSET) && fIndex < (Clips[i].EndFrame + SELECT_OFFSET))
                        {
                            _GrabPosition = GrabPositionEnum.EndFrame;
                            Cursor.Current = Cursors.SizeWE;
                            HighlightedClipIndex = i;
                            break;
                        }
                    }


                    // Check clip move if no bounds selected
                    if (_GrabPosition == GrabPositionEnum.None)
                    {
                        for (int i = 0; i < Clips.Count; i++)
                        {
                            if (fIndex > Clips[i].StartFrame && fIndex < Clips[i].EndFrame)
                            {
                                _GrabPosition = GrabPositionEnum.Body;
                                Cursor.Current = Cursors.NoMoveHoriz;
                                HighlightedClipIndex = i;
                                break;
                            }
                        }
                    }

                    // If no bounds or bodies selected the restore cursor to default
                    if (_GrabPosition == GrabPositionEnum.None) { Cursor.Current = Cursors.Default; }
                    break;
            }

            this.Refresh();
            _LastMouseFrameIndex = fIndex;
        }
        private void ClipSelector_MouseUp(object sender, MouseEventArgs e)
        {
            _DragAction = DragActionEnum.None;
        }

        public void AddClip(int startFrame, int endFrame)
        {
            if (startFrame >= endFrame) { throw new Exception("Start Frame must precede End Frame"); }
            if (startFrame < 0 || startFrame > NumberOfFrames -1) { throw new Exception("Start Frame is out of bounds"); }
            if (endFrame < 0 || endFrame > NumberOfFrames - 1) { throw new Exception("End Frame is out of bounds"); }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Graphics g = pe.Graphics;

            g.DrawRectangle(new Pen(Color.Black), this.DisplayRectangle);
            if (Clips != null)
            {
                
                SolidBrush b = new SolidBrush(Color.Orange);
                SolidBrush fb = new SolidBrush(Color.Black);
                
                
                Font f = new Font(SystemFonts.DefaultFont.FontFamily, 8);
                

                for (int i = 0; i < Clips.Count; i++)
                {
                    if (i != SelectedClipIndex)
                    {
                        if (i == HighlightedClipIndex) { b.Color = Color.DarkOrange; }
                        else { b.Color = Color.Orange; }

                        DrawClip(Clips[i], g, b, f, fb);
                    }
                }

                if (SelectedClipIndex >= 0)
                {
                    b.Color = Color.Red;
                    DrawClip(Clips[SelectedClipIndex], g, b, f, fb);
                }
            }
        }

        private void DrawClip(ClipInfo clip, Graphics g, Brush clipBrush, Font font, Brush fontBrush)
        {
            int x1, x2;
            Rectangle r;
            float clipLength_s;
            string strClipLength;
            x1 = GetPixelFromFrameIndex(clip.StartFrame);
            x2 = GetPixelFromFrameIndex(clip.EndFrame);

            r = new Rectangle(x1, BorderWidth, x2 - x1, this.Height - 2 * BorderWidth);

            g.FillRectangle(clipBrush, r);

            clipLength_s = clip.Length * _FrameRate_Hz;
            strClipLength = clipLength_s.ToString() + "s";
            SizeF strLen = g.MeasureString(strClipLength, font);
            g.DrawString(strClipLength, font, fontBrush, x1 + (r.Width - strLen.Width) / 2, (this.Height - strLen.Height) / 2);
        }

        private int GetFrameIndexFromPoint(int x)
        {
            //return ((x - BorderWidth) / (this.Width - 2 * BorderWidth)) * (NumberOfFrames - 1);
            float controlWidth = (float)(this.Width - 2 * BorderWidth);
            float xControl = (float)(x - BorderWidth);
            float maxFrameIndex = (float)(NumberOfFrames - 1);
            return (int)(maxFrameIndex * xControl / controlWidth);
            
        }

        private int GetPixelFromFrameIndex(int fIndex)
        {
            //return (fIndex * (this.Width - 2 * BorderWidth) / (NumberOfFrames - 1)) + BorderWidth;
            float controlWidth = (float)(this.Width - 2 * BorderWidth);
            float maxFrameIndex = (float)(NumberOfFrames - 1);

            return (int)((float)fIndex * controlWidth / maxFrameIndex) + BorderWidth;
        }

        private enum DragActionEnum
        {
            None,
            MoveStart,
            MoveEnd,
            Move
        }

        private enum GrabPositionEnum
        {
            None,
            Body,
            StartFrame,
            EndFrame
        }
    }


    public class ClipInfo
    {
        public int StartFrame { get; set; }
        public int EndFrame { get; set; }

        public int Length { get { return EndFrame - StartFrame; } }
    }
}
