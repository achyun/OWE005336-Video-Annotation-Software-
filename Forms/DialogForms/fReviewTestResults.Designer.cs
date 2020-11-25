namespace OWE005336__Video_Annotation_Software_
{
    partial class fReviewTestResults
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
            this.components = new System.ComponentModel.Container();
            this.imgFrames = new System.Windows.Forms.ImageList(this.components);
            this.stsStatus = new System.Windows.Forms.StatusStrip();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvImages = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabelCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxConf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinConf = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roiSelector1 = new OWE005336__Video_Annotation_Software_.ROISelector();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).BeginInit();
            this.SuspendLayout();
            // 
            // imgFrames
            // 
            this.imgFrames.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgFrames.ImageSize = new System.Drawing.Size(16, 16);
            this.imgFrames.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // stsStatus
            // 
            this.stsStatus.Location = new System.Drawing.Point(0, 729);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(1467, 22);
            this.stsStatus.TabIndex = 2;
            this.stsStatus.Text = "statusStrip1";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1121, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 729);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvImages);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1125, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 729);
            this.panel1.TabIndex = 5;
            // 
            // dgvImages
            // 
            this.dgvImages.AllowUserToDeleteRows = false;
            this.dgvImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LabelCount,
            this.MaxConf,
            this.MinConf,
            this.FilePath});
            this.dgvImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImages.Location = new System.Drawing.Point(0, 0);
            this.dgvImages.Name = "dgvImages";
            this.dgvImages.Size = new System.Drawing.Size(342, 623);
            this.dgvImages.TabIndex = 4;
            this.dgvImages.SelectionChanged += new System.EventHandler(this.dgvImages_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 623);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(342, 106);
            this.panel2.TabIndex = 5;
            // 
            // LabelCount
            // 
            this.LabelCount.HeaderText = "Label Count";
            this.LabelCount.Name = "LabelCount";
            // 
            // MaxConf
            // 
            this.MaxConf.HeaderText = "Max Conf";
            this.MaxConf.Name = "MaxConf";
            // 
            // MinConf
            // 
            this.MinConf.HeaderText = "Min Conf";
            this.MinConf.Name = "MinConf";
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "File Path";
            this.FilePath.Name = "FilePath";
            // 
            // roiSelector1
            // 
            this.roiSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roiSelector1.Location = new System.Drawing.Point(0, 0);
            this.roiSelector1.Name = "roiSelector1";
            this.roiSelector1.SelectedROIIndex = -1;
            this.roiSelector1.Size = new System.Drawing.Size(1121, 729);
            this.roiSelector1.TabIndex = 1;
            this.roiSelector1.Text = "roiSelector1";
            // 
            // fReviewTestResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 751);
            this.Controls.Add(this.roiSelector1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stsStatus);
            this.Name = "fReviewTestResults";
            this.Text = "Review Test Results";
            this.Load += new System.EventHandler(this.fReviewTestResults_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ROISelector roiSelector1;
        private System.Windows.Forms.ImageList imgFrames;
        private System.Windows.Forms.StatusStrip stsStatus;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvImages;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LabelCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxConf;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinConf;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
    }
}