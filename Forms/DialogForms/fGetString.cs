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
    public partial class fGetString : Form
    {
        public fGetString(string stringDescription, string currentString = "")
        {
            InitializeComponent();
            lblName.Text = stringDescription;
            this.Text = stringDescription;
            if (currentString != "") { txtString.Text = currentString; }
            txtString.Focus();
        }

        public string ReturnString
        {
            get { return txtString.Text; }
        }

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
