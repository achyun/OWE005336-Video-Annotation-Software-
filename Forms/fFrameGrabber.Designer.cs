namespace OWE005336__Video_Annotation_Software_
{
    partial class fFrameGrabber
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
            this.ltvThumbnails = new System.Windows.Forms.ListView();
            this.imgFrames = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tkbScan = new System.Windows.Forms.TrackBar();
            this.cpsClipSelector = new OWE005336__Video_Annotation_Software_.ClipSelector();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSaveFrame = new System.Windows.Forms.Button();
            this.tmrRefreshFrame = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ckbArchiveVideo = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblImageCount = new System.Windows.Forms.Label();
            this.lblClipCount = new System.Windows.Forms.Label();
            this.tgbTags = new OWE005336__Video_Annotation_Software_.TagBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSensorType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.btnSelectLabel = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            this.pcbFrame = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbScan)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // ltvThumbnails
            // 
            this.ltvThumbnails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltvThumbnails.HideSelection = false;
            this.ltvThumbnails.LargeImageList = this.imgFrames;
            this.ltvThumbnails.Location = new System.Drawing.Point(0, 0);
            this.ltvThumbnails.Name = "ltvThumbnails";
            this.ltvThumbnails.Size = new System.Drawing.Size(275, 776);
            this.ltvThumbnails.TabIndex = 1;
            this.ltvThumbnails.UseCompatibleStateImageBehavior = false;
            this.ltvThumbnails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ltvThumbnails_KeyDown);
            // 
            // imgFrames
            // 
            this.imgFrames.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgFrames.ImageSize = new System.Drawing.Size(160, 90);
            this.imgFrames.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(1240, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 776);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tkbScan);
            this.panel1.Controls.Add(this.cpsClipSelector);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 707);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1240, 69);
            this.panel1.TabIndex = 4;
            // 
            // tkbScan
            // 
            this.tkbScan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tkbScan.Location = new System.Drawing.Point(0, -5);
            this.tkbScan.Name = "tkbScan";
            this.tkbScan.Size = new System.Drawing.Size(1193, 56);
            this.tkbScan.TabIndex = 1;
            this.tkbScan.ValueChanged += new System.EventHandler(this.tkbScan_ValueChanged);
            // 
            // cpsClipSelector
            // 
            this.cpsClipSelector.BorderWidth = 5;
            this.cpsClipSelector.DefaultClipLength = 4F;
            this.cpsClipSelector.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cpsClipSelector.HighlightedClipIndex = -1;
            this.cpsClipSelector.Location = new System.Drawing.Point(0, 51);
            this.cpsClipSelector.Name = "cpsClipSelector";
            this.cpsClipSelector.SelectedClipIndex = -1;
            this.cpsClipSelector.Size = new System.Drawing.Size(1193, 18);
            this.cpsClipSelector.TabIndex = 3;
            this.cpsClipSelector.Text = "clipSelector1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSaveFrame);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1193, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(47, 69);
            this.panel2.TabIndex = 2;
            // 
            // btnSaveFrame
            // 
            this.btnSaveFrame.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFrame.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.plus_icon__32x32_;
            this.btnSaveFrame.Location = new System.Drawing.Point(3, 16);
            this.btnSaveFrame.Name = "btnSaveFrame";
            this.btnSaveFrame.Size = new System.Drawing.Size(41, 41);
            this.btnSaveFrame.TabIndex = 0;
            this.btnSaveFrame.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveFrame.UseVisualStyleBackColor = true;
            this.btnSaveFrame.Click += new System.EventHandler(this.btnSaveFrame_Click);
            // 
            // tmrRefreshFrame
            // 
            this.tmrRefreshFrame.Interval = 333;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.ltvThumbnails);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(1244, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(275, 776);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.tgbTags);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.cmbSensorType);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.Label);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 490);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(275, 286);
            this.panel4.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.ckbArchiveVideo);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Controls.Add(this.btnSave);
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(5, 209);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5);
            this.panel6.Size = new System.Drawing.Size(265, 78);
            this.panel6.TabIndex = 19;
            // 
            // ckbArchiveVideo
            // 
            this.ckbArchiveVideo.AutoSize = true;
            this.ckbArchiveVideo.Location = new System.Drawing.Point(175, 54);
            this.ckbArchiveVideo.Name = "ckbArchiveVideo";
            this.ckbArchiveVideo.Size = new System.Drawing.Size(102, 19);
            this.ckbArchiveVideo.TabIndex = 11;
            this.ckbArchiveVideo.Text = "Archive Video";
            this.ckbArchiveVideo.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.CancelIcon;
            this.btnCancel.Location = new System.Drawing.Point(171, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(41, 41);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.OkTick;
            this.btnSave.Location = new System.Drawing.Point(218, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 41);
            this.btnSave.TabIndex = 1;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblImageCount);
            this.groupBox1.Controls.Add(this.lblClipCount);
            this.groupBox1.Location = new System.Drawing.Point(8, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 47);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save";
            // 
            // lblImageCount
            // 
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Location = new System.Drawing.Point(38, 14);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(61, 15);
            this.lblImageCount.TabIndex = 7;
            this.lblImageCount.Text = "Images: 0";
            // 
            // lblClipCount
            // 
            this.lblClipCount.AutoSize = true;
            this.lblClipCount.Location = new System.Drawing.Point(38, 31);
            this.lblClipCount.Name = "lblClipCount";
            this.lblClipCount.Size = new System.Drawing.Size(47, 15);
            this.lblClipCount.TabIndex = 8;
            this.lblClipCount.Text = "Clips: 0";
            // 
            // tgbTags
            // 
            this.tgbTags.AutoScroll = true;
            this.tgbTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tgbTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.tgbTags.Location = new System.Drawing.Point(5, 92);
            this.tgbTags.Name = "tgbTags";
            this.tgbTags.Size = new System.Drawing.Size(265, 117);
            this.tgbTags.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tags";
            // 
            // cmbSensorType
            // 
            this.cmbSensorType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbSensorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSensorType.FormattingEnabled = true;
            this.cmbSensorType.Location = new System.Drawing.Point(5, 56);
            this.cmbSensorType.Name = "cmbSensorType";
            this.cmbSensorType.Size = new System.Drawing.Size(265, 21);
            this.cmbSensorType.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Sensor Type";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtLabel);
            this.panel5.Controls.Add(this.btnSelectLabel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel5.Location = new System.Drawing.Point(5, 20);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(265, 21);
            this.panel5.TabIndex = 18;
            // 
            // txtLabel
            // 
            this.txtLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLabel.Location = new System.Drawing.Point(0, 0);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.ReadOnly = true;
            this.txtLabel.Size = new System.Drawing.Size(240, 20);
            this.txtLabel.TabIndex = 11;
            this.txtLabel.DoubleClick += new System.EventHandler(this.txtLabel_DoubleClick);
            // 
            // btnSelectLabel
            // 
            this.btnSelectLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectLabel.Location = new System.Drawing.Point(240, 0);
            this.btnSelectLabel.Name = "btnSelectLabel";
            this.btnSelectLabel.Size = new System.Drawing.Size(25, 21);
            this.btnSelectLabel.TabIndex = 14;
            this.btnSelectLabel.Text = "...";
            this.btnSelectLabel.UseVisualStyleBackColor = true;
            this.btnSelectLabel.Click += new System.EventHandler(this.btnSelectLabel_Click);
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label.Location = new System.Drawing.Point(5, 5);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(38, 15);
            this.Label.TabIndex = 9;
            this.Label.Text = "Label";
            // 
            // pcbFrame
            // 
            this.pcbFrame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pcbFrame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pcbFrame.Location = new System.Drawing.Point(0, 0);
            this.pcbFrame.Name = "pcbFrame";
            this.pcbFrame.Size = new System.Drawing.Size(1240, 707);
            this.pcbFrame.TabIndex = 3;
            this.pcbFrame.TabStop = false;
            // 
            // fFrameGrabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1519, 776);
            this.Controls.Add(this.pcbFrame);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel3);
            this.Name = "fFrameGrabber";
            this.Text = "fFrameGrabber";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkbScan)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView ltvThumbnails;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.PictureBox pcbFrame;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar tkbScan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSaveFrame;
        private System.Windows.Forms.Timer tmrRefreshFrame;
        private System.Windows.Forms.ImageList imgFrames;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblClipCount;
        private System.Windows.Forms.Label lblImageCount;
        private ClipSelector cpsClipSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSelectLabel;
        private TagBox tgbTags;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSensorType;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox ckbArchiveVideo;
    }
}