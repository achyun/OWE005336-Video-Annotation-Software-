namespace OWE005336__Video_Annotation_Software_
{
    partial class TagTextBox
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
            this.txtTextBox = new System.Windows.Forms.TextBox();
            this.pcbClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbClose)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTextBox
            // 
            this.txtTextBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtTextBox.Location = new System.Drawing.Point(0, 0);
            this.txtTextBox.Name = "txtTextBox";
            this.txtTextBox.Size = new System.Drawing.Size(80, 20);
            this.txtTextBox.TabIndex = 0;
            // 
            // pcbClose
            // 
            this.pcbClose.BackgroundImage = global::OWE005336__Video_Annotation_Software_.Properties.Resources.Close_box__18x18_;
            this.pcbClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcbClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.pcbClose.Location = new System.Drawing.Point(80, 0);
            this.pcbClose.Name = "pcbClose";
            this.pcbClose.Size = new System.Drawing.Size(20, 20);
            this.pcbClose.TabIndex = 1;
            this.pcbClose.TabStop = false;
            this.pcbClose.Click += new System.EventHandler(this.pcbClose_Click);
            // 
            // TagTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pcbClose);
            this.Controls.Add(this.txtTextBox);
            this.Name = "TagTextBox";
            this.Size = new System.Drawing.Size(100, 20);
            ((System.ComponentModel.ISupportInitialize)(this.pcbClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTextBox;
        private System.Windows.Forms.PictureBox pcbClose;
    }
}
