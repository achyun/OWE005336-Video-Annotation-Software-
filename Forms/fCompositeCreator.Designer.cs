namespace OWE005336__Video_Annotation_Software_

{
    partial class fCompositeCreator
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.nudRotationRange = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudSizeMax = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudSizeMin = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudNumberVariations = new System.Windows.Forms.NumericUpDown();
            this.btnSelectLabel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtObjectLabel = new System.Windows.Forms.TextBox();
            this.btnObjectDirectory = new System.Windows.Forms.Button();
            this.btnBackgroundDirectory = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtObjectImagesDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBackgroundImagesDir = new System.Windows.Forms.TextBox();
            this.ckbMinimiseBBox = new System.Windows.Forms.CheckBox();
            this.btnSaveAll = new System.Windows.Forms.Button();
            this.btnSaveSingle = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbSensorType = new System.Windows.Forms.ComboBox();
            this.stsStatus = new System.Windows.Forms.StatusStrip();
            this.roiSelector = new OWE005336__Video_Annotation_Software_.ROISelector();
            this.tagBox = new OWE005336__Video_Annotation_Software_.TagBox();
            this.ckbIncludeSubDirsBackground = new System.Windows.Forms.CheckBox();
            this.ckbIncludeSubDirsObjects = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberVariations)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ckbIncludeSubDirsObjects);
            this.panel1.Controls.Add(this.ckbIncludeSubDirsBackground);
            this.panel1.Controls.Add(this.cmbSensorType);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnSaveAll);
            this.panel1.Controls.Add(this.btnSaveSingle);
            this.panel1.Controls.Add(this.ckbMinimiseBBox);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.nudRotationRange);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.nudSizeMax);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.nudSizeMin);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.nudNumberVariations);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnSelectLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtObjectLabel);
            this.panel1.Controls.Add(this.btnObjectDirectory);
            this.panel1.Controls.Add(this.btnBackgroundDirectory);
            this.panel1.Controls.Add(this.tagBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtObjectImagesDir);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtBackgroundImagesDir);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 633);
            this.panel1.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(67, 485);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "+/-";
            // 
            // nudRotationRange
            // 
            this.nudRotationRange.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudRotationRange.Location = new System.Drawing.Point(12, 483);
            this.nudRotationRange.Maximum = new decimal(new int[] {
            180,
            0,
            0,
            0});
            this.nudRotationRange.Name = "nudRotationRange";
            this.nudRotationRange.Size = new System.Drawing.Size(49, 20);
            this.nudRotationRange.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 467);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Rotation Range";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 446);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "to";
            // 
            // nudSizeMax
            // 
            this.nudSizeMax.DecimalPlaces = 2;
            this.nudSizeMax.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudSizeMax.Location = new System.Drawing.Point(89, 444);
            this.nudSizeMax.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSizeMax.Name = "nudSizeMax";
            this.nudSizeMax.Size = new System.Drawing.Size(49, 20);
            this.nudSizeMax.TabIndex = 15;
            this.nudSizeMax.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 428);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Fraction of Image Size";
            // 
            // nudSizeMin
            // 
            this.nudSizeMin.DecimalPlaces = 2;
            this.nudSizeMin.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudSizeMin.Location = new System.Drawing.Point(12, 444);
            this.nudSizeMin.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSizeMin.Name = "nudSizeMin";
            this.nudSizeMin.Size = new System.Drawing.Size(49, 20);
            this.nudSizeMin.TabIndex = 13;
            this.nudSizeMin.Value = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 389);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Number of variations per object image";
            // 
            // nudNumberVariations
            // 
            this.nudNumberVariations.Location = new System.Drawing.Point(12, 405);
            this.nudNumberVariations.Name = "nudNumberVariations";
            this.nudNumberVariations.Size = new System.Drawing.Size(49, 20);
            this.nudNumberVariations.TabIndex = 1;
            this.nudNumberVariations.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnSelectLabel
            // 
            this.btnSelectLabel.Location = new System.Drawing.Point(194, 337);
            this.btnSelectLabel.Name = "btnSelectLabel";
            this.btnSelectLabel.Size = new System.Drawing.Size(25, 20);
            this.btnSelectLabel.TabIndex = 10;
            this.btnSelectLabel.Text = "...";
            this.btnSelectLabel.UseVisualStyleBackColor = true;
            this.btnSelectLabel.Click += new System.EventHandler(this.btnSelectLabel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Object Label";
            // 
            // txtObjectLabel
            // 
            this.txtObjectLabel.Location = new System.Drawing.Point(12, 338);
            this.txtObjectLabel.Name = "txtObjectLabel";
            this.txtObjectLabel.ReadOnly = true;
            this.txtObjectLabel.Size = new System.Drawing.Size(180, 20);
            this.txtObjectLabel.TabIndex = 8;
            // 
            // btnObjectDirectory
            // 
            this.btnObjectDirectory.Location = new System.Drawing.Point(194, 264);
            this.btnObjectDirectory.Name = "btnObjectDirectory";
            this.btnObjectDirectory.Size = new System.Drawing.Size(25, 20);
            this.btnObjectDirectory.TabIndex = 7;
            this.btnObjectDirectory.Text = "...";
            this.btnObjectDirectory.UseVisualStyleBackColor = true;
            this.btnObjectDirectory.Click += new System.EventHandler(this.btnObjectDirectory_Click);
            // 
            // btnBackgroundDirectory
            // 
            this.btnBackgroundDirectory.Location = new System.Drawing.Point(194, 32);
            this.btnBackgroundDirectory.Name = "btnBackgroundDirectory";
            this.btnBackgroundDirectory.Size = new System.Drawing.Size(25, 20);
            this.btnBackgroundDirectory.TabIndex = 6;
            this.btnBackgroundDirectory.Text = "...";
            this.btnBackgroundDirectory.UseVisualStyleBackColor = true;
            this.btnBackgroundDirectory.Click += new System.EventHandler(this.btnBackgroundDirectory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Object Images Directory";
            // 
            // txtObjectImagesDir
            // 
            this.txtObjectImagesDir.Location = new System.Drawing.Point(12, 265);
            this.txtObjectImagesDir.Name = "txtObjectImagesDir";
            this.txtObjectImagesDir.Size = new System.Drawing.Size(180, 20);
            this.txtObjectImagesDir.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tags";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Background Images Directory";
            // 
            // txtBackgroundImagesDir
            // 
            this.txtBackgroundImagesDir.Location = new System.Drawing.Point(12, 32);
            this.txtBackgroundImagesDir.Name = "txtBackgroundImagesDir";
            this.txtBackgroundImagesDir.Size = new System.Drawing.Size(180, 20);
            this.txtBackgroundImagesDir.TabIndex = 1;
            // 
            // ckbMinimiseBBox
            // 
            this.ckbMinimiseBBox.AutoSize = true;
            this.ckbMinimiseBBox.Location = new System.Drawing.Point(15, 509);
            this.ckbMinimiseBBox.Name = "ckbMinimiseBBox";
            this.ckbMinimiseBBox.Size = new System.Drawing.Size(193, 17);
            this.ckbMinimiseBBox.TabIndex = 2;
            this.ckbMinimiseBBox.Text = "Minimise bounding box with rotation";
            this.ckbMinimiseBBox.UseVisualStyleBackColor = true;
            // 
            // btnSaveAll
            // 
            this.btnSaveAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAll.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.Save_All_32x32;
            this.btnSaveAll.Location = new System.Drawing.Point(84, 532);
            this.btnSaveAll.Name = "btnSaveAll";
            this.btnSaveAll.Size = new System.Drawing.Size(41, 41);
            this.btnSaveAll.TabIndex = 21;
            this.btnSaveAll.UseVisualStyleBackColor = true;
            this.btnSaveAll.Click += new System.EventHandler(this.btnSaveAll_Click);
            // 
            // btnSaveSingle
            // 
            this.btnSaveSingle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSingle.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.Save_32x32;
            this.btnSaveSingle.Location = new System.Drawing.Point(131, 532);
            this.btnSaveSingle.Name = "btnSaveSingle";
            this.btnSaveSingle.Size = new System.Drawing.Size(41, 41);
            this.btnSaveSingle.TabIndex = 20;
            this.btnSaveSingle.UseVisualStyleBackColor = true;
            this.btnSaveSingle.Click += new System.EventHandler(this.btnSaveSingle_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.refresh_icon_32x32;
            this.btnRefresh.Location = new System.Drawing.Point(178, 532);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(41, 41);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Sensor Type";
            // 
            // cmbSensorType
            // 
            this.cmbSensorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSensorType.FormattingEnabled = true;
            this.cmbSensorType.Location = new System.Drawing.Point(12, 103);
            this.cmbSensorType.Name = "cmbSensorType";
            this.cmbSensorType.Size = new System.Drawing.Size(207, 21);
            this.cmbSensorType.TabIndex = 23;
            // 
            // stsStatus
            // 
            this.stsStatus.Location = new System.Drawing.Point(0, 633);
            this.stsStatus.Name = "stsStatus";
            this.stsStatus.Size = new System.Drawing.Size(1001, 22);
            this.stsStatus.TabIndex = 2;
            this.stsStatus.Text = "statusStrip1";
            // 
            // roiSelector
            // 
            this.roiSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roiSelector.Enabled = false;
            this.roiSelector.Location = new System.Drawing.Point(230, 0);
            this.roiSelector.Name = "roiSelector";
            this.roiSelector.SelectedROIIndex = -1;
            this.roiSelector.Size = new System.Drawing.Size(771, 633);
            this.roiSelector.TabIndex = 1;
            this.roiSelector.Text = "roiSelector1";
            // 
            // tagBox
            // 
            this.tagBox.AutoScroll = true;
            this.tagBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tagBox.Location = new System.Drawing.Point(12, 143);
            this.tagBox.Name = "tagBox";
            this.tagBox.Size = new System.Drawing.Size(207, 79);
            this.tagBox.TabIndex = 1;
            // 
            // ckbIncludeSubDirsBackground
            // 
            this.ckbIncludeSubDirsBackground.AutoSize = true;
            this.ckbIncludeSubDirsBackground.Location = new System.Drawing.Point(15, 58);
            this.ckbIncludeSubDirsBackground.Name = "ckbIncludeSubDirsBackground";
            this.ckbIncludeSubDirsBackground.Size = new System.Drawing.Size(136, 17);
            this.ckbIncludeSubDirsBackground.TabIndex = 3;
            this.ckbIncludeSubDirsBackground.Text = "Include Sub Directories";
            this.ckbIncludeSubDirsBackground.UseVisualStyleBackColor = true;
            // 
            // ckbIncludeSubDirsObjects
            // 
            this.ckbIncludeSubDirsObjects.AutoSize = true;
            this.ckbIncludeSubDirsObjects.Location = new System.Drawing.Point(15, 291);
            this.ckbIncludeSubDirsObjects.Name = "ckbIncludeSubDirsObjects";
            this.ckbIncludeSubDirsObjects.Size = new System.Drawing.Size(136, 17);
            this.ckbIncludeSubDirsObjects.TabIndex = 24;
            this.ckbIncludeSubDirsObjects.Text = "Include Sub Directories";
            this.ckbIncludeSubDirsObjects.UseVisualStyleBackColor = true;
            // 
            // fCompositeCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 655);
            this.Controls.Add(this.roiSelector);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.stsStatus);
            this.Name = "fCompositeCreator";
            this.Text = "Composite Creator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fCompositeCreator_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRotationRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSizeMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumberVariations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private TagBox tagBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtObjectImagesDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBackgroundImagesDir;
        private System.Windows.Forms.Button btnSelectLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtObjectLabel;
        private System.Windows.Forms.Button btnObjectDirectory;
        private System.Windows.Forms.Button btnBackgroundDirectory;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudNumberVariations;
        private ROISelector roiSelector;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudRotationRange;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudSizeMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudSizeMin;
        private System.Windows.Forms.CheckBox ckbMinimiseBBox;
        private System.Windows.Forms.Button btnSaveAll;
        private System.Windows.Forms.Button btnSaveSingle;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbSensorType;
        private System.Windows.Forms.StatusStrip stsStatus;
        private System.Windows.Forms.CheckBox ckbIncludeSubDirsObjects;
        private System.Windows.Forms.CheckBox ckbIncludeSubDirsBackground;
    }
}