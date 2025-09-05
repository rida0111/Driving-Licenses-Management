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

namespace DVLD.Applications.Local_License.Control
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private  int _LDLApplicationID;

        private clsLocalDrivingLicenseApplication _clsLDLApplication;

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }
     
        private void _FillLicenseInfo()
        {
            ctrlApplicationBasicInfo1.LoadAppLicationInfo(_LDLApplicationID);

            lblDriverLicenseAppID.Text = _clsLDLApplication.LdlApplicationID.ToString();

            lblLicenseClassName.Text = _clsLDLApplication.LicenseClassInfo.ClassName;

            lblPassedTest.Text = _clsLDLApplication.PassedTest().ToString() + "/3";

            llLicenseInfo.Enabled = _clsLDLApplication.IsHasLicense();
        }

        private void _ResetLicenseInfo()
        {
            lblDriverLicenseAppID.Text = "[???]";
            lblLicenseClassName.Text = "[???]";
            lblPassedTest.Text = "[???]";

            llLicenseInfo.Enabled = false;

            ctrlApplicationBasicInfo1.ResetApplicationInfo();
        }

        public void LoadLicenseInfo(int LDLApplicationID)
        {
            _LDLApplicationID = LDLApplicationID;

            _clsLDLApplication = clsLocalDrivingLicenseApplication.FindByID(LDLApplicationID);

            if (_clsLDLApplication != null)
                _FillLicenseInfo();
            else
                _ResetLicenseInfo();
        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            int LicenseID = _clsLDLApplication.LocalLicenseInfo.LicenseId;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);

            frm.ShowDialog();
        }

      
    }
}
