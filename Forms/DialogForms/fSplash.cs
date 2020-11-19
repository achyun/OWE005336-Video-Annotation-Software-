using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace OWE005336__Video_Annotation_Software_
{
    public partial class fSplash : Form
    {
        public bool LoginSuccessful { get; private set; } = false;
        public fSplash()
        {
            InitializeComponent();
            pnlLoginDetails.BackColor = Color.FromArgb(50, Color.Gray);
            pnlDatabaseDetails.BackColor = pnlLoginDetails.BackColor;
            pnlTitle.BackColor = pnlLoginDetails.BackColor;

            if (Properties.Settings.Default.Password != "")
            {
                txtPassword.Text = ToInsecureString(DecryptString(Properties.Settings.Default.Password)); ;
            }
            
            txtServer.Text = Properties.Settings.Default.Server;
            txtDatabase.Text = Properties.Settings.Default.Database;
            txtUsername.Text = Properties.Settings.Default.Username;
            ckbSavePassword.Checked = Properties.Settings.Default.SavePassword;

            // Ensure it is easy to 'logon' by pressing Enter from anywhere on the form
            txtUsername.KeyDown += TxtUsername_KeyDown;
            txtPassword.KeyDown += TxtUsername_KeyDown;
            txtServer.KeyDown += TxtUsername_KeyDown;
            txtDatabase.KeyDown += TxtUsername_KeyDown;
            this.KeyDown += TxtUsername_KeyDown;
        }

        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                lbtLogin_Click(sender, new EventArgs());
            }
        }

        private void lbtLogin_Click(object sender, EventArgs e)
        {
            //LoginSuccessful = true;

            //this.Close();

            //return; 
            if (Program.ImageDatabase.VerifyLoginCredentials(txtServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text))
            {
                Properties.Settings.Default.Server = txtServer.Text;
                Properties.Settings.Default.Database = txtDatabase.Text;
                Properties.Settings.Default.Username = txtUsername.Text;
                if (ckbSavePassword.Checked)
                {
                    Properties.Settings.Default.Password = EncryptString(ToSecureString(txtPassword.Text));
                }
                else { Properties.Settings.Default.Password = ""; }
                
                Properties.Settings.Default.Save();
                LoginSuccessful = true;

                this.Close();
            }
            else
            {
                MessageBox.Show("Check login details and try again", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenMain()
        {
            fMain m = new fMain();
            m.Show();
        }

        private void lbtClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region "Password Security Stuff"
        static byte[] _AdditionalEntropy = Encoding.Unicode.GetBytes("SkyWall");
        private static string EncryptString(SecureString input)
        {
            byte[] encryptedData = ProtectedData.Protect(Encoding.Unicode.GetBytes(ToInsecureString(input)), _AdditionalEntropy, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        private static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), _AdditionalEntropy, DataProtectionScope.CurrentUser);
                return ToSecureString(Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        #endregion
    }
}
