using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabellingDB;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class TagTextBox : UserControl
    {
        private bool _Disposing = false;
        public delegate void TagCancelledEventHandler(TagTextBox sender, EventArgs e);
        public delegate void TagDeletedEventHandler(TagTextBox sender, EventArgs e);
        public delegate void TagCommitedEventHandler(TagTextBox sender, TagTextBoxCommittedArgs e);
        public event TagCancelledEventHandler TagCancelled;
        public event TagDeletedEventHandler TagDeleted;
        public event TagCommitedEventHandler TagCommitted;

        public AutoCompleteStringCollection AutoCompleteStrings
        {
            get { return txtTextBox.AutoCompleteCustomSource; }
            set { txtTextBox.AutoCompleteCustomSource = value; }
        }

        public override string Text
        {
            get { return txtTextBox.Text; }
        }

        public TagTextBox(AutoCompleteStringCollection autoCompleteStringCollection)
        {
            InitializeComponent();
            this.Height = txtTextBox.Height;
            txtTextBox.CharacterCasing = CharacterCasing.Lower;
            txtTextBox.TextChanged += TxtTextBox_TextChanged;
            txtTextBox.LostFocus += TxtTextBox_LostFocus;
            txtTextBox.KeyDown += TxtTextBox_KeyDown;
            txtTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTextBox.AutoCompleteCustomSource = autoCompleteStringCollection;
        }

        public TagTextBox(string tag)
        {
            InitializeComponent();
            this.Height = txtTextBox.Height;
            txtTextBox.Text = tag;
            txtTextBox.ReadOnly = true;
        }

        public TagTextBox(LabelNode label)
        {
            InitializeComponent();
            this.Tag = label;
            this.Height = txtTextBox.Height;
            txtTextBox.Text = label.Name;
            txtTextBox.ReadOnly = true;
        }

        public TagTextBox(Project project)
        {
            InitializeComponent();
            this.Tag = project;
            this.Height = txtTextBox.Height;
            txtTextBox.Text = project.Name;
            txtTextBox.ReadOnly = true;
        }

        private void TxtTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SaveTag();
            }
            else if (e.KeyData == Keys.Escape)
            {
                _Disposing = true;
                TagDeleted?.Invoke(this, new EventArgs());
            }
        }

        private void TxtTextBox_LostFocus(object sender, EventArgs e)
        {
            SaveTag();
        }

        private void TxtTextBox_TextChanged(object sender, EventArgs e)
        {
            if (txtTextBox.Text != null)
            {
                Bitmap b = new Bitmap(100, 100);
                Graphics g = Graphics.FromImage(b);
                SizeF strSize = g.MeasureString(txtTextBox.Text, txtTextBox.Font);
                txtTextBox.Width = (int)(strSize.Width + 80);
                this.Width = txtTextBox.Width + pcbClose.Width;
            }
            else
            {
                this.Width = 20;
            }
        }

        private void pcbClose_Click(object sender, EventArgs e)
        {
            _Disposing = true;
            TagDeleted?.Invoke(this, new EventArgs());
        }

        private void SaveTag()
        {
            if (!_Disposing)
            {
                _Disposing = true;
                if (txtTextBox.AutoCompleteCustomSource.Contains(txtTextBox.Text))
                {
                    txtTextBox.ReadOnly = true;
                    TagCommitted?.Invoke(this, new TagTextBoxCommittedArgs(false));
                }
                else if (txtTextBox.Text == "")
                {
                    TagCancelled?.Invoke(this, new EventArgs());
                }
                else
                {
                    if (MessageBox.Show("Tag not found\r\n\r\nAre you sure you want to add this as a new tag?", "Tag not found", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        txtTextBox.ReadOnly = true;
                        TagCommitted?.Invoke(this, new TagTextBoxCommittedArgs(true));
                    }
                    else
                    {
                        TagCancelled?.Invoke(this, new EventArgs());
                    }
                }

                txtTextBox.SelectionLength = 0;
                txtTextBox.SelectionStart = 0;
                
            }
        }
    }

    public class TagTextBoxCommittedArgs
    {
        public bool TagNeedsAddingToDatabase { get; set; }

        public TagTextBoxCommittedArgs(bool tagNeedsAddingToDatabase)
        {
            TagNeedsAddingToDatabase = tagNeedsAddingToDatabase;
        }
    }
}
