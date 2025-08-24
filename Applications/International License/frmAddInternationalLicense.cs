using Businesses_Access_Layer;
using DVLD2.Licenses;
using DVLD2.Licenses.International_Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Applications.International_License
{
    public partial class frmAddInternationalLicense : Form
    {
        private static int _LocalLicenseID;

        private clsInternationalLicense _clsinterLicense = new clsInternationalLicense();


        public frmAddInternationalLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _FillApplicationInfo()
        {

            lbInterAppID.Text = _clsinterLicense.ApplicationID.ToString();

            lbIntelicenseID.Text = _clsinterLicense.InterLicenseID.ToString();

            lbApplicationDate.Text = _clsinterLicense.ApplicationInfo.ApplicationDate.ToShortDateString();

            lbIssueDate.Text = _clsinterLicense.IssueDate.ToShortDateString();

            lbExDate.Text = _clsinterLicense.ExpirationDate.ToShortDateString();

            lbCreatedby.Text = clsUserInfo.CurrentUser.UserName;

            lbFees.Text = clsApplicationType.FindByTitle("New International License").Fees.ToString();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license ?", "Confirm"
                     , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            _clsinterLicense = _clsinterLicense.IssueIntenationalLicense(_LocalLicenseID, clsUserInfo.CurrentUser.UserId);

            if (_clsinterLicense != null)
            {
                MessageBox.Show($"International License Issued Successfully with ID={_clsinterLicense.InterLicenseID}"
                    , "license Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);


                _FillApplicationInfo();

                btnIssue.Enabled = false;

                ctrlDriverLicenseInfowithFilter1.Filter = false;

                lbShowLicensesInfo.Enabled = true;
            }
            else
                MessageBox.Show("Failed To Add New International License,Try again! "
                    , "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmAddInternationalLicense_Load(object sender, EventArgs e)
        {
            this.AcceptButton = ctrlDriverLicenseInfowithFilter1.btnSearch;

            lbApplicationDate.Text = DateTime.Now.ToShortDateString();

            lbIssueDate.Text = DateTime.Now.ToShortDateString();

            lbExDate.Text = DateTime.Now.AddYears(1).ToShortDateString();

            lbFees.Text = clsApplicationType.FindByTitle("New International License").Fees.ToString();

            lbCreatedby.Text = clsUserInfo.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _LocalLicenseID = obj;

            lbLocalLicenseID.Text= _LocalLicenseID.ToString();

            clsLocalLicenses LicenseInfo = clsLocalLicenses.FindByID(_LocalLicenseID);

            if (LicenseInfo == null)
            {
                lbShowLicensesHistory.Enabled = false;
                return;
            }
            else
                lbShowLicensesHistory.Enabled = true;


            clsInternationalLicense clsinterLicense = clsInternationalLicense.FindByLocalLicenseID(_LocalLicenseID);

            if (clsinterLicense != null)
            {
                MessageBox.Show($"Person already have an active international license with ID =" +
                    $" {clsinterLicense.InterLicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                lbShowLicensesInfo.Enabled = true;

                return;
            }

            if (!LicenseInfo.IsActive)
                MessageBox.Show("You cannot apply for an international driving license because your license is inactive!"
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            else if (LicenseInfo.ExpirationDate < DateTime.Now)
                MessageBox.Show("You cannot apply for an international driving license because your license is expiration Date!"
                         , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);


            else if (LicenseInfo.LicenseClass != 3)
                MessageBox.Show("You cannot apply for an international driving license because your license is Not Ordinary Driving License!"
                           , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                btnIssue.Enabled = true;
        }

        private void lbShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int InterLicenseID = clsInternationalLicense.FindByLocalLicenseID(_LocalLicenseID).InterLicenseID;


            frmShowInterLicenseInfo frm = new frmShowInterLicenseInfo(InterLicenseID);

            frm.ShowDialog();
        }

        private void lbShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          
            int DriverID = clsLocalLicenses.FindByID(_LocalLicenseID).DriverId;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

            frm.ShowDialog();
        }
    }
}
