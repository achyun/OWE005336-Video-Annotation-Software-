namespace OWE005336__Video_Annotation_Software_
{
    partial class fProcessTrackerImages
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
            this.dgvImages = new System.Windows.Forms.DataGridView();
            this.Thumbnail = new System.Windows.Forms.DataGridViewImageColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MetadataFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAddedCount = new System.Windows.Forms.Label();
            this.lblImageCount = new System.Windows.Forms.Label();
            this.tgbTags = new OWE005336__Video_Annotation_Software_.TagBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSensorType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.btnSelectLabel = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            this.tips = new System.Windows.Forms.ToolTip(this.components);
            this.roiSelector = new OWE005336__Video_Annotation_Software_.ROISelector();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
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
            this.FileName,
            this.FilePath,
            this.MetadataFilePath});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvImages.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvImages.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvImages.Location = new System.Drawing.Point(0, 0);
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
            this.dgvImages.RowHeadersWidth = 51;
            this.dgvImages.RowTemplate.Height = 50;
            this.dgvImages.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvImages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImages.Size = new System.Drawing.Size(268, 607);
            this.dgvImages.TabIndex = 10;
            this.dgvImages.SelectionChanged += new System.EventHandler(this.dgvImages_SelectionChanged);
            this.dgvImages.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvImages_KeyDown);
            this.dgvImages.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvImages_KeyUp);
            // 
            // Thumbnail
            // 
            this.Thumbnail.HeaderText = "Thumbnail";
            this.Thumbnail.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Thumbnail.MinimumWidth = 6;
            this.Thumbnail.Name = "Thumbnail";
            this.Thumbnail.ReadOnly = true;
            this.Thumbnail.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Thumbnail.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Thumbnail.Width = 125;
            // 
            // FileName
            // 
            this.FileName.HeaderText = "Name";
            this.FileName.MinimumWidth = 6;
            this.FileName.Name = "FileName";
            this.FileName.Width = 125;
            // 
            // FilePath
            // 
            this.FilePath.HeaderText = "File Path";
            this.FilePath.MinimumWidth = 6;
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Width = 125;
            // 
            // MetadataFilePath
            // 
            this.MetadataFilePath.HeaderText = "Metadata File Path";
            this.MetadataFilePath.MinimumWidth = 6;
            this.MetadataFilePath.Name = "MetadataFilePath";
            this.MetadataFilePath.ReadOnly = true;
            this.MetadataFilePath.Width = 125;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(268, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(6, 607);
            this.splitter1.TabIndex = 11;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1136, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 607);
            this.panel1.TabIndex = 13;
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
            this.panel4.Location = new System.Drawing.Point(0, 321);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(5);
            this.panel4.Size = new System.Drawing.Size(200, 286);
            this.panel4.TabIndex = 14;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnFinish);
            this.panel6.Controls.Add(this.btnCancel);
            this.panel6.Controls.Add(this.btnSave);
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(5, 203);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5);
            this.panel6.Size = new System.Drawing.Size(190, 78);
            this.panel6.TabIndex = 19;
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(112, 52);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 11;
            this.btnFinish.Text = "Finish";
            this.tips.SetToolTip(this.btnFinish, "Close");
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.CancelIcon;
            this.btnCancel.Location = new System.Drawing.Point(96, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(41, 41);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tips.SetToolTip(this.btnCancel, "Reject & Delete Image");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.OkTick;
            this.btnSave.Location = new System.Drawing.Point(143, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 41);
            this.btnSave.TabIndex = 1;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tips.SetToolTip(this.btnSave, "Import Image");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAddedCount);
            this.groupBox1.Controls.Add(this.lblImageCount);
            this.groupBox1.Location = new System.Drawing.Point(8, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 47);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Save";
            // 
            // lblAddedCount
            // 
            this.lblAddedCount.AutoSize = true;
            this.lblAddedCount.Location = new System.Drawing.Point(46, 16);
            this.lblAddedCount.Name = "lblAddedCount";
            this.lblAddedCount.Size = new System.Drawing.Size(13, 13);
            this.lblAddedCount.TabIndex = 9;
            this.lblAddedCount.Text = "0";
            // 
            // lblImageCount
            // 
            this.lblImageCount.AutoSize = true;
            this.lblImageCount.Location = new System.Drawing.Point(6, 16);
            this.lblImageCount.Name = "lblImageCount";
            this.lblImageCount.Size = new System.Drawing.Size(41, 13);
            this.lblImageCount.TabIndex = 7;
            this.lblImageCount.Text = "Added:";
            // 
            // tgbTags
            // 
            this.tgbTags.AutoScroll = true;
            this.tgbTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tgbTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.tgbTags.Location = new System.Drawing.Point(5, 86);
            this.tgbTags.Name = "tgbTags";
            this.tgbTags.Size = new System.Drawing.Size(190, 117);
            this.tgbTags.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Tags";
            // 
            // cmbSensorType
            // 
            this.cmbSensorType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbSensorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSensorType.FormattingEnabled = true;
            this.cmbSensorType.Location = new System.Drawing.Point(5, 52);
            this.cmbSensorType.Name = "cmbSensorType";
            this.cmbSensorType.Size = new System.Drawing.Size(190, 21);
            this.cmbSensorType.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Sensor Type";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtLabel);
            this.panel5.Controls.Add(this.btnSelectLabel);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel5.Location = new System.Drawing.Point(5, 18);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(190, 21);
            this.panel5.TabIndex = 18;
            // 
            // txtLabel
            // 
            this.txtLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLabel.Location = new System.Drawing.Point(0, 0);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.ReadOnly = true;
            this.txtLabel.Size = new System.Drawing.Size(165, 20);
            this.txtLabel.TabIndex = 11;
            this.txtLabel.DoubleClick += new System.EventHandler(this.txtLabel_DoubleClick);
            // 
            // btnSelectLabel
            // 
            this.btnSelectLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectLabel.Location = new System.Drawing.Point(165, 0);
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
            this.Label.Size = new System.Drawing.Size(33, 13);
            this.Label.TabIndex = 9;
            this.Label.Text = "Label";
            // 
            // roiSelector
            // 
            this.roiSelector.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.roiSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roiSelector.Location = new System.Drawing.Point(274, 0);
            this.roiSelector.Name = "roiSelector";
            this.roiSelector.SelectedROIIndex = -1;
            this.roiSelector.Size = new System.Drawing.Size(862, 607);
            this.roiSelector.TabIndex = 0;
            this.roiSelector.Text = "roiSelector1";
            // 
            // fProcessTrackerImages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 607);
            this.Controls.Add(this.roiSelector);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.dgvImages);
            this.Name = "fProcessTrackerImages";
            this.Text = "Review Images from CV System";
            this.Load += new System.EventHandler(this.fProcessTrackerImages_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvImages_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImages)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ROISelector roiSelector;
        private System.Windows.Forms.DataGridView dgvImages;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblImageCount;
        private TagBox tgbTags;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSensorType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Button btnSelectLabel;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.ToolTip tips;
        private System.Windows.Forms.Label lblAddedCount;
        private System.Windows.Forms.DataGridViewImageColumn Thumbnail;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn MetadataFilePath;
    }
}