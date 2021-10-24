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
    public partial class fProjectSelector : Form
    {
        public fProjectSelector()
        {
            InitializeComponent();
            cmbSelectProject.DataSource = Program.ImageDatabase.Projects;
            cmbSelectProject.Focus();
        }

        public Project SelectedProject => cmbSelectProject.SelectedItem as Project;

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
