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

namespace DVLD2.User
{
    public partial class frmChangePassword : Form
    {
        private static clsUser _clsUser;

        private static int _UserID;
  
        public frmChangePassword(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error"
                    , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_clsUser == null)
                return;

            _clsUser.Password = txtConfirmPass.Text;

            if (_clsUser.Save())
                MessageBox.Show("Data Saved Successfully !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;

            _clsUser = clsUser.FindByID(_UserID);

            if (txtCurrentPassword.Text != _clsUser.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "Current Password is Wrong!");
            }

        }

        private void txtConfirmPass_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;

            if (txtConfirmPass.Text != txtNewPass.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPass, "Password Confirmation does not match New Password!");
            }
        }
        private void txtNewPass_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;
        }

    }
}
