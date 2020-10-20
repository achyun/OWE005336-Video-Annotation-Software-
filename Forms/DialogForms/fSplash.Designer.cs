namespace OWE005336__Video_Annotation_Software_
{
    partial class fSplash
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.imgBackground = new System.Windows.Forms.ImageList(this.components);
            this.ckbSavePassword = new System.Windows.Forms.CheckBox();
            this.pnlLoginDetails = new System.Windows.Forms.Panel();
            this.lbtClose = new OWE005336__Video_Annotation_Software_.LabelButton();
            this.lbtLogin = new OWE005336__Video_Annotation_Software_.LabelButton();
            this.pnlDatabaseDetails = new System.Windows.Forms.Panel();
            this.pnlLoginDetails.SuspendLayout();
            this.pnlDatabaseDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(7, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(188, 20);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(7, 22);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(188, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Database";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(7, 67);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(188, 20);
            this.txtDatabase.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 19);
            this.label4.TabIndex = 7;
            this.label4.Text = "Server";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(7, 22);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(188, 20);
            this.txtServer.TabIndex = 4;
            // 
            // imgBackground
            // 
            this.imgBackground.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgBackground.ImageSize = new System.Drawing.Size(16, 16);
            this.imgBackground.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ckbSavePassword
            // 
            this.ckbSavePassword.AutoSize = true;
            this.ckbSavePassword.BackColor = System.Drawing.Color.Transparent;
            this.ckbSavePassword.Location = new System.Drawing.Point(7, 93);
            this.ckbSavePassword.Name = "ckbSavePassword";
            this.ckbSavePassword.Size = new System.Drawing.Size(99, 17);
            this.ckbSavePassword.TabIndex = 12;
            this.ckbSavePassword.Text = "Save password";
            this.ckbSavePassword.UseVisualStyleBackColor = false;
            // 
            // pnlLoginDetails
            // 
            this.pnlLoginDetails.Controls.Add(this.label1);
            this.pnlLoginDetails.Controls.Add(this.ckbSavePassword);
            this.pnlLoginDetails.Controls.Add(this.txtPassword);
            this.pnlLoginDetails.Controls.Add(this.lbtClose);
            this.pnlLoginDetails.Controls.Add(this.txtUsername);
            this.pnlLoginDetails.Controls.Add(this.lbtLogin);
            this.pnlLoginDetails.Controls.Add(this.label2);
            this.pnlLoginDetails.Location = new System.Drawing.Point(501, 299);
            this.pnlLoginDetails.Name = "pnlLoginDetails";
            this.pnlLoginDetails.Size = new System.Drawing.Size(207, 139);
            this.pnlLoginDetails.TabIndex = 13;
            // 
            // lbtClose
            // 
            this.lbtClose.AutoSize = true;
            this.lbtClose.BackColor = System.Drawing.Color.Transparent;
            this.lbtClose.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.lbtClose.ForeColor = System.Drawing.Color.White;
            this.lbtClose.Location = new System.Drawing.Point(27, 110);
            this.lbtClose.Name = "lbtClose";
            this.lbtClose.Size = new System.Drawing.Size(61, 27);
            this.lbtClose.TabIndex = 11;
            this.lbtClose.Text = "Close";
            this.lbtClose.Click += new System.EventHandler(this.lbtClose_Click);
            // 
            // lbtLogin
            // 
            this.lbtLogin.AutoSize = true;
            this.lbtLogin.BackColor = System.Drawing.Color.Transparent;
            this.lbtLogin.Font = new System.Drawing.Font("Calibri", 16F, System.Drawing.FontStyle.Bold);
            this.lbtLogin.ForeColor = System.Drawing.Color.White;
            this.lbtLogin.Location = new System.Drawing.Point(120, 110);
            this.lbtLogin.Name = "lbtLogin";
            this.lbtLogin.Size = new System.Drawing.Size(60, 27);
            this.lbtLogin.TabIndex = 3;
            this.lbtLogin.Text = "Login";
            this.lbtLogin.Click += new System.EventHandler(this.lbtLogin_Click);
            // 
            // pnlDatabaseDetails
            // 
            this.pnlDatabaseDetails.Controls.Add(this.label4);
            this.pnlDatabaseDetails.Controls.Add(this.txtDatabase);
            this.pnlDatabaseDetails.Controls.Add(this.label3);
            this.pnlDatabaseDetails.Controls.Add(this.txtServer);
            this.pnlDatabaseDetails.Location = new System.Drawing.Point(12, 12);
            this.pnlDatabaseDetails.Name = "pnlDatabaseDetails";
            this.pnlDatabaseDetails.Size = new System.Drawing.Size(199, 92);
            this.pnlDatabaseDetails.TabIndex = 14;
            // 
            // fSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::OWE005336__Video_Annotation_Software_.Properties.Resources.NeuralNet;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(720, 450);
            this.Controls.Add(this.pnlDatabaseDetails);
            this.Controls.Add(this.pnlLoginDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fSplash";
            this.Text = "fSplash";
            this.pnlLoginDetails.ResumeLayout(false);
            this.pnlLoginDetails.PerformLayout();
            this.pnlDatabaseDetails.ResumeLayout(false);
            this.pnlDatabaseDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ImageList imgBackground;
        private LabelButton lbtLogin;
        private LabelButton lbtClose;
        private System.Windows.Forms.CheckBox ckbSavePassword;
        private System.Windows.Forms.Panel pnlLoginDetails;
        private System.Windows.Forms.Panel pnlDatabaseDetails;
    }
}