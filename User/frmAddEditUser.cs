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
    public partial class frmAddEditUser : Form
    {

        private clsUser _clsUser = new clsUser();

        private static int _UserID;

        enum enMode { AddNew = 0, UpdateNew = 1 };

        private  enMode _Mode;

        public frmAddEditUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();

            _Mode = enMode.UpdateNew;

            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _UpdateUserInfo(int UserId)
        {

            _clsUser = clsUser.FindByID(UserId);

            if (_clsUser == null)
            {
                MessageBox.Show("User not Found,Please Contact your admin!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _UserID = _clsUser.UserId;

            lbText.Text = "Update User";

            this.Text = "Update User";

            tbUserName.Text = _clsUser.UserName;

            lbUserID.Text = _clsUser.UserId.ToString();

            tbConfirmPassword.Text = _clsUser.Password;

            tbPassword.Text = _clsUser.Password;

            cbIsActive.Checked = (_clsUser.IsActive) ? true : false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valid!, put the mouse over the red icon(s) to see the error"
                    , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _clsUser.PersonId = ctrlPersonCardwithFilter1.PersonID;

            _clsUser.IsActive = (cbIsActive.Checked) ? true : false;

            _clsUser.UserName = tbUserName.Text;

            _clsUser.Password = tbPassword.Text;


            if (_clsUser.Save())
            {

                if (_Mode == enMode.AddNew)
                {

                    MessageBox.Show("Data Saved Successfully !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ctrlPersonCardwithFilter1.Filter = false;

                    _UpdateUserInfo(_clsUser.UserId);

                    _Mode = enMode.UpdateNew;

                }
                else
                    MessageBox.Show("Data Updated Successfully !", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Fail To Add or Update User!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.UpdateNew)
            {
                btnSave.Enabled = true;
                tpLoginInfo.Enabled = true;

                TabControl1.SelectedIndex = 1;
                return;
            }

            if (_Mode == enMode.AddNew)
            {

                clsPerson clsPerson = clsPerson.FindById(ctrlPersonCardwithFilter1.PersonID);

                if (clsPerson == null)
                    MessageBox.Show("Please select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {

                    if (clsUser.IsPersonUser(clsPerson.PersonID))
                    {
                        MessageBox.Show("Selected Person already has a User,choose another one", "Select another Person"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        tpLoginInfo.Enabled = true;
                        TabControl1.SelectedIndex = 1;
                    }

                }

            }

        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;

            if (_Mode == enMode.AddNew)
            {
                clsUser clsUsers = clsUser.FindByName(tbUserName.Text.Trim());

                if (clsUsers != null)
                {
                    e.Cancel = true;

                    errorProvider.SetError(tbUserName, "User Name is Already Exist!,write another one");
                }
                else
                    errorProvider.SetError(tbUserName, "");

                return;
            }

            if (_Mode == enMode.UpdateNew)
            {
                clsUser clsUser = clsUser.FindByID(_UserID);

                if (clsUser.UserName == tbUserName.Text.Trim())
                    errorProvider.SetError(tbUserName, "");
                else
                {
                    clsUser clsUsers = clsUser.FindByName(tbUserName.Text.Trim());

                    if (clsUsers != null)
                    {
                        e.Cancel = true;
                        errorProvider.SetError(tbUserName, "User Name is Already Exist!,write another one");
                    }
                    else
                        errorProvider.SetError(tbUserName, "");
                }

                return;
            }
            else
                errorProvider.SetError(tbUserName, "");

        }

        private bool Text_Validating(object sender, CancelEventArgs e)
        {

            System.Windows.Forms.TextBox textbox = (System.Windows.Forms.TextBox)sender;

            if (string.IsNullOrEmpty(textbox.Text))
            {
                e.Cancel = true;
                errorProvider.SetError(textbox, "cannot be blank");
                return false;
            }
            else
            {
                e.Cancel = false;
                errorProvider.SetError(textbox, "");
                return true;
            }

        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            Text_Validating(sender, e);
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            if (!Text_Validating(sender, e))
                return;

            if (tbConfirmPassword.Text != tbPassword.Text)
            {
                e.Cancel = true;
                errorProvider.SetError(tbConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
                errorProvider.SetError(tbConfirmPassword, "");
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {

            if (_Mode == enMode.AddNew)
            {
                lbText.Text = "Add New User";
                this.Text = "Add New User";

                btnSave.Enabled = false;
                tpLoginInfo.Enabled = false;

               
                this.AcceptButton = ctrlPersonCardwithFilter1.btnSearch;
            }

            if (_Mode == enMode.UpdateNew)
            {
                tpLoginInfo.Enabled = true;

                int PersonID = clsUser.FindByID(_UserID).PersonId;
            
                ctrlPersonCardwithFilter1.LoadPersonInfo(PersonID);
                ctrlPersonCardwithFilter1.Enabled = false;
                
                _UpdateUserInfo(_UserID);
            }

        }
    }
}
