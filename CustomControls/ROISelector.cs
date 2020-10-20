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
    public partial class ROISelector : Control
    {
        Bitmap _Frame;
        float _Scale = 1;
        PointF _Offset = new Point(0, 0);
        PointF _OldMouseLocationRaw;
        PointF _OldMouseLocationScaled;
        List<ROIObject> _ROIs = new List<ROIObject>();
        DragActionEnum _DragAction = DragActionEnum.None;
        //ROIObject _CurrentROI = null;
        GrabBoxLocation _CurrentGrabBox = GrabBoxLocation.None;
        int _HighlightedROIIndex = -1;
        int _SelectedROIIndex = -1;

        public delegate void NewROIAddedEventHandler(ROISelector sender, Rectangle roi);
        public delegate void ROIChangedEventHandler(ROISelector sender, int index, Rectangle roi);
        public delegate void ROIDeletedEventHandler(ROISelector sender, int index);
        public delegate void ROISelectionChangedEventHandler(ROISelector sender, int index);
        public event NewROIAddedEventHandler NewROIAdded;
        public event ROIChangedEventHandler ROIChanged;
        public event ROIDeletedEventHandler ROIDeleted;
        public event ROISelectionChangedEventHandler ROISelectionChanged;

        //public int HighlightedROIIndex
        //{
        //    get { return _HighlightedROIIndex; }
        //    set
        //    {
        //        if (value != _HighlightedROIIndex)
        //        {
        //            _HighlightedROIIndex = value;
        //            this.Refresh();
        //        }
                
        //    }
        //}

        public int SelectedROIIndex
        {
            get { return _SelectedROIIndex; }
            set
            {
                if (value != _SelectedROIIndex)
                {
                    _SelectedROIIndex = value;
                    ROISelectionChanged?.Invoke(this, _SelectedROIIndex);
                    this.Refresh();
                }

            }
        }

        public void LinkToLabelledImage(List<Rectangle> rois, Bitmap frame)
        {
            _Frame = frame;

            _ROIs.Clear();

            CalculateScale();

            foreach (Rectangle r in rois)
            {
                _ROIs.Add(new ROIObject(r, _Scale));
            }

            _HighlightedROIIndex = -1;
            _SelectedROIIndex = -1;
            
            this.Refresh();
        }

        public ROISelector()
        {
            InitializeComponent();
            this.Resize += ROISelector_Resize;
            this.MouseDown += ROISelector_MouseDown;
            this.MouseMove += ROISelector_MouseMove;
            this.MouseUp += ROISelector_MouseUp;
            this.KeyDown += ROISelector_KeyDown;
            this.MouseWheel += ROISelector_MouseWheel;

            this.DoubleBuffered = true;
        }

        private void ROISelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && _DragAction == DragActionEnum.DrawNew)
            {
                _DragAction = DragActionEnum.None;
                _ROIs.RemoveAt(_ROIs.Count - 1);
                this.Refresh();
            }
            else if (e.KeyCode == Keys.Delete && SelectedROIIndex >= 0)
            {
                _ROIs.RemoveAt(SelectedROIIndex);
                ROIDeleted?.Invoke(this, SelectedROIIndex);
                SelectedROIIndex = -1;
            }
        }

        private void ROISelector_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedROIIndex >= 0)
            {
                if (_DragAction == DragActionEnum.DrawNew)
                {
                    ROIChanged?.Invoke(this, SelectedROIIndex, Rectangle.Round(_ROIs[SelectedROIIndex].GetROI()));
                }
                else if (_DragAction == DragActionEnum.Resize || _DragAction == DragActionEnum.Move)
                {
                    ROIChanged?.Invoke(this, SelectedROIIndex, Rectangle.Round(_ROIs[SelectedROIIndex].GetROI()));
                }
                _DragAction = DragActionEnum.None;
                this.Refresh();
            }
            _DragAction = DragActionEnum.None;
        }

        private void ROISelector_MouseMove(object sender, MouseEventArgs e)
        {
            PointF p = TransformPoint(new Point(e.X, e.Y));
            var currentROI = (_HighlightedROIIndex >= 0 ? _ROIs[_HighlightedROIIndex] : null);

            switch (_DragAction)
            {
                case DragActionEnum.Pan:
                    _Offset.X -= (float)(_OldMouseLocationRaw.X - e.Location.X);
                    _Offset.Y -= (float)(_OldMouseLocationRaw.Y - e.Location.Y);

                    _OldMouseLocationRaw.X = e.Location.X;
                    _OldMouseLocationRaw.Y = e.Location.Y;

                    this.Refresh();
                    break;
                case DragActionEnum.DrawNew:
                    {
                        float x, y, w, h;

                        if (p.X > _OldMouseLocationScaled.X)
                        {
                            x = _OldMouseLocationScaled.X;
                            w = p.X - _OldMouseLocationScaled.X;
                        }
                        else
                        {
                            x = p.X;
                            w = _OldMouseLocationScaled.X - p.X;
                        }

                        if (p.Y > _OldMouseLocationScaled.Y)
                        {
                            y = _OldMouseLocationScaled.Y;
                            h = p.Y - _OldMouseLocationScaled.Y;
                        }
                        else
                        {
                            y = p.Y;
                            h = _OldMouseLocationScaled.Y - p.Y;
                        }

                        currentROI.SetRectangle(new RectangleF(x, y, w, h), _Scale);
                        this.Refresh();
                        break;
                    }
                case DragActionEnum.Resize:
                    {
                        var r = currentROI.GetROI();

                        float x = r.Left;
                        float y = r.Top;
                        float w = r.Width;
                        float h = r.Height;

                        switch (_CurrentGrabBox)
                        {
                            case GrabBoxLocation.TopLeft:
                                if (p.X < r.Right) { x = p.X; w = r.Right - p.X; }
                                if (p.Y < r.Bottom) { y = p.Y; h = r.Bottom - p.Y; }
                                break;
                            case GrabBoxLocation.TopRight:
                                if (p.X > r.Left) { w = p.X - r.Left; }
                                if (p.Y < r.Bottom) { y = p.Y; h = r.Bottom - p.Y; }
                                break;
                            case GrabBoxLocation.BottomRight:
                                if (p.X > r.Left) { w = p.X - r.Left; }
                                if (p.Y > r.Top) { h = p.Y - r.Top; }
                                break;
                            case GrabBoxLocation.BottomLeft:
                                if (p.X < r.Right) { x = p.X; w = r.Right - p.X; }
                                if (p.Y > r.Top) { h = p.Y - r.Top; }
                                break;
                            case GrabBoxLocation.TopMiddle:
                                if (p.Y < r.Bottom) { y = p.Y; h = r.Bottom - p.Y; }
                                break;
                            case GrabBoxLocation.RightMiddle:
                                if (p.X > r.Left) { w = p.X - r.Left; }
                                break;
                            case GrabBoxLocation.BottomMiddle:
                                if (p.Y > r.Top) { h = p.Y - r.Top; }
                                break;
                            case GrabBoxLocation.LeftMiddle:
                                if (p.X < r.Right) { x = p.X;  w = r.Right - p.X; }
                                break;
                        }

                        currentROI.SetRectangle(new RectangleF(x, y, w, h), _Scale);
                        this.Refresh();
                        break;
                    }
                case DragActionEnum.Move:
                    {
                        var r = currentROI.GetROI();
                        float x = r.Left - (_OldMouseLocationScaled.X - p.X);
                        float y = r.Top - (_OldMouseLocationScaled.Y - p.Y);

                        _OldMouseLocationScaled.X = p.X;
                        _OldMouseLocationScaled.Y = p.Y;

                        currentROI.SetRectangle(new RectangleF(x, y, r.Width, r.Height), _Scale);
                        this.Refresh();

                        break;
                    }
                case DragActionEnum.None:
                    {
                        GrabBoxLocation loc = GrabBoxLocation.None;
                        int oldHighlightedROIIndex = _HighlightedROIIndex;
                        _HighlightedROIIndex = -1;
                        _CurrentGrabBox = GrabBoxLocation.None;

                        for (int i = 0; i < _ROIs.Count; i++)
                        {
                            loc = _ROIs[i].TestMouseOverGrabBox(p);
                            if (loc != GrabBoxLocation.None)
                            {
                                _HighlightedROIIndex = i;
                                _CurrentGrabBox = loc;

                                switch (loc)
                                {
                                    case GrabBoxLocation.TopLeft:
                                        Cursor.Current = Cursors.SizeNWSE;
                                        break;
                                    case GrabBoxLocation.TopRight:
                                        Cursor.Current = Cursors.SizeNESW;
                                        break;
                                    case GrabBoxLocation.BottomRight:
                                        Cursor.Current = Cursors.SizeNWSE;
                                        break;
                                    case GrabBoxLocation.BottomLeft:
                                        Cursor.Current = Cursors.SizeNESW;
                                        break;
                                    case GrabBoxLocation.TopMiddle:
                                        Cursor.Current = Cursors.SizeNS;
                                        break;
                                    case GrabBoxLocation.RightMiddle:
                                        Cursor.Current = Cursors.SizeWE;
                                        break;
                                    case GrabBoxLocation.BottomMiddle:
                                        Cursor.Current = Cursors.SizeNS;
                                        break;
                                    case GrabBoxLocation.LeftMiddle:
                                        Cursor.Current = Cursors.SizeWE;
                                        break;
                                }

                                break;
                            }
                            else if (_ROIs[i].BorderContainsPoint(p))
                            {
                                _HighlightedROIIndex = i;
                                Cursor.Current = Cursors.NoMove2D;
                                break;
                            }
                        }

                        if (oldHighlightedROIIndex != _HighlightedROIIndex) { this.Refresh(); }

                        break;
                    }
            }
        }

        private void ROISelector_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            _OldMouseLocationScaled = TransformPoint(new Point(e.X, e.Y));
            _OldMouseLocationRaw = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Left && _Frame != null)
            {
                if (_HighlightedROIIndex < 0)
                {
                    ROIObject newROI = new ROIObject(new RectangleF(e.X, e.Y, 6, 6), _Scale);
                    _ROIs.Add(newROI);
                    _SelectedROIIndex = _ROIs.Count - 1;
                    _HighlightedROIIndex = _SelectedROIIndex;
                    _DragAction = DragActionEnum.DrawNew;
                    NewROIAdded?.Invoke(this, Rectangle.Round(_ROIs[_SelectedROIIndex].GetROI()));
                }
                else
                {
                    if (_CurrentGrabBox != GrabBoxLocation.None)
                    {
                        _DragAction = DragActionEnum.Resize;
                    }
                    else
                    {
                        _DragAction = DragActionEnum.Move;
                    }
                }
                _SelectedROIIndex = _HighlightedROIIndex;
                ROISelectionChanged?.Invoke(this, _SelectedROIIndex);
            }
            else if (e.Button == MouseButtons.Middle && _Frame != null)
            {
                _DragAction = DragActionEnum.Pan;
            }
        }

        private void ROISelector_MouseWheel(object sender, MouseEventArgs e)
        {
            double zoom = Math.Pow(1.05, e.Delta / SystemInformation.MouseWheelScrollDelta);

            _Scale *= (float)zoom;
            this.Refresh();
        }

        private void ROISelector_Resize(object sender, EventArgs e)
        {
            CalculateScale();
            foreach (var roi in _ROIs)
            {
                roi.UpdateGrabBoxes(_Scale);
            }
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            using (var bmp = new Bitmap(this.Width, this.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.ScaleTransform(_Scale, _Scale);
                    g.TranslateTransform(_Offset.X, _Offset.Y);

                    if (_Frame != null)
                    {
                        g.DrawImage(_Frame, new PointF(0,0));
                    }

                    for (int i = 0; i < _ROIs.Count; i++)
                    {
                        if (i == SelectedROIIndex)
                        {
                            _ROIs[i].Draw(g, false, Color.Red);
                        }
                        else if (i == _HighlightedROIIndex)
                        {
                            _ROIs[i].Draw(g, false, Color.DarkOrange);
                        }
                        else
                        {
                            _ROIs[i].Draw(g, false, Color.Orange);
                        }
                    }
                }
                pe.Graphics.DrawImage(bmp, 0, 0);
            }
        }

        #region "Helper Functions"
        public void RemoveROIByIndex(int index)
        {
            _ROIs.RemoveAt(index);
            this.Refresh();
        }
        private void CalculateScale()
        {
            if (_Frame != null)
            {
                var hscale = (float)this.Width / (float)_Frame.Width;
                var vscale = (float)this.Height / (float)_Frame.Height;

                _Scale = Math.Min(hscale, vscale);
            }
            else
            {
                _Scale = 1;
            }
        }

        private PointF TransformPoint(Point p)
        {
            return new PointF(((float)(p.X) / _Scale) - _Offset.X, ((float)(p.Y) / _Scale) - _Offset.Y);
        }

        #endregion

        private enum DragActionEnum
        {
            None,
            DrawNew,
            Resize,
            Move,
            Pan
        }
    }

    public class ROIObject
    {
        private RectangleF _ROI;
        private RectangleF[] _GrabBoxes = new RectangleF[8];
        private RectangleF[] _Border = new RectangleF[4];
        private const int GRAB_WIDTH = 3;
        public ROIObject(RectangleF roi, float scale)
        {
            SetRectangle(roi, scale);
        }
        public RectangleF GetROI()
        {
            return _ROI;
        }

        public void SetRectangle(RectangleF roi, float scale)
        {
            _ROI = roi;
            UpdateGrabBoxes(scale);
        }
        public void UpdateGrabBoxes(float scale)
        {
            _GrabBoxes[(int)GrabBoxLocation.TopLeft] = CalcGrabBoxAtPoint(_ROI.Location.X, _ROI.Location.Y, scale);
            _GrabBoxes[(int)GrabBoxLocation.TopRight] = CalcGrabBoxAtPoint(_ROI.Location.X + _ROI.Width, _ROI.Location.Y, scale);
            _GrabBoxes[(int)GrabBoxLocation.BottomRight] = CalcGrabBoxAtPoint(_ROI.Location.X + _ROI.Width, _ROI.Location.Y + _ROI.Height, scale);
            _GrabBoxes[(int)GrabBoxLocation.BottomLeft] = CalcGrabBoxAtPoint(_ROI.Location.X, _ROI.Location.Y + _ROI.Height, scale);

            _GrabBoxes[(int)GrabBoxLocation.TopMiddle] = CalcGrabBoxAtPoint(_ROI.Location.X + _ROI.Width / 2, _ROI.Location.Y, scale);
            _GrabBoxes[(int)GrabBoxLocation.RightMiddle] = CalcGrabBoxAtPoint(_ROI.Location.X + _ROI.Width, _ROI.Location.Y + _ROI.Height / 2, scale);
            _GrabBoxes[(int)GrabBoxLocation.BottomMiddle] = CalcGrabBoxAtPoint(_ROI.Location.X + _ROI.Width / 2, _ROI.Location.Y + _ROI.Height, scale);
            _GrabBoxes[(int)GrabBoxLocation.LeftMiddle] = CalcGrabBoxAtPoint(_ROI.Location.X, _ROI.Location.Y + _ROI.Height / 2, scale);

            _Border[0] = new RectangleF(_ROI.X - GRAB_WIDTH, _ROI.Y - GRAB_WIDTH, _ROI.Width + 2 * GRAB_WIDTH, 2 * GRAB_WIDTH);           // Top Edge
            _Border[1] = new RectangleF(_ROI.X - GRAB_WIDTH, _ROI.Bottom - GRAB_WIDTH, _ROI.Width + 2 * GRAB_WIDTH, 2 * GRAB_WIDTH);      // Bottom Edge
            _Border[2] = new RectangleF(_ROI.X - GRAB_WIDTH, _ROI.Y - GRAB_WIDTH, 2 * GRAB_WIDTH, _ROI.Height + 2 * GRAB_WIDTH);                        // Left Edge
            _Border[3] = new RectangleF(_ROI.Right - GRAB_WIDTH, _ROI.Y - GRAB_WIDTH, 2 * GRAB_WIDTH, _ROI.Height + 2 * GRAB_WIDTH);                    // Right Edge
        }

        public RectangleF[] GetGrabBoxes()
        {
            return _GrabBoxes;
        }

        public GrabBoxLocation TestMouseOverGrabBox(PointF p)
        {
            for (int i = 0; i < 8; i++)
            {
                if (_GrabBoxes[i].Contains(p))
                {
                    return (GrabBoxLocation)i;
                }
            }

            // If none found
            return GrabBoxLocation.None;
        }

        public bool BorderContainsPoint(PointF pt)
        {
            bool contains = false;

            for (int i = 0; i < 4; i++)
            {
                if (_Border[i].Contains(pt))
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        public void Draw(Graphics g, bool selected, Color c)
        {
            var b = new SolidBrush(c);

            g.DrawRectangle(new Pen(c, 1), _ROI.X, _ROI.Y, _ROI.Width, _ROI.Height);
            g.FillRectangles(new SolidBrush(c), _GrabBoxes);
        }

        private RectangleF CalcGrabBoxAtPoint(float x, float y, float scale)
        {
            RectangleF r = new RectangleF((x - (GRAB_WIDTH / scale)),
                                          (y - (GRAB_WIDTH / scale)),
                                          (GRAB_WIDTH * 2 / scale),
                                          (GRAB_WIDTH * 2 / scale));

            return r;
        }


    }

    public enum GrabBoxLocation
    {
        None = -1,
        TopLeft = 0,
        TopRight = 1,
        BottomRight = 2,
        BottomLeft = 3,
        TopMiddle = 4,
        RightMiddle = 5,
        BottomMiddle = 6,
        LeftMiddle = 7
    }
}
