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
    public partial class frmAddEditUser : Form
    {
        private clsUser _clsUser;

        private  int _UserID;

        enum enMode { AddNew = 0, UpdateNew = 1 };

        private enMode _Mode;


        public frmAddEditUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();    
            _UserID = UserID;

            _Mode = enMode.UpdateNew;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillUserInfo(int UserId)
        {

            _clsUser = clsUser.FindByID(UserId);

            if (_clsUser == null)
            {
                MessageBox.Show("User not Found,Please Contact your admin!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _UserID = _clsUser.UserID;

            lbText.Text = "Update User";

            this.Text = "Update User";

            txtUserName.Text = _clsUser.UserName;

            lblUserID.Text = _clsUser.UserID.ToString();

            txtConfirmPassword.Text = _clsUser.Password;

            txtPassword.Text = _clsUser.Password;

            checkbIsActive.Checked = _clsUser.IsActive ? true : false;

            ctrlPersonCardwithFilter1.Filter = false;
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

            _clsUser.IsActive = (checkbIsActive.Checked) ? true : false;

            _clsUser.UserName = txtUserName.Text;

            _clsUser.Password = txtPassword.Text;


            if (_clsUser.Save())
            {
                MessageBox.Show("Data Saved Successfully !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
         
                _FillUserInfo(_clsUser.UserID);

                _Mode = enMode.UpdateNew;               
            }
            else
                MessageBox.Show("Fail To Add or Update User!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.UpdateNew)
            {
                btnSave.Enabled = true;
                tcLoginInfo.Enabled = true;

                tcPersonandLoginInfo.SelectedTab = tcLoginInfo;
                return;
            }

            if (ctrlPersonCardwithFilter1.PersonID != -1)
            {
                if (clsUser.IsPersonUser(ctrlPersonCardwithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already has a User,choose another one", "Select another Person"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                btnSave.Enabled = true;
                tcLoginInfo.Enabled = true;
                tcPersonandLoginInfo.SelectedIndex = 1;               
            }
            else
                MessageBox.Show("Please select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (!Text_Validating(sender, e))
                return;

            clsUser clsUser = clsUser.FindByName(txtUserName.Text.Trim());

            if (clsUser != null && _clsUser.UserName != clsUser.UserName)
            {
                e.Cancel = true;
                errorProvider.SetError(txtUserName, "User Name is Already Exist!,write another one");
            }
            else
                errorProvider.SetError(txtUserName, "");
       
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

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            Text_Validating(sender, e);
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            if (!Text_Validating(sender, e))
                return;

            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                e.Cancel = true;
                errorProvider.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
                errorProvider.SetError(txtConfirmPassword, "");
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {

            if (_Mode == enMode.AddNew)
            {
                _clsUser = new clsUser();
                
                lbText.Text = "Add New User";
                this.Text = "Add New User";

                btnSave.Enabled = false;
                tcLoginInfo.Enabled = false;

            }

            if (_Mode == enMode.UpdateNew)
            {
                tcLoginInfo.Enabled = true;  
                int PersonID = clsUser.FindByID(_UserID).PersonId;

                ctrlPersonCardwithFilter1.LoadPersonInfo(PersonID);          
                _FillUserInfo(_UserID);
            }

        }

        private void frmAddEditUser_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardwithFilter1.FilterFocus();
        }
    }

}

