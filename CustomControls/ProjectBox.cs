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
    public partial class ProjectBox : FlowLayoutPanel
    {
        public event EventHandler ProjectsChanged;
        public event Action<ProjectBox, EventArgs, int> ProjectAdded;
        public event Action<ProjectBox, EventArgs, int> ProjectDeleted;

        private List<TagTextBox> TextBoxes = new List<TagTextBox>();
        private AutoCompleteStringCollection _AllowableProjects;
        public ProjectBox()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;
            this.Click += TagBox_Click;
            this.MouseMove += TagBox_MouseMove;
            _AllowableProjects = new AutoCompleteStringCollection();
            _AllowableProjects.AddRange(Program.ImageDatabase.Projects.Select(x => x.Name).ToArray());
        }

        public Project[] SelectedProjects {
            get
            {
                return this.Controls.OfType<TagTextBox>().Select(x => x.Tag as Project).ToArray();
            }
        }

        public void SetProjects(Project[] projects)
        {
            ClearProjects();

            foreach (var project in projects)
            {
                TagTextBox ttb = new TagTextBox(project);
                TextBoxes.Add(ttb);
                this.Controls.Add(ttb);
                ttb.TagDeleted += Ttb_Deleted;
            }
        }

        public void ClearProjects()
        {
            this.Controls.Clear();
            TextBoxes.Clear();
            ProjectsChanged?.Invoke(this, new EventArgs());
        }

        private void TagBox_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void TagBox_Click(object sender, EventArgs e)
        {
            using (fProjectSelector projectSelector = new fProjectSelector())
            {
                if (projectSelector.ShowDialog() == DialogResult.OK)
                {
                    Project l = projectSelector.SelectedProject;
                    TagTextBox ttb = new TagTextBox(l);
                    TextBoxes.Add(ttb);
                    this.Controls.Add(ttb);
                    ttb.TagDeleted += Ttb_Deleted;
                    ProjectsChanged?.Invoke(this, new EventArgs());
                    ProjectAdded?.Invoke(this, new EventArgs(), l.ID);
                }
            }
        }

        private void Ttb_Deleted(TagTextBox sender, EventArgs e)
        {
            TextBoxes.Remove(sender);
            this.Controls.Remove(sender);
            Project prj = (Project)(sender.Tag);
            ProjectsChanged?.Invoke(this, new EventArgs());
            ProjectDeleted?.Invoke(this, new EventArgs(), prj.ID);
            sender.Dispose();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
