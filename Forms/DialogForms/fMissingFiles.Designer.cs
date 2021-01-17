namespace OWE005336__Video_Annotation_Software_
{
    partial class fMissingFiles
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ltbMissingFiles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ltbMissingFiles
            // 
            this.ltbMissingFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltbMissingFiles.FormattingEnabled = true;
            this.ltbMissingFiles.Location = new System.Drawing.Point(0, 0);
            this.ltbMissingFiles.Name = "ltbMissingFiles";
            this.ltbMissingFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ltbMissingFiles.Size = new System.Drawing.Size(377, 564);
            this.ltbMissingFiles.TabIndex = 0;
            // 
            // fMissingFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 564);
            this.Controls.Add(this.ltbMissingFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "fMissingFiles";
            this.Text = "Missing Files";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ltbMissingFiles;
    }
}