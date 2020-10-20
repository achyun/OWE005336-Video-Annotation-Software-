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
    public partial class LabelButton : Label
    {
        public LabelButton()
        {
            InitializeComponent();
            this.MouseEnter += LabelButton_MouseEnter;
            this.MouseLeave += LabelButton_MouseLeave;
            this.Font = new Font("Calibri", 16, FontStyle.Bold);
            this.ForeColor = Color.White;
            this.BackColor = Color.Transparent;
        }

        private void LabelButton_MouseLeave(object sender, EventArgs e)
        {
            this.ForeColor = Color.White;
        }

        private void LabelButton_MouseEnter(object sender, EventArgs e)
        {
            this.ForeColor = Color.LightGray;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
