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

namespace DVLD2.Applications.Local_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApp : Form
    {

        private static int _PersonID;

        private static int _LDLApplicationID;
        private enum enMode { AddNew = 0, UpdateNew = 1 }

        private enMode _Mode;

        public frmAddUpdateLocalDrivingLicenseApp()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApp(int LDLApplicationID)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;

            _Mode = enMode.UpdateNew;
        }

        private void _FillcbLicenseClasses()
        {

            DataTable dt = clsLicenseClass.GetClassName();

            foreach (DataRow dr in dt.Rows)
            {
                cbLicenseClass.Items.Add(dr["ClassName"]);
            }

        }

        private bool IsAgeAllowed()
        {

            int MinimumAge = clsLicenseClass.FindByName(cbLicenseClass.Text.Trim()).MinimumAge;

            int Age = DateTime.Today.Year - ctrlPersonCardwithFilter1.PersonInfo.DateofBirth.Year;

            if (MinimumAge > Age)
            {
                MessageBox.Show($@"Your Age is {Age} and you Should have more than or equal {MinimumAge}."
                     , "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            else
                return true;
        }

        private bool HandleExistApplicationorApplied()
        {
            _PersonID = ctrlPersonCardwithFilter1.PersonInfo.PersonID;

            clsLDLApplication clsLdlApplication = new clsLDLApplication();

            if (clsLdlApplication.IsApplicationExist(_PersonID, (byte)clsApplication.enStatus.New, cbLicenseClass.Text))
            {

                MessageBox.Show($"Choose another License Class,the Selected Person" +
                   $" Already have an active application for the selected class with id ={clsLdlApplication.ApplicationID} ", "Error"
                  , MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (clsLdlApplication.IsApplicationExist(_PersonID, (byte)clsApplication.enStatus.Completed, cbLicenseClass.Text))
            {

                MessageBox.Show(@"Person already have a license with the same applied driving
                            class,choose diffrent Driving class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;

        }

        private void UpdateApplicationInfo()
        {

            clsLDLApplication LDLApplication = clsLDLApplication.FindByID(_LDLApplicationID);

            if (LDLApplication == null)
                return;

            lbTitle.Text = "Update Local Driving License Application";

            this.Text = "Update Local Driving License Application";

            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(LDLApplication.LicenseClassInfo.ClassName);

            lbApplicationID.Text = _LDLApplicationID.ToString();

            lbDate.Text = LDLApplication.ApplicationInfo.ApplicationDate.ToShortDateString();

            lbFess.Text = LDLApplication.ApplicationInfo.PaidFess.ToString();

            lbUserName.Text = LDLApplication.ApplicationInfo.UserInfo.UserName;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsAgeAllowed() || !HandleExistApplicationorApplied())
                return;


            if (_Mode == enMode.UpdateNew)
            {
                clsLDLApplication LdLApplication = clsLDLApplication.FindByID(_LDLApplicationID);

                LdLApplication.LicenceClassID = clsLicenseClass.FindByName(cbLicenseClass.Text.Trim()).LicenseClassID;

                if (LdLApplication.Save())
                    MessageBox.Show("Data Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Fail To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (_Mode == enMode.AddNew)

            {
                int LDLApplicationID = ctrlPersonCardwithFilter1.PersonInfo.AddNewLDLApplication(cbLicenseClass.Text.Trim(), clsUserInfo.CurrentUser.UserId);

                if (LDLApplicationID != -1)
                {
                    _LDLApplicationID = LDLApplicationID;

                    _Mode = enMode.UpdateNew;

                    MessageBox.Show("Data Saved Successfully", "Saved"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);

                    UpdateApplicationInfo();
                }
                else
                    MessageBox.Show("Fail To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAddUpdateLocalDrivingLicenseApp_Load(object sender, EventArgs e)
        {
            _FillcbLicenseClasses();


            if (_Mode == enMode.AddNew)
            {
                lbTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";

                lbUserName.Text = clsUserInfo.CurrentUser.UserName;

                lbDate.Text = DateTime.Now.ToShortDateString();

                this.AcceptButton = ctrlPersonCardwithFilter1.btnSearch;

                btnSave.Enabled = false;

                cbLicenseClass.SelectedIndex = 0;

                tpApplicationInfo.Enabled = false;

                ctrlPersonCardwithFilter1.Filter = true;
            }

            else
            {
                btnSave.Enabled = true;

                _PersonID = clsLDLApplication.FindByID(_LDLApplicationID).ApplicationInfo.PersonId;

                ctrlPersonCardwithFilter1.LoadPersonInfo(_PersonID);

                UpdateApplicationInfo();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.UpdateNew)
            {
                tabControl1.SelectedIndex = 1;

                tpApplicationInfo.Enabled = true;

                ctrlPersonCardwithFilter1.Filter = false;

                btnSave.Enabled = true;

                return;
            }

            if (_Mode == enMode.AddNew)
            {

                if (ctrlPersonCardwithFilter1.PersonInfo != null)
                {
                    tabControl1.SelectedIndex = 1;

                    btnSave.Enabled = true;

                    tpApplicationInfo.Enabled = true;
                }
                else
                    MessageBox.Show("Please Select a Person!", "Select a Person"
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
