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
        PointF _OldMouseLocation_pixels;
        List<ROIObject> _ROIs = new List<ROIObject>();
        DragActionEnum _DragAction = DragActionEnum.None;
        //ROIObject _CurrentROI = null;
        GrabBoxLocation _CurrentGrabBox = GrabBoxLocation.None;
        int _HighlightedROIIndex = -1;
        int _SelectedROIIndex = -1;
        bool _DisplayCrosshairs = false;

        public delegate void NewROIAddedEventHandler(ROISelector sender, Rectangle roi);
        public delegate void ROIChangedEventHandler(ROISelector sender, int index, Rectangle roi);
        public delegate void ROIDeletedEventHandler(ROISelector sender, int index);
        public delegate void ROISelectionChangedEventHandler(ROISelector sender, int index);
        public delegate void ShortcutKeyPressedEventHandler(ROISelector sender, int keyValue);
        public event NewROIAddedEventHandler NewROIAdded;
        public event ROIChangedEventHandler ROIChanged;
        public event ROIDeletedEventHandler ROIDeleted;
        public event ROISelectionChangedEventHandler ROISelectionChanged;
        public event ShortcutKeyPressedEventHandler ShortcutKeyPressed;

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

        public void LinkToLabelledImage(List<ROIObject> rois, Bitmap frame)
        {
            _Frame = frame;

            _ROIs = rois;

            _Offset = new PointF(0, 0);
            CalculateScale();         

            foreach (ROIObject r in _ROIs)
            {
                r.UpdateGrabBoxes(_Scale);
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
            this.MouseEnter += ROISelector_MouseEnter;
            this.MouseLeave += ROISelector_MouseLeave;
            this.DoubleBuffered = true;
        }

        private void ROISelector_MouseEnter(object sender, EventArgs e)
        {
            _DisplayCrosshairs = true;
        }

        private void ROISelector_MouseLeave(object sender, EventArgs e)
        {
            _DisplayCrosshairs = false;
            this.Refresh();
        }

        private void ROISelector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (_DragAction == DragActionEnum.DrawNew)
                {
                    _DragAction = DragActionEnum.None;
                    _ROIs.RemoveAt(_ROIs.Count - 1);
                    this.Refresh();
                }
                else if (SelectedROIIndex > -1)
                {
                    SelectedROIIndex = -1;
                }
                
            }
            else if (SelectedROIIndex >= 0)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    _ROIs.RemoveAt(SelectedROIIndex);
                    ROIDeleted?.Invoke(this, SelectedROIIndex);
                    SelectedROIIndex = -1;
                }
                else if (e.KeyValue >= 0x30 && e.KeyValue <= 0x39)
                {
                    ShortcutKeyPressed(this, e.KeyValue);
                }
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
            PointF p = TransformPointToPixel(new Point(e.X, e.Y));
            var currentROI = (_HighlightedROIIndex >= 0 ? _ROIs[_HighlightedROIIndex] : null);

            switch (_DragAction)
            {
                case DragActionEnum.Pan:
                    _Offset.X -= (float)(_OldMouseLocationRaw.X - e.Location.X) / _Scale;
                    _Offset.Y -= (float)(_OldMouseLocationRaw.Y - e.Location.Y) / _Scale;

                    _OldMouseLocationRaw.X = e.Location.X;
                    _OldMouseLocationRaw.Y = e.Location.Y;

                    this.Refresh();
                    break;
                case DragActionEnum.DrawNew:
                    {
                        float x, y, w, h;

                        if (p.X > _OldMouseLocation_pixels.X)
                        {
                            x = _OldMouseLocation_pixels.X;
                            w = p.X - _OldMouseLocation_pixels.X;
                        }
                        else
                        {
                            x = p.X;
                            w = _OldMouseLocation_pixels.X - p.X;
                        }

                        if (p.Y > _OldMouseLocation_pixels.Y)
                        {
                            y = _OldMouseLocation_pixels.Y;
                            h = p.Y - _OldMouseLocation_pixels.Y;
                        }
                        else
                        {
                            y = p.Y;
                            h = _OldMouseLocation_pixels.Y - p.Y;
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
                        float x = r.Left - (_OldMouseLocation_pixels.X - p.X);
                        float y = r.Top - (_OldMouseLocation_pixels.Y - p.Y);

                        _OldMouseLocation_pixels.X = p.X;
                        _OldMouseLocation_pixels.Y = p.Y;

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

                        //if (oldHighlightedROIIndex != _HighlightedROIIndex) { this.Refresh(); }

                        break;
                    }
                    
            }
            this.Refresh();
        }

        private void ROISelector_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            _OldMouseLocation_pixels = TransformPointToPixel(new Point(e.X, e.Y));
            _OldMouseLocationRaw = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Left && _Frame != null)
            {
                if (_HighlightedROIIndex < 0)
                {
                    ROIObject newROI = new ROIObject(new RectangleF(e.X, e.Y, 6, 6), _Scale, "");
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

            float oldScale = _Scale;
            _Scale *= (float)zoom;

            PointF p_img = TransformPointToPixel(e.Location);

            _Offset.X = e.Location.X / _Scale - e.Location.X / oldScale + _Offset.X;
            _Offset.Y = e.Location.Y / _Scale - e.Location.Y / oldScale + _Offset.Y;

            foreach (var roi in _ROIs)
            {
                roi.UpdateGrabBoxes(_Scale);
            }

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
                        g.DrawImage(_Frame, new PointF(0, 0));
                        if (SelectedROIIndex > -1 && (_HighlightedROIIndex == -1 || _HighlightedROIIndex == SelectedROIIndex))
                        {
                            SolidBrush b_transparent = new SolidBrush(Color.FromArgb(100, Color.Gray));
                            var r = _ROIs[SelectedROIIndex].GetROI();
                            g.FillRectangle(b_transparent, 0, 0, r.Left, _Frame.Height);
                            g.FillRectangle(b_transparent, r.Right, 0, _Frame.Width - r.Right, _Frame.Height);
                            g.FillRectangle(b_transparent, r.Left, 0, r.Width, r.Top);
                            g.FillRectangle(b_transparent, r.Left, r.Bottom, r.Width, _Frame.Height - r.Bottom);
                        }
                    }

                    for (int i = 0; i < _ROIs.Count; i++)
                    {
                        if (i == SelectedROIIndex)
                        {
                            _ROIs[i].Draw(g, false, Color.Red, _Scale);
                        }
                        else if (i == _HighlightedROIIndex && _HighlightedROIIndex != SelectedROIIndex)
                        {
                            _ROIs[i].Draw(g, true, Color.DarkOrange, _Scale);
                        }
                        else
                        {
                            _ROIs[i].Draw(g, false, Color.Orange, _Scale);
                        }
                    }
                }
                pe.Graphics.DrawImage(bmp, 0, 0);
            }

            if (_DisplayCrosshairs)
            {
                Pen pen = new Pen(Color.Gray);
                Point p = this.PointToClient(Control.MousePosition);
                pe.Graphics.DrawLine(pen, p.X, 0, p.X, this.Height);
                pe.Graphics.DrawLine(pen, 0, p.Y, this.Width, p.Y);
            }
            if (_Frame != null)
            {
                pe.Graphics.DrawString(_Frame.Width + "," + _Frame.Height, this.Font, new SolidBrush(Color.Black), new Point(0, 0));
            }
        }

        #region "Helper Functions"
        public void RemoveROIByIndex(int index)
        {
            _ROIs.RemoveAt(index);
            this.Refresh();
        }

        public void UpdateROIName(int index, string name)
        {
            _ROIs[index].Name = name;
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

        private PointF TransformPointToPixel(Point p)
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
        public string Name { get; set; }
        private float _DefaultFontSize = 10;
        private Font _Font = new Font("Calibri", 10);
        private const int GRAB_WIDTH = 3;
        public ROIObject(RectangleF roi, float scale, string name)
        {
            Name = name;
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

        public void Draw(Graphics g, bool higlighted, Color c, float scale)
        {
            var b = new SolidBrush(c);
            var p = new Pen(c, 1 / scale);
            
            if (higlighted)
            {
                SolidBrush b_transparent = new SolidBrush(Color.FromArgb(100, Color.Gray));
                g.FillRectangle(b_transparent, _ROI.X, _ROI.Y, _ROI.Width, _ROI.Height);
            }
            
            g.DrawRectangle(p, _ROI.X, _ROI.Y, _ROI.Width, _ROI.Height);
            g.FillRectangles(b, _GrabBoxes);
            

            if (Name !=  "")
            {
                var textBrush = new SolidBrush(Color.White);
                _Font = new Font(_Font.FontFamily, _DefaultFontSize / scale);
                SizeF textBox = g.MeasureString(Name, _Font);
                
                g.FillRectangle(b, _ROI.X, _ROI.Y - textBox.Height, textBox.Width, textBox.Height);
                g.DrawString(Name, _Font, textBrush, _ROI.X, _ROI.Y - textBox.Height);
            }
            
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
