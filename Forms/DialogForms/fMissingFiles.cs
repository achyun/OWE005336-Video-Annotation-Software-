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
    public partial class fMissingFiles : Form
    {
        public fMissingFiles(List<string> missingFiles)
        {
            InitializeComponent();
            ltbMissingFiles.Items.AddRange(missingFiles.ToArray());
        }
    }
}
