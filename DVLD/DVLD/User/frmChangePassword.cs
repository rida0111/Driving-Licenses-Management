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

namespace DVLD.User
{
    public partial class frmChangePassword : Form
    {

        private clsUser _clsUser;

        private int _UserID;

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void _RestPasswordInfo()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error"
                    , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _clsUser.Password = txtConfirmPassword.Text;

            if (_clsUser.Save())
            {
                MessageBox.Show("Data Saved Successfully !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RestPasswordInfo();
            }          
            else
                MessageBox.Show("Fail To Save Data !", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool Text_Validating(object sender, CancelEventArgs e)
        {

            System.Windows.Forms.TextBox temp = (TextBox)sender;

            if (string.IsNullOrEmpty(temp.Text.Trim()))
            {
                e.Cancel = true;

                errorProvider1.SetError(temp, temp.Tag + " cannot be blank!");
                return false;
            }
            else
            {
                errorProvider1.SetError(temp, "");
                return true;
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);

            _clsUser = ctrlUserCard1.UserInfo;
            txtCurrentPassword.Focus();
        }
        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;

            if (txtCurrentPassword.Text != _clsUser.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is Wrong!");
            }

        }
        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;

            if (txtConfirmPassword.Text != txtNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match New Password!");
            }
        }
        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;
        }

    
    }
}
