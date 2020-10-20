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
    public partial class TagBox : FlowLayoutPanel
    {
        public delegate void TagsChangedEventHandler(TagBox sender, EventArgs e);
        public event TagsChangedEventHandler TagsChanged;

        private List<TagTextBox> TextBoxes = new List<TagTextBox>();
        private AutoCompleteStringCollection _AllowableTags;
        public TagBox()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;
            this.Click += TagBox_Click;
            this.MouseMove += TagBox_MouseMove;
            _AllowableTags = Program.ImageDatabase.Tags_Load();
        }

        public void PopulateTagsFromString(string tagString)
        {
            this.Controls.Clear();
            TextBoxes.Clear();

            if (tagString != null)
            {
                string[] tags = tagString.Split('#');
                
                if (tags.Length > 1)
                {
                    for (int i = 1; i < tags.Length; i++)
                    {
                        TagTextBox ttb = new TagTextBox(tags[i]);
                        TextBoxes.Add(ttb);
                        this.Controls.Add(ttb);
                        ttb.TagDeleted += Ttb_Deleted;
                    }
                }
            }
            
        }

        private void TagBox_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.IBeam;
        }

        private void TagBox_Click(object sender, EventArgs e)
        {
            TagTextBox ttb = new TagTextBox(_AllowableTags);
            TextBoxes.Add(ttb);
            this.Controls.Add(ttb);
            ttb.Show();
            ttb.TagCancelled += Ttb_TagCancelled;
            ttb.TagDeleted += Ttb_Deleted;
            ttb.TagCommitted += Ttb_TagCommitted;
            ttb.Focus();
        }

        private void Ttb_TagCancelled(TagTextBox sender, EventArgs e)
        {
            TextBoxes.Remove(sender);
            this.Controls.Remove(sender);
            sender.Dispose();
        }

        private void Ttb_TagCommitted(TagTextBox sender, TagTextBoxCommittedArgs e)
        {
            if (e.TagNeedsAddingToDatabase)
            {
                Program.ImageDatabase.Tags_Add(sender.Text);
            }
            TagsChanged?.Invoke(this, new EventArgs());
            this.Focus();
        }

        private void Ttb_Deleted(TagTextBox sender, EventArgs e)
        {
            TextBoxes.Remove(sender);
            this.Controls.Remove(sender);
            TagsChanged?.Invoke(this, new EventArgs());
            sender.Dispose();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public override string ToString()
        {
            string str = "";

            foreach (TagTextBox ttb in TextBoxes)
            {
                str += "#" + ttb.Text;
            }

            return str;
        }
    }

    //public partial class TagTextBox : Control
    //{
    //    public delegate void DeletedEventHandler(TagTextBox sender, EventArgs e);
    //    public event DeletedEventHandler Deleted;

    //    const int MIN_WIDTH = 50;
    //    public TagTextBox()
    //    {
    //        this.Controls.Add()
    //        this.SetStyle(ControlStyles.UserPaint, true);
    //        this.BorderStyle = BorderStyle.FixedSingle;
    //        this.Margin = new Padding(5);
    //        this.Width = MIN_WIDTH;
    //        this.OnTextChanged += TagTextBox_OnTextChanged;
    //        this.KeyPress += TagTextBox_KeyPress;
    //        this.MouseDown += TagTextBox_MouseDown;
    //        this.MouseMove += TagTextBox_MouseMove;
            
    //    }

    //    private void TagTextBox_MouseMove(object sender, MouseEventArgs e)
    //    {
    //        if (e.X > this.Width - this.Height)
    //        {
    //            Cursor.Current = Cursors.Hand;
    //        }
    //        else
    //        {
    //            Cursor.Current = Cursors.IBeam;
    //        }
    //    }

    //    private void TagTextBox_MouseDown(object sender, MouseEventArgs e)
    //    {
    //        if (e.X > this.Width - this.Height)
    //        {
    //            Deleted?.Invoke(this, new EventArgs());
    //        }
    //    }

    //    private void TagTextBox_OnTextChanged(object sender, KeyPressEventArgs e)
    //    {

    //    }

    //    private void TagTextBox_KeyPress(object sender, KeyPressEventArgs e)
    //    {
    //        if (this.Text != null)
    //        {
    //            Bitmap b = new Bitmap(100, 100);
    //            Graphics g = Graphics.FromImage(b);
    //            SizeF strSize = g.MeasureString(this.Text, this.Font);
    //            this.Width = (int)(strSize.Width + this.Height);
    //        }
    //        else
    //        {
    //            this.Width = MIN_WIDTH;
    //        }
            
            
    //    }

    //    protected override void OnPaint(PaintEventArgs pe)
    //    {
    //        base.OnPaint(pe);

    //        Graphics g = pe.Graphics;
    //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

    //        //g.FillRectangle(new SolidBrush(Color.White), 0, 0, this.Width, this.Height);
    //        //g.DrawString(this.Text, this.Font, new SolidBrush(Color.Black), 0, 0);
            
            
    //        g.TranslateTransform(this.Width - this.Height, 0);
    //        DrawCross(g);
    //    }

    //    private void DrawCross(Graphics g)
    //    {
    //        SolidBrush b = new SolidBrush(Color.LightGray);
    //        Pen p = new Pen(Color.White);
    //        int h = (int)(this.Height * 0.8);
    //        int offset = (int)((this.Height - h) / 2);
    //        Rectangle r = new Rectangle(offset, offset, h, h);
    //        p.Width = 2;

    //        g.FillEllipse(b, r);
    //        g.DrawLine(p, r.Left, r.Top, r.Right, r.Bottom);
    //        g.DrawLine(p, r.Right, r.Top, r.Left, r.Bottom);
    //    }
    //}
}
