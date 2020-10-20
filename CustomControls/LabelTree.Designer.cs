namespace OWE005336__Video_Annotation_Software_
{
    partial class LabelTree
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelTree));
            this.imgLabels = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imgLabels
            // 
            this.imgLabels.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLabels.ImageStream")));
            this.imgLabels.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLabels.Images.SetKeyName(0, "folder_directory_open-512 (16x16).png");
            this.imgLabels.Images.SetKeyName(1, "Label (Black) (16x16).png");
            this.imgLabels.Images.SetKeyName(2, "Label (White) (16x16).png");
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgLabels;
    }
}
