using Businesses_Access_Layer;
using DVLD2.Licenses.Local_License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        private static int _LDLApplicationID;


        private void _FillLicenseInfo()
        {
            lbDlAppID.Text = ctrlApplicationBasicInfo1.LDLApplicationInfo.LdlApplicationID.ToString();

            lbLicenseClassName.Text = ctrlApplicationBasicInfo1.LDLApplicationInfo.LicenseClassInfo.ClassName;

            lbPassedTest.Text = clsTest.NumberPassedTests(_LDLApplicationID).ToString() + "/3";

            lbLicenseInfo.Enabled = clsLocalLicenses.IsHasLicense(ctrlApplicationBasicInfo1.LDLApplicationInfo.ApplicationID, ctrlApplicationBasicInfo1.LDLApplicationInfo.LicenceClassID);
        }

        private void _ResetLicenseInfo()
        {
            lbDlAppID.Text = "[???]";
            lbLicenseClassName.Text = "[???]";

            lbPassedTest.Text = "[???]";
            lbLicenseInfo.Enabled = false;
        }

        public void LoadLicenseAppInfo(int LDLApplicatinID)
        {
            _LDLApplicationID = LDLApplicatinID;

            ctrlApplicationBasicInfo1.LoadAppLicationInfo(LDLApplicatinID);

            if (ctrlApplicationBasicInfo1.LDLApplicationInfo != null)
                _FillLicenseInfo();
            else
                _ResetLicenseInfo();
        }

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void lbLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            int LicenseID = ctrlApplicationBasicInfo1.LDLApplicationInfo.LocalLicenseInfo.LicenseId;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);

            frm.ShowDialog();
        }
    }
}
