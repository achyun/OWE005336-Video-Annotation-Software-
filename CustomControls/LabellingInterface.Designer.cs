namespace OWE005336__Video_Annotation_Software_
{
    partial class LabellingInterface
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pnlMetaData = new System.Windows.Forms.Panel();
            this.dgvLabels = new System.Windows.Forms.DataGridView();
            this.Label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OutOfFocus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Truncated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Occluded = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ROILocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ROISize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbxTags = new OWE005336__Video_Annotation_Software_.TagBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSensorType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.btnSelectLabel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlMetaData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlImage
            // 
            this.pnlImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(0, 0);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(769, 693);
            this.pnlImage.TabIndex = 6;
            // 
            // pnlMetaData
            // 
            this.pnlMetaData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMetaData.Controls.Add(this.dgvLabels);
            this.pnlMetaData.Controls.Add(this.panel1);
            this.pnlMetaData.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlMetaData.Location = new System.Drawing.Point(773, 0);
            this.pnlMetaData.Name = "pnlMetaData";
            this.pnlMetaData.Size = new System.Drawing.Size(384, 693);
            this.pnlMetaData.TabIndex = 4;
            // 
            // dgvLabels
            // 
            this.dgvLabels.AllowUserToAddRows = false;
            this.dgvLabels.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLabels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLabels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLabels.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Label,
            this.OutOfFocus,
            this.Truncated,
            this.Occluded,
            this.ROILocation,
            this.ROISize});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLabels.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLabels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLabels.Enabled = false;
            this.dgvLabels.Location = new System.Drawing.Point(0, 0);
            this.dgvLabels.Name = "dgvLabels";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLabels.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLabels.RowHeadersVisible = false;
            this.dgvLabels.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLabels.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLabels.Size = new System.Drawing.Size(382, 471);
            this.dgvLabels.TabIndex = 0;
            this.dgvLabels.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvLabels_CellBeginEdit);
            // 
            // Label
            // 
            this.Label.HeaderText = "Label";
            this.Label.Name = "Label";
            this.Label.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // OutOfFocus
            // 
            this.OutOfFocus.HeaderText = "Out Of Focus";
            this.OutOfFocus.Name = "OutOfFocus";
            this.OutOfFocus.Width = 65;
            // 
            // Truncated
            // 
            this.Truncated.HeaderText = "Truncated";
            this.Truncated.Name = "Truncated";
            this.Truncated.Width = 65;
            // 
            // Occluded
            // 
            this.Occluded.HeaderText = "Occluded";
            this.Occluded.Name = "Occluded";
            this.Occluded.Width = 65;
            // 
            // ROILocation
            // 
            this.ROILocation.HeaderText = "Location";
            this.ROILocation.Name = "ROILocation";
            this.ROILocation.ReadOnly = true;
            this.ROILocation.Width = 70;
            // 
            // ROISize
            // 
            this.ROISize.HeaderText = "Size";
            this.ROISize.Name = "ROISize";
            this.ROISize.ReadOnly = true;
            this.ROISize.Width = 70;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tbxTags);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbSensorType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 471);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(382, 220);
            this.panel1.TabIndex = 2;
            // 
            // tbxTags
            // 
            this.tbxTags.AutoScroll = true;
            this.tbxTags.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxTags.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbxTags.Enabled = false;
            this.tbxTags.Location = new System.Drawing.Point(5, 91);
            this.tbxTags.Name = "tbxTags";
            this.tbxTags.Size = new System.Drawing.Size(372, 121);
            this.tbxTags.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(5, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tags";
            // 
            // cmbSensorType
            // 
            this.cmbSensorType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbSensorType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSensorType.Enabled = false;
            this.cmbSensorType.FormattingEnabled = true;
            this.cmbSensorType.Location = new System.Drawing.Point(5, 57);
            this.cmbSensorType.Name = "cmbSensorType";
            this.cmbSensorType.Size = new System.Drawing.Size(372, 21);
            this.cmbSensorType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(5, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sensor Type";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtLabel);
            this.panel2.Controls.Add(this.btnSelectLabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(5, 18);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(372, 26);
            this.panel2.TabIndex = 6;
            // 
            // txtLabel
            // 
            this.txtLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLabel.Enabled = false;
            this.txtLabel.Location = new System.Drawing.Point(3, 3);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.ReadOnly = true;
            this.txtLabel.Size = new System.Drawing.Size(341, 20);
            this.txtLabel.TabIndex = 1;
            this.txtLabel.DoubleClick += new System.EventHandler(this.txtLabel_DoubleClick);
            // 
            // btnSelectLabel
            // 
            this.btnSelectLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelectLabel.Enabled = false;
            this.btnSelectLabel.Location = new System.Drawing.Point(344, 3);
            this.btnSelectLabel.Name = "btnSelectLabel";
            this.btnSelectLabel.Size = new System.Drawing.Size(25, 20);
            this.btnSelectLabel.TabIndex = 0;
            this.btnSelectLabel.Text = "...";
            this.btnSelectLabel.UseVisualStyleBackColor = true;
            this.btnSelectLabel.Click += new System.EventHandler(this.btnSelectLabel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(5, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Label";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(769, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 693);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // LabellingInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlImage);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlMetaData);
            this.Name = "LabellingInterface";
            this.Size = new System.Drawing.Size(1157, 693);
            this.pnlMetaData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLabels)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.Panel pnlMetaData;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSensorType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLabels;
        private TagBox tbxTags;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Button btnSelectLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Label;
        private System.Windows.Forms.DataGridViewCheckBoxColumn OutOfFocus;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Truncated;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Occluded;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROILocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn ROISize;
    }
}
