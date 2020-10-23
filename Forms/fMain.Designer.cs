﻿namespace OWE005336__Video_Annotation_Software_
{
    partial class fMain
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mniFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mniOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tbcOptions = new System.Windows.Forms.TabControl();
            this.tbpImages = new System.Windows.Forms.TabPage();
            this.dgvImages = new System.Windows.Forms.DataGridView();
            this.Thumbnail = new System.Windows.Forms.DataGridViewImageColumn();
            this.Complete = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ckbFilterForNoLabels = new System.Windows.Forms.CheckBox();
            this.ckbFilterForIncomplete = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOpenImages = new System.Windows.Forms.Button();
            this.btnOpenVideo = new System.Windows.Forms.Button();
            this.tbpLabels = new System.Windows.Forms.TabPage();
            this.trvLabels = new OWE005336__Video_Annotation_Software_.LabelTree();
            this.tbpSettings = new System.Windows.Forms.TabPage();
            this.btnSelectProcessedFileArchiveDir = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProcessedFilesDir = new System.Windows.Forms.TextBox();
            this.btnSetVideoArchiveDir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVideoArchiveDir = new System.Windows.Forms.TextBox();
            this.btnSetImageDir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImageDir = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.stsStatus = new System.Windows.Forms.StatusStrip();
            this.lbiImage = new OWE005336__Video_Annotation_Software_.LabellingInterface();
            this.menuStrip1.SuspendLayout();
            this.tbcOptions.SuspendLayout();
            this.tbpImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tbpLabels.SuspendLayout();
            this.tbpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1209, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mniFile
            // 
            this.mniFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniOpen});
            this.mniFile.Name = "mniFile";
            this.mniFile.Size = new System.Drawing.Size(37, 20);
            this.mniFile.Text = "File";
            // 
            // mniOpen
            // 
            this.mniOpen.Name = "mniOpen";
            this.mniOpen.Size = new System.Drawing.Size(103, 22);
            this.mniOpen.Text = "Open";
            this.mniOpen.Click += new System.EventHandler(this.mniOpen_Click);
            // 
            // tbcOptions
            // 
            this.tbcOptions.Controls.Add(this.tbpImages);
            this.tbcOptions.Controls.Add(this.tbpLabels);
            this.tbcOptions.Controls.Add(this.tbpSettings);
            this.tbcOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbcOptions.Location = new System.Drawing.Point(0, 24);
            this.tbcOptions.Name = "tbcOptions";
            this.tbcOptions.SelectedIndex = 0;
            this.tbcOptions.Size = new System.Drawing.Size(282, 693);
            this.tbcOptions.TabIndex = 7;
            // 
            // tbpImages
            // 
            this.tbpImages.Controls.Add(this.dgvImages);
            this.tbpImages.Controls.Add(this.panel1);
            this.tbpImages.Controls.Add(this.panel2);
            this.tbpImages.Location = new System.Drawing.Point(4, 22);
            this.tbpImages.Name = "tbpImages";
            this.tbpImages.Padding = new System.Windows.Forms.Padding(3);
            this.tbpImages.Size = new System.Drawing.Size(274, 667);
            this.tbpImages.TabIndex = 1;
            this.tbpImages.Text = "Images";
            this.tbpImages.UseVisualStyleBackColor = true;
            // 
            // dgvImages
            // 
            this.dgvImages.AllowDrop = true;
            this.dgvImages.AllowUserToAddRows = false;
            this.dgvImages.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImages.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvImages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Thumbnail,
            this.Complete,
            this.FilePath});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvImages.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImages.Location = new System.Drawing.Point(3, 3);
            this.dgvImages.Name = "dgvImages";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImages.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvImages.RowHeadersVisible = false;
            this.dgvImages.RowTemplate.Height = 50;
            this.dgvImages.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvImages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImages.Size = new System.Drawing.Size(268, 554);
            this.dgvImages.TabIndex = 9;
            this.dgvImages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvImages_KeyDown);
            // 
            // Thumbnail
            // 
            this.Thumbnail.HeaderText = "Thumbnail";
            this.Thumbnail.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Thumbnail.Name = "Thumbnail";
            this.Thumbnail.ReadOnly = true;
            this.Thumbnail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Thumbnail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Complete
            // 
            this.Complete.HeaderText = "Complete";
            this.Complete.Name = "Complete";
            this.Complete.Width = 60;
            // 
            // FilePath
            // 
            this.FilePath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FilePath.HeaderText = "File Path";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckbFilterForNoLabels);
            this.panel1.Controls.Add(this.ckbFilterForIncomplete);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 557);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 50);
            this.panel1.TabIndex = 8;
            // 
            // ckbFilterForNoLabels
            // 
            this.ckbFilterForNoLabels.AutoSize = true;
            this.ckbFilterForNoLabels.Location = new System.Drawing.Point(5, 26);
            this.ckbFilterForNoLabels.Name = "ckbFilterForNoLabels";
            this.ckbFilterForNoLabels.Size = new System.Drawing.Size(70, 17);
            this.ckbFilterForNoLabels.TabIndex = 10;
            this.ckbFilterForNoLabels.Text = "No labels";
            this.ckbFilterForNoLabels.UseVisualStyleBackColor = true;
            this.ckbFilterForNoLabels.CheckedChanged += new System.EventHandler(this.ckbFilterForNoLabels_CheckedChanged);
            // 
            // ckbFilterForIncomplete
            // 
            this.ckbFilterForIncomplete.AutoSize = true;
            this.ckbFilterForIncomplete.Checked = true;
            this.ckbFilterForIncomplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFilterForIncomplete.Location = new System.Drawing.Point(5, 3);
            this.ckbFilterForIncomplete.Name = "ckbFilterForIncomplete";
            this.ckbFilterForIncomplete.Size = new System.Drawing.Size(141, 17);
            this.ckbFilterForIncomplete.TabIndex = 9;
            this.ckbFilterForIncomplete.Text = "Not marked as complete";
            this.ckbFilterForIncomplete.UseVisualStyleBackColor = true;
            this.ckbFilterForIncomplete.CheckedChanged += new System.EventHandler(this.ckbFilterForIncomplete_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnOpenImages);
            this.panel2.Controls.Add(this.btnOpenVideo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 607);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(268, 57);
            this.panel2.TabIndex = 10;
            // 
            // btnOpenImages
            // 
            this.btnOpenImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenImages.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources._1200px_OneDrive_Folder_Icon__32x32_;
            this.btnOpenImages.Location = new System.Drawing.Point(177, 11);
            this.btnOpenImages.Name = "btnOpenImages";
            this.btnOpenImages.Size = new System.Drawing.Size(41, 41);
            this.btnOpenImages.TabIndex = 1;
            this.btnOpenImages.UseVisualStyleBackColor = true;
            this.btnOpenImages.Click += new System.EventHandler(this.btnOpenImages_Click);
            // 
            // btnOpenVideo
            // 
            this.btnOpenVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenVideo.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.frame_512__32x32_;
            this.btnOpenVideo.Location = new System.Drawing.Point(224, 11);
            this.btnOpenVideo.Name = "btnOpenVideo";
            this.btnOpenVideo.Size = new System.Drawing.Size(41, 41);
            this.btnOpenVideo.TabIndex = 0;
            this.btnOpenVideo.UseVisualStyleBackColor = true;
            this.btnOpenVideo.Click += new System.EventHandler(this.btnOpenVideo_Click);
            // 
            // tbpLabels
            // 
            this.tbpLabels.Controls.Add(this.trvLabels);
            this.tbpLabels.Location = new System.Drawing.Point(4, 22);
            this.tbpLabels.Name = "tbpLabels";
            this.tbpLabels.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLabels.Size = new System.Drawing.Size(274, 667);
            this.tbpLabels.TabIndex = 0;
            this.tbpLabels.Text = "Labels";
            this.tbpLabels.UseVisualStyleBackColor = true;
            // 
            // trvLabels
            // 
            this.trvLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvLabels.ImageIndex = 0;
            this.trvLabels.Location = new System.Drawing.Point(3, 3);
            this.trvLabels.Name = "trvLabels";
            this.trvLabels.SelectedImageIndex = 0;
            this.trvLabels.ShowNodeToolTips = true;
            this.trvLabels.Size = new System.Drawing.Size(268, 661);
            this.trvLabels.TabIndex = 0;
            // 
            // tbpSettings
            // 
            this.tbpSettings.Controls.Add(this.btnSelectProcessedFileArchiveDir);
            this.tbpSettings.Controls.Add(this.label3);
            this.tbpSettings.Controls.Add(this.txtProcessedFilesDir);
            this.tbpSettings.Controls.Add(this.btnSetVideoArchiveDir);
            this.tbpSettings.Controls.Add(this.label2);
            this.tbpSettings.Controls.Add(this.txtVideoArchiveDir);
            this.tbpSettings.Controls.Add(this.btnSetImageDir);
            this.tbpSettings.Controls.Add(this.label1);
            this.tbpSettings.Controls.Add(this.txtImageDir);
            this.tbpSettings.Location = new System.Drawing.Point(4, 22);
            this.tbpSettings.Name = "tbpSettings";
            this.tbpSettings.Size = new System.Drawing.Size(274, 667);
            this.tbpSettings.TabIndex = 2;
            this.tbpSettings.Text = "Settings";
            this.tbpSettings.UseVisualStyleBackColor = true;
            // 
            // btnSelectProcessedFileArchiveDir
            // 
            this.btnSelectProcessedFileArchiveDir.Location = new System.Drawing.Point(241, 104);
            this.btnSelectProcessedFileArchiveDir.Name = "btnSelectProcessedFileArchiveDir";
            this.btnSelectProcessedFileArchiveDir.Size = new System.Drawing.Size(30, 20);
            this.btnSelectProcessedFileArchiveDir.TabIndex = 13;
            this.btnSelectProcessedFileArchiveDir.Text = "...";
            this.btnSelectProcessedFileArchiveDir.UseVisualStyleBackColor = true;
            this.btnSelectProcessedFileArchiveDir.Click += new System.EventHandler(this.btnSelectProcessedFileArchiveDir_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Processed Files Archive Directory";
            // 
            // txtProcessedFilesDir
            // 
            this.txtProcessedFilesDir.Location = new System.Drawing.Point(11, 104);
            this.txtProcessedFilesDir.Name = "txtProcessedFilesDir";
            this.txtProcessedFilesDir.Size = new System.Drawing.Size(224, 20);
            this.txtProcessedFilesDir.TabIndex = 15;
            this.txtProcessedFilesDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessedFilesDir_KeyDown);
            // 
            // btnSetVideoArchiveDir
            // 
            this.btnSetVideoArchiveDir.Location = new System.Drawing.Point(241, 65);
            this.btnSetVideoArchiveDir.Name = "btnSetVideoArchiveDir";
            this.btnSetVideoArchiveDir.Size = new System.Drawing.Size(30, 20);
            this.btnSetVideoArchiveDir.TabIndex = 10;
            this.btnSetVideoArchiveDir.Text = "...";
            this.btnSetVideoArchiveDir.UseVisualStyleBackColor = true;
            this.btnSetVideoArchiveDir.Click += new System.EventHandler(this.btnSetVideoArchiveDir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Video Clip Archive Directory";
            // 
            // txtVideoArchiveDir
            // 
            this.txtVideoArchiveDir.Location = new System.Drawing.Point(11, 65);
            this.txtVideoArchiveDir.Name = "txtVideoArchiveDir";
            this.txtVideoArchiveDir.Size = new System.Drawing.Size(224, 20);
            this.txtVideoArchiveDir.TabIndex = 12;
            this.txtVideoArchiveDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVideoArchiveDir_KeyDown);
            // 
            // btnSetImageDir
            // 
            this.btnSetImageDir.Location = new System.Drawing.Point(241, 26);
            this.btnSetImageDir.Name = "btnSetImageDir";
            this.btnSetImageDir.Size = new System.Drawing.Size(30, 20);
            this.btnSetImageDir.TabIndex = 9;
            this.btnSetImageDir.Text = "...";
            this.btnSetImageDir.UseVisualStyleBackColor = true;
            this.btnSetImageDir.Click += new System.EventHandler(this.btnSetImageDir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Image Directory";
            // 
            // txtImageDir
            // 
            this.txtImageDir.Location = new System.Drawing.Point(11, 26);
            this.txtImageDir.Name = "txtImageDir";
            this.txtImageDir.Size = new System.Drawing.Size(224, 20);
            this.txtImageDir.TabIndex = 9;
            this.txtImageDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImageDir_KeyDown);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(282, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 693);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // stsStatus
            // 
            this.stsStatus.Location = new System.Drawing.Point(0, 717);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(1209, 22);
            this.stsStatus.TabIndex = 9;
            this.stsStatus.Text = "statusStrip1";
            // 
            // lbiImage
            // 
            this.lbiImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbiImage.Location = new System.Drawing.Point(286, 24);
            this.lbiImage.Name = "lbiImage";
            this.lbiImage.Size = new System.Drawing.Size(923, 693);
            this.lbiImage.TabIndex = 4;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1209, 739);
            this.Controls.Add(this.lbiImage);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tbcOptions);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.stsStatus);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fMain";
            this.Text = "AIData";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tbcOptions.ResumeLayout(false);
            this.tbpImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tbpLabels.ResumeLayout(false);
            this.tbpSettings.ResumeLayout(false);
            this.tbpSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mniFile;
        private System.Windows.Forms.ToolStripMenuItem mniOpen;
        private LabellingInterface lbiImage;
        private System.Windows.Forms.TabControl tbcOptions;
        private System.Windows.Forms.TabPage tbpLabels;
        private System.Windows.Forms.TabPage tbpImages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabPage tbpSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImageDir;
        private System.Windows.Forms.Button btnSetImageDir;
        private System.Windows.Forms.DataGridView dgvImages;
        private System.Windows.Forms.CheckBox ckbFilterForNoLabels;
        private System.Windows.Forms.CheckBox ckbFilterForIncomplete;
        private LabelTree trvLabels;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnOpenVideo;
        private System.Windows.Forms.Button btnOpenImages;
        private System.Windows.Forms.Button btnSetVideoArchiveDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVideoArchiveDir;
        private System.Windows.Forms.StatusStrip stsStatus;
        private System.Windows.Forms.DataGridViewImageColumn Thumbnail;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Complete;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.Button btnSelectProcessedFileArchiveDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProcessedFilesDir;
    }
}
