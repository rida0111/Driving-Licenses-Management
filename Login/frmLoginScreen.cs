using Businesses_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Login
{
    public partial class frmLoginScreen : Form
    {
        public frmLoginScreen()
        {
            InitializeComponent();
        }


        private void TextFile(string Content)
        {
            string FilePath = @"C:\Users\Admin\OneDrive\Documents\UserInfo.txt";
          
            File.WriteAllText(FilePath, Content);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindUserByUserIDandPassword(tbUserName.Text.Trim(), tbPassword.Text.Trim());

            if (User != null)
            {

                if (cbRememberMe.Checked)
                    TextFile(tbUserName.Text.Trim() + "," + tbPassword.Text.Trim());
                else
                    TextFile("");

                if (!User.IsActive)
                {
                    tbUserName.Focus();

                    MessageBox.Show("your Account is deactivated,Please Contact your admin.", "Wrong Credintials",
                     MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsUserInfo.CurrentUser = User;

                frmMain frm = new frmMain(this);

                frm.ShowDialog();
            }
            else
            {
                tbUserName.Focus();

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
            string FilePath = @"C:\Users\Admin\OneDrive\Documents\UserInfo.txt";


            string[] Content = File.ReadAllLines(FilePath);


            if (Content.Length > 0)
            {
                string[] entry = Content[0].Split(',');

                tbUserName.Text = entry[0];
                tbPassword.Text = entry[1];

                cbRememberMe.Checked = true;
            }

        }
    }
}
