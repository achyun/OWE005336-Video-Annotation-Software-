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
    public partial class fLabelSelector : Form
    {
        public fLabelSelector()
        {
            InitializeComponent();
            trvLabels.PopulateLabels();
            trvLabels.AfterSelect += trvLabels_AfterSelect;
            trvLabels.DoubleClick += trvLabels_DoubleClick;
        }

        private void trvLabels_DoubleClick(object sender, EventArgs e)
        {
            btnSave_Click(sender, e);
        }

        private void trvLabels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvLabels.SelectedNode != null)
            {
                SelectedLabel = (LabelNode)trvLabels.SelectedNode.Tag;
            }
            else
            {
                SelectedLabel = null;
            }
        }

        public LabelNode SelectedLabel { get; private set; } = null;

        private void btnSave_Click(object sender, EventArgs e)
        {
            int parentID = -1;

            if (trvLabels.SelectedNode != null)
            {
                SelectedLabel = (LabelNode)trvLabels.SelectedNode.Tag;
                parentID = SelectedLabel.ParentID;
            }
            else
            {
                SelectedLabel = null;
            }

            if (parentID > -1)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select a valid label", "Invalid Label", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
