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
            _AllowableTags = new AutoCompleteStringCollection();
            _AllowableTags.AddRange(Program.ImageDatabase.Tags_Load());
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
}
