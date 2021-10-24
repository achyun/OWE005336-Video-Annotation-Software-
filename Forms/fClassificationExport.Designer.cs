namespace OWE005336__Video_Annotation_Software_
{
    partial class fClassificationExport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnOpenPrevScript = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.nudMinPixelsTest = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudMinPixelsValidation = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudMinPixelsTrain = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.ckbPadTrainData = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbPadValidationData = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbPadTestData = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudTrainPct = new System.Windows.Forms.NumericUpDown();
            this.nudTestPct = new System.Windows.Forms.NumericUpDown();
            this.nudValidPct = new System.Windows.Forms.NumericUpDown();
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.lblSummary = new System.Windows.Forms.Label();
            this.txtOutputDir = new System.Windows.Forms.TextBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnSelectDirectory = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkGenerateImages = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.domainLabelsSelector = new OWE005336__Video_Annotation_Software_.LabelBox();
            this.outputLabelsSelector = new OWE005336__Video_Annotation_Software_.LabelBox();
            this.projectSelector = new OWE005336__Video_Annotation_Software_.ProjectBox();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixelsTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixelsValidation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixelsTrain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValidPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSQL);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.splitter2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.dgvTasks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 658);
            this.panel1.TabIndex = 0;
            // 
            // txtSQL
            // 
            this.txtSQL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQL.Location = new System.Drawing.Point(0, 227);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(828, 360);
            this.txtSQL.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.projectSelector);
            this.panel5.Controls.Add(this.label13);
            this.panel5.Controls.Add(this.domainLabelsSelector);
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.label11);
            this.panel5.Controls.Add(this.outputLabelsSelector);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 124);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(828, 103);
            this.panel5.TabIndex = 15;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Domains:";
            this.toolTip1.SetToolTip(this.label12, "The labels that will cause this classifier to be run");
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(47, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Outputs:";
            this.toolTip1.SetToolTip(this.label11, "The labels that this classifier will be trained against");
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 120);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(828, 4);
            this.splitter2.TabIndex = 13;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnOpenPrevScript);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.nudMinPixelsTest);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.nudMinPixelsValidation);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.nudMinPixelsTrain);
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.ckbPadTrainData);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.ckbPadValidationData);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.ckbPadTestData);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.nudTrainPct);
            this.panel3.Controls.Add(this.nudTestPct);
            this.panel3.Controls.Add(this.nudValidPct);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 587);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(828, 71);
            this.panel3.TabIndex = 12;
            // 
            // btnOpenPrevScript
            // 
            this.btnOpenPrevScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPrevScript.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources._1200px_OneDrive_Folder_Icon__32x32_;
            this.btnOpenPrevScript.Location = new System.Drawing.Point(696, 17);
            this.btnOpenPrevScript.Name = "btnOpenPrevScript";
            this.btnOpenPrevScript.Size = new System.Drawing.Size(41, 41);
            this.btnOpenPrevScript.TabIndex = 21;
            this.btnOpenPrevScript.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnOpenPrevScript, "Open previous export script");
            this.btnOpenPrevScript.UseVisualStyleBackColor = true;
            this.btnOpenPrevScript.Click += new System.EventHandler(this.btnOpenPrevScript_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(380, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Min Test Image Size";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(574, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "pixels";
            // 
            // nudMinPixelsTest
            // 
            this.nudMinPixelsTest.Location = new System.Drawing.Point(510, 47);
            this.nudMinPixelsTest.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMinPixelsTest.Name = "nudMinPixelsTest";
            this.nudMinPixelsTest.Size = new System.Drawing.Size(63, 20);
            this.nudMinPixelsTest.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(380, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Min Validation Image Size";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(574, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "pixels";
            // 
            // nudMinPixelsValidation
            // 
            this.nudMinPixelsValidation.Location = new System.Drawing.Point(510, 24);
            this.nudMinPixelsValidation.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMinPixelsValidation.Name = "nudMinPixelsValidation";
            this.nudMinPixelsValidation.Size = new System.Drawing.Size(63, 20);
            this.nudMinPixelsValidation.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(380, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Min Train Image Size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(574, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "pixels";
            // 
            // nudMinPixelsTrain
            // 
            this.nudMinPixelsTrain.Location = new System.Drawing.Point(510, 2);
            this.nudMinPixelsTrain.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMinPixelsTrain.Name = "nudMinPixelsTrain";
            this.nudMinPixelsTrain.Size = new System.Drawing.Size(63, 20);
            this.nudMinPixelsTrain.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.OkTick;
            this.btnSave.Location = new System.Drawing.Point(774, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 41);
            this.btnSave.TabIndex = 2;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.btnSave, "Save query text and generate images");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ckbPadTrainData
            // 
            this.ckbPadTrainData.AutoSize = true;
            this.ckbPadTrainData.Location = new System.Drawing.Point(3, 3);
            this.ckbPadTrainData.Name = "ckbPadTrainData";
            this.ckbPadTrainData.Size = new System.Drawing.Size(112, 17);
            this.ckbPadTrainData.TabIndex = 3;
            this.ckbPadTrainData.Text = "Pad Training Data";
            this.ckbPadTrainData.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "% as Test Images";
            // 
            // ckbPadValidationData
            // 
            this.ckbPadValidationData.AutoSize = true;
            this.ckbPadValidationData.Location = new System.Drawing.Point(3, 26);
            this.ckbPadValidationData.Name = "ckbPadValidationData";
            this.ckbPadValidationData.Size = new System.Drawing.Size(120, 17);
            this.ckbPadValidationData.TabIndex = 4;
            this.ckbPadValidationData.Text = "Pad Validation Data";
            this.ckbPadValidationData.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "% as Validation Images";
            // 
            // ckbPadTestData
            // 
            this.ckbPadTestData.AutoSize = true;
            this.ckbPadTestData.Location = new System.Drawing.Point(3, 49);
            this.ckbPadTestData.Name = "ckbPadTestData";
            this.ckbPadTestData.Size = new System.Drawing.Size(95, 17);
            this.ckbPadTestData.TabIndex = 5;
            this.ckbPadTestData.Text = "Pad Test Data";
            this.ckbPadTestData.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(218, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "% as Training Images";
            // 
            // nudTrainPct
            // 
            this.nudTrainPct.Location = new System.Drawing.Point(149, 2);
            this.nudTrainPct.Name = "nudTrainPct";
            this.nudTrainPct.Size = new System.Drawing.Size(63, 20);
            this.nudTrainPct.TabIndex = 6;
            // 
            // nudTestPct
            // 
            this.nudTestPct.Location = new System.Drawing.Point(149, 48);
            this.nudTestPct.Name = "nudTestPct";
            this.nudTestPct.Size = new System.Drawing.Size(63, 20);
            this.nudTestPct.TabIndex = 8;
            // 
            // nudValidPct
            // 
            this.nudValidPct.Location = new System.Drawing.Point(149, 25);
            this.nudValidPct.Name = "nudValidPct";
            this.nudValidPct.Size = new System.Drawing.Size(63, 20);
            this.nudValidPct.TabIndex = 7;
            // 
            // dgvTasks
            // 
            this.dgvTasks.AllowUserToResizeRows = false;
            this.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTasks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTasks.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvTasks.Location = new System.Drawing.Point(0, 0);
            this.dgvTasks.MultiSelect = false;
            this.dgvTasks.Name = "dgvTasks";
            this.dgvTasks.Size = new System.Drawing.Size(828, 120);
            this.dgvTasks.TabIndex = 1;
            this.dgvTasks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTasks_CellEndEdit);
            this.dgvTasks.SelectionChanged += new System.EventHandler(this.dgvTasks_SelectionChanged);
            this.dgvTasks.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvTasks_UserAddedRow);
            this.dgvTasks.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvTasks_UserDeletingRow);
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSummary.Location = new System.Drawing.Point(3, 3);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(372, 115);
            this.lblSummary.TabIndex = 13;
            this.lblSummary.Text = "label4";
            // 
            // txtOutputDir
            // 
            this.txtOutputDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputDir.Location = new System.Drawing.Point(96, 121);
            this.txtOutputDir.Name = "txtOutputDir";
            this.txtOutputDir.Size = new System.Drawing.Size(251, 20);
            this.txtOutputDir.TabIndex = 14;
            this.txtOutputDir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutputDir_KeyDown);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(233, 152);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(130, 40);
            this.btnExport.TabIndex = 15;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnSelectDirectory
            // 
            this.btnSelectDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectDirectory.Location = new System.Drawing.Point(353, 121);
            this.btnSelectDirectory.Name = "btnSelectDirectory";
            this.btnSelectDirectory.Size = new System.Drawing.Size(22, 20);
            this.btnSelectDirectory.TabIndex = 16;
            this.btnSelectDirectory.Text = "...";
            this.btnSelectDirectory.UseVisualStyleBackColor = true;
            this.btnSelectDirectory.Click += new System.EventHandler(this.btnSelectDirectory_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(828, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 658);
            this.splitter1.TabIndex = 17;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvResults);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(832, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(378, 658);
            this.panel2.TabIndex = 18;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.Size = new System.Drawing.Size(378, 451);
            this.dgvResults.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chkGenerateImages);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lblSummary);
            this.panel4.Controls.Add(this.txtOutputDir);
            this.panel4.Controls.Add(this.btnExport);
            this.panel4.Controls.Add(this.btnSelectDirectory);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 451);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(378, 207);
            this.panel4.TabIndex = 18;
            // 
            // chkGenerateImages
            // 
            this.chkGenerateImages.AutoSize = true;
            this.chkGenerateImages.Location = new System.Drawing.Point(9, 152);
            this.chkGenerateImages.Name = "chkGenerateImages";
            this.chkGenerateImages.Size = new System.Drawing.Size(107, 17);
            this.chkGenerateImages.TabIndex = 18;
            this.chkGenerateImages.Text = "Generate Images";
            this.toolTip1.SetToolTip(this.chkGenerateImages, "Generate images of each labelled ROI and save into folders");
            this.chkGenerateImages.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Output Directory";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Projects:";
            this.toolTip1.SetToolTip(this.label13, "The projects from which to draw images");
            // 
            // domainLabelsSelector
            // 
            this.domainLabelsSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.domainLabelsSelector.AutoScroll = true;
            this.domainLabelsSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.domainLabelsSelector.Location = new System.Drawing.Point(59, 39);
            this.domainLabelsSelector.Name = "domainLabelsSelector";
            this.domainLabelsSelector.Size = new System.Drawing.Size(766, 29);
            this.domainLabelsSelector.TabIndex = 15;
            // 
            // outputLabelsSelector
            // 
            this.outputLabelsSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputLabelsSelector.AutoScroll = true;
            this.outputLabelsSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputLabelsSelector.Location = new System.Drawing.Point(59, 71);
            this.outputLabelsSelector.Name = "outputLabelsSelector";
            this.outputLabelsSelector.Size = new System.Drawing.Size(766, 29);
            this.outputLabelsSelector.TabIndex = 14;
            // 
            // projectBox1
            // 
            this.projectSelector.AutoScroll = true;
            this.projectSelector.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.projectSelector.Location = new System.Drawing.Point(59, 6);
            this.projectSelector.Name = "projectBox1";
            this.projectSelector.Size = new System.Drawing.Size(766, 29);
            this.projectSelector.TabIndex = 18;
            // 
            // fClassificationExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1210, 658);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Name = "fClassificationExport";
            this.Text = "Export Images for Classifier Training";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixelsTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixelsValidation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixelsTrain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValidPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.TextBox txtSQL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTestPct;
        private System.Windows.Forms.NumericUpDown nudValidPct;
        private System.Windows.Forms.NumericUpDown nudTrainPct;
        private System.Windows.Forms.CheckBox ckbPadTestData;
        private System.Windows.Forms.CheckBox ckbPadValidationData;
        private System.Windows.Forms.CheckBox ckbPadTrainData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.TextBox txtOutputDir;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnSelectDirectory;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudMinPixelsTrain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudMinPixelsTest;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudMinPixelsValidation;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnOpenPrevScript;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label11;
        private LabelBox outputLabelsSelector;
        private LabelBox domainLabelsSelector;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkGenerateImages;
        private System.Windows.Forms.Label label13;
        private ProjectBox projectSelector;
    }
}