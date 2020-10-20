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
    public partial class fProgressBar : Form
    {
        public delegate void TaskCancelledEventHandler(fProgressBar sender, EventArgs e);
        public event TaskCancelledEventHandler TaskCancelled;

        public bool Cancelled { get; private set; } = false;
        public fProgressBar(string caption, string message)
        {
            InitializeComponent();
            this.Text = caption;
            lblProgressMessage.Text = message;
        }

        public void UpdateProgress(float percentComplete, string message)
        {
            lblProgressMessage.Text = message;
            pgbProgress.Value = (int)(pgbProgress.Maximum * Math.Max(0, Math.Min(percentComplete, 100)) / 100);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            TaskCancelled?.Invoke(this, new EventArgs());
        }
    }
}
