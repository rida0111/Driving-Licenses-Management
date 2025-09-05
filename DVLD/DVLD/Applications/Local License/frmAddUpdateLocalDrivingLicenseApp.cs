using Businesses_Access_Layer;
using DVLD.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.Local_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
       
        clsLocalDrivingLicenseApplication _LDLApplication;

        private  int _LDLApplicationID;
        private enum enMode { AddNew = 0, UpdateNew = 1 }

        private enMode _Mode;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LDLApplicationID)
        {
            InitializeComponent();

            _Mode = enMode.UpdateNew;

            _LDLApplicationID = LDLApplicationID;
        }

        private void _FillcbLicenseClasses()
        {
            DataTable dt = clsLicenseClass.GetAllLicenseClasses();

            foreach (DataRow dr in dt.Rows)
            {
                cbLicenseClass.Items.Add(dr["ClassName"]);
            }
        }

        private bool _IsAgeAllowed()
        {

            int MinimumAge = clsLicenseClass.FindByName(cbLicenseClass.Text.Trim()).MinimumAge;

            int Age = DateTime.Today.Year - ctrlPersonCardWithFilter1.PersonInfo.DateofBirth.Year;

            if (MinimumAge > Age)
            {
                MessageBox.Show($@"Your Age is {Age} and you Should have more than or equal {MinimumAge}."
                     , "Not Allow", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            else
                return true;
        }

        private bool _HandleExistApplicationorApplied()
        {
            int PersonID = ctrlPersonCardWithFilter1.PersonID;

            clsLocalDrivingLicenseApplication clsLdlApplication = new clsLocalDrivingLicenseApplication();

            if (clsLdlApplication.IsApplicationExist(PersonID, (byte)clsApplication.enStatus.New, cbLicenseClass.Text))
            {

                MessageBox.Show($"Choose another License Class,the Selected Person Already have an active application for the selected class with id ={clsLdlApplication.ApplicationID} ", "Error"
                  , MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (clsLdlApplication.IsApplicationExist(PersonID, (byte)clsApplication.enStatus.Completed, cbLicenseClass.Text))
            {

                MessageBox.Show("Person already has a license with the same applied driving class,choose different Driving class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;

        }

        private void _LoadData()
        {

           _LDLApplication = clsLocalDrivingLicenseApplication.FindByID(_LDLApplicationID);

            if (_LDLApplication == null)
                return;

            ctrlPersonCardWithFilter1.LoadPersonInfo(_LDLApplication.ApplicationInfo.PersonID);

            lbText.Text = "Update Local Driving License Application";

            this.Text = "Update Local Driving License Application";

            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(_LDLApplication.LicenseClassInfo.ClassName);

            lblApplicationID.Text = _LDLApplicationID.ToString();

            lblDate.Text = _LDLApplication.ApplicationInfo.ApplicationDate.ToShortDateString();

            lblFess.Text = _LDLApplication.ApplicationInfo.PaidFess.ToString();

            lblUserName.Text = _LDLApplication.ApplicationInfo.UserInfo.UserName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_IsAgeAllowed() || !_HandleExistApplicationorApplied())
                return;

            if (_Mode == enMode.UpdateNew)
            {
                _LDLApplication.LicenseClassID = clsLicenseClass.FindByName(cbLicenseClass.Text.Trim()).LicenseClassID;

                if (_LDLApplication.Save())
                    MessageBox.Show("Data Updated Successfully", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Fail To Save Data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (_Mode == enMode.AddNew)

            {
                int LDLApplicationID = ctrlPersonCardWithFilter1.PersonInfo.AddNewLDLApplication(cbLicenseClass.Text.Trim(), clsGlobal.CurrentUser.UserID);

                if (LDLApplicationID != -1)
                {
                    _LDLApplicationID = LDLApplicationID;

                    _Mode = enMode.UpdateNew;

                    MessageBox.Show("Data Saved Successfully", "Saved"
                        , MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _LoadData();
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
                lbText.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";

                lblUserName.Text = clsGlobal.CurrentUser.UserName;

                lblDate.Text = DateTime.Now.ToShortDateString();

                cbLicenseClass.SelectedIndex = 0;

                lblFess.Text = clsApplicationType.Find(clsApplication.enApplicationType.NewLocalDrivingLicense).Fees.ToString();
                tpApplicationInfo.Enabled = false;   
            }

            else
            {
                btnSave.Enabled = true;          
                ctrlPersonCardWithFilter1.Filter = false;           
                _LoadData();
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (_Mode == enMode.UpdateNew || ctrlPersonCardWithFilter1.PersonID != -1)
            {
                tabControl1.SelectedIndex = 1;
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
            }
            else
                MessageBox.Show("Please Select a Person!", "Select a Person"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }

}
