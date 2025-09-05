using Businesses_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using DVLD.Global;

namespace DVLD.Login
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindUserByUserIDandPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User != null)
            {

                if (chkRememberMe.Checked)
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                else
                    clsGlobal.RememberUsernameAndPassword("", "");


                if (!User.IsActive)
                {
                    txtUserName.Focus();

                    MessageBox.Show("your Account is deactivated,Please Contact your admin.", "Wrong Credintials",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = User;

                this.Hide();
                frmMain frm = new frmMain(this);

                frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();

                MessageBox.Show("Invalid UserName/PassWord", "Wrong Credintials",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLoginScreen_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
                chkRememberMe.Checked = false;

        }


    }
}
