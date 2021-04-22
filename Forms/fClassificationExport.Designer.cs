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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtSQL = new System.Windows.Forms.TextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
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
            this.label4 = new System.Windows.Forms.Label();
            this.nudMinPixels = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValidPct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixels)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSQL);
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
            this.txtSQL.Location = new System.Drawing.Point(0, 124);
            this.txtSQL.Multiline = true;
            this.txtSQL.Name = "txtSQL";
            this.txtSQL.Size = new System.Drawing.Size(828, 463);
            this.txtSQL.TabIndex = 1;
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
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.nudMinPixels);
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
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::OWE005336__Video_Annotation_Software_.Properties.Resources.OkTick;
            this.btnSave.Location = new System.Drawing.Point(774, 17);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 41);
            this.btnSave.TabIndex = 2;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            this.lblSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSummary.Location = new System.Drawing.Point(3, 3);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(372, 115);
            this.lblSummary.TabIndex = 13;
            this.lblSummary.Text = "label4";
            // 
            // txtOutputDir
            // 
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Output Directory";
            // 
            // nudMinPixels
            // 
            this.nudMinPixels.Location = new System.Drawing.Point(469, 3);
            this.nudMinPixels.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudMinPixels.Name = "nudMinPixels";
            this.nudMinPixels.Size = new System.Drawing.Size(63, 20);
            this.nudMinPixels.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(533, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "pixels";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(384, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Min Image Size";
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
            this.Text = "fClassificationExport";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrainPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTestPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudValidPct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinPixels)).EndInit();
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
        private System.Windows.Forms.NumericUpDown nudMinPixels;
    }
}