using Businesses_Access_Layer;
using DVLD2.Licenses;
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

namespace DVLD2.Applications.Release_Application
{
    public partial class frmReleaseDetainLicense : Form
    {

        private static int _LicenseID;

        private enum enMode { AddNew = 0, UpdateNew = 1 }

        private enMode _Mode;

        public frmReleaseDetainLicense()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmReleaseDetainLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;

            _Mode = enMode.UpdateNew;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to Release This Detained License ?", "Confirm"
           , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ReleaseApplicationID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ReleaseDetainLicense(clsUserInfo.CurrentUser.UserId);

            if (ReleaseApplicationID != -1)
            {
                lbReleaseApplicationID.Text = ReleaseApplicationID.ToString();

                MessageBox.Show($"Detained License released Successfully"
                            , "Detained License released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                linkLicenseInfo.Enabled = true;

                btnRelease.Enabled = false;

                ctrlDriverLicenseInfowithFilter1.Filter = false;
            }
            else

                MessageBox.Show($"Fail To Detain This License!", "Fail"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void _LoadReleaseLicenseInfo(int LicenseID)
        {
            if (LicenseID == -1)
                _RestReleaseLicenseInfo();
            else
                _FillReleaseLicenseInfo(LicenseID);
        }

        private void _FillReleaseLicenseInfo(int LicenseID)
        {
            clsDetainLicense clsDetainLicense = clsDetainLicense.FindByID(LicenseID);

            lbDetainID.Text = clsDetainLicense.DetainId.ToString();

            lbDetainDate.Text = clsDetainLicense.DetainDate.ToShortDateString();

            lbReleaseFees.Text = "7";

            lbDetainFees.Text = clsDetainLicense.FineFees.ToString();

            lbTotalFees.Text = (clsDetainLicense.FineFees + 7).ToString();

            lbCreatedby.Text = clsDetainLicense.UserInfo.UserName;
        }

        private void _RestReleaseLicenseInfo()
        {
            lbDetainID.Text = "[???]";

            lbDetainDate.Text = "[??/??/????]";

            lbReleaseFees.Text = "[$$$$]";

            lbDetainFees.Text = "[$$$$]";

            lbTotalFees.Text = "[$$$$]";
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _LicenseID = obj;

            lbLicenseID.Text = _LicenseID.ToString();

            btnRelease.Enabled = false;

            linkLicenseInfo.Enabled = false;

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo == null)
            {
                LinkLicenseHistory.Enabled = false;
                return;
            }
            else
                LinkLicenseHistory.Enabled = true;


            if (!clsDetainLicense.IsLicenseDetained(_LicenseID))
            {
                _LoadReleaseLicenseInfo(-1);

                MessageBox.Show("Selected License is not Detained , choose another one."
                  , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnRelease.Enabled = true;
                _LoadReleaseLicenseInfo(_LicenseID);
            }

        }

        private void frmReleaseDetainLicense_Load(object sender, EventArgs e)
        {
            this.AcceptButton = ctrlDriverLicenseInfowithFilter1.btnSearch;

            if (_Mode == enMode.UpdateNew)
            {
                ctrlDriverLicenseInfowithFilter1.LoadLicenseID(_LicenseID);
                ctrlDriverLicenseInfowithFilter1.Filter = false;
            }
        }

        private void linkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);

            frm.ShowDialog();
        }

        private void LinkLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int DriverID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.DriverId;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

            frm.ShowDialog();
        }
    }
}
