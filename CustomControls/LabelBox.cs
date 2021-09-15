using LabellingDB;
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
    public partial class LabelBox : FlowLayoutPanel
    {
        public delegate void LabelsChangedEventHandler(LabelBox sender, EventArgs e);
        public event LabelsChangedEventHandler LabelsChanged;

        private List<TagTextBox> TextBoxes = new List<TagTextBox>();
        private AutoCompleteStringCollection _AllowableTags;
        public LabelBox()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;
            this.Click += TagBox_Click;
            this.MouseMove += TagBox_MouseMove;
            _AllowableTags = new AutoCompleteStringCollection();
            _AllowableTags.AddRange(Program.ImageDatabase.Tags_Load());
        }

        public LabelNode[] SelectedLabels {
            get
            {
                return this.Controls.OfType<TagTextBox>().Select(x => x.Tag as LabelNode).ToArray();
            }
        }

        public void SetLabels(LabelNode[] labels)
        {
            this.Controls.Clear();
            TextBoxes.Clear();

            foreach (var label in labels)
            {
                TagTextBox ttb = new TagTextBox(label);
                TextBoxes.Add(ttb);
                this.Controls.Add(ttb);
                ttb.TagDeleted += Ttb_Deleted;
            }
        }

        private void TagBox_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.IBeam;
        }

        private void TagBox_Click(object sender, EventArgs e)
        {
            using (fLabelSelector labelSelector = new fLabelSelector())
            {
                if (labelSelector.ShowDialog() == DialogResult.OK)
                {
                    LabelNode l = labelSelector.SelectedLabel;
                    TagTextBox ttb = new TagTextBox(l);
                    TextBoxes.Add(ttb);
                    this.Controls.Add(ttb);
                    ttb.TagDeleted += Ttb_Deleted;
                    LabelsChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        private void Ttb_Deleted(TagTextBox sender, EventArgs e)
        {
            TextBoxes.Remove(sender);
            this.Controls.Remove(sender);
            LabelsChanged?.Invoke(this, new EventArgs());
            sender.Dispose();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
