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

namespace DVLD
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private  int _LocalDrivingLicenseApplicationID;

        private clsLocalDrivingLicenseApplication _clsLocalDrivingLicenseApp;
        public frmIssueDriverLicenseFirstTime(int LDLApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LDLApplicationID;
        }

        private void btnIssue_Click(object sender, System.EventArgs e)
        {

            int LicenseID = _clsLocalDrivingLicenseApp.IssueLicenseForTheFirstTime(txtNote.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
                MessageBox.Show($"License Issued Successfully With License ID ={LicenseID}",
                                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"License was Not Added !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _clsLocalDrivingLicenseApp = clsLocalDrivingLicenseApplication.FindByID(_LocalDrivingLicenseApplicationID);

            if(!_clsLocalDrivingLicenseApp.IsPassedAllTests())
            {
                MessageBox.Show("Person not passed All Tests!", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if(_clsLocalDrivingLicenseApp.IsHasAnActiveLicense())
            {
                MessageBox.Show("Person Already Has An active license!", "Error"
                , MessageBoxButtons.OK, MessageBoxIcon.Error);
              this.Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.LoadLicenseInfo(_LocalDrivingLicenseApplicationID);
        }


    }

}
