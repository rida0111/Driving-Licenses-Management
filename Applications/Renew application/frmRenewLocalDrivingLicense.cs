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

namespace DVLD2.Applications.Renew_application
{
    public partial class frmRenewLocalDrivingLicense : Form
    {

        private static int _LicenseID;

        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void ResetApplicationInfo()
        {
            lbOldLicenseID.Text = "[???]";

            lbExprationDate.Text = "[???]";

            lbTotalFees.Text = "[???]";

            lbLicenseFees.Text = "[???]";
        }

        private void FillApplicationInfo()
        {
            lbOldLicenseID.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.LicenseId.ToString();

            lbExprationDate.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ExpirationDate.ToShortDateString();

            int LicenseFees = ctrlDriverLicenseInfowithFilter1.LicenseInfo.LicenseClassInfo.ClassFess;

            lbLicenseFees.Text = LicenseFees.ToString();

            lbTotalFees.Text = (LicenseFees + 7).ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _LicenseID = obj;

            btnRenew.Enabled = false;

            lbShowRnewLicense.Enabled = false;

            lbShowLicensesHistory.Enabled = false;

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo == null)
            {
                ResetApplicationInfo();
                return;
            }
            else
            {
                lbShowLicensesHistory.Enabled = true;
                FillApplicationInfo();
            }

            if (!ctrlDriverLicenseInfowithFilter1.LicenseInfo.IsLicenseActive())
            {
                MessageBox.Show(@"You can not renew disactive License", "Not Allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo.ExpirationDate >= DateTime.Now)
            {
                MessageBox.Show($"Selected license is not expaired , it will expire on:" +
                    $"{ctrlDriverLicenseInfowithFilter1.LicenseInfo.ExpirationDate.ToShortDateString()}", "Not Allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnRenew.Enabled = true;
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            this.AcceptButton = ctrlDriverLicenseInfowithFilter1.btnSearch;

            lbApplicationDate.Text = DateTime.Now.ToShortDateString();

            lbIssueDate.Text = DateTime.Now.ToShortDateString();

            lbFees.Text = "7";

            lbCreatedby.Text = clsUserInfo.CurrentUser.UserName;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are Sure you want to Renew This License ?", "Confirm"
             , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLocalLicenses clsLicense = ctrlDriverLicenseInfowithFilter1.LicenseInfo.RenewLocalLicense(clsUserInfo.CurrentUser.UserId, tbNotes.Text.Trim());


            if (clsLicense != null)
            {
                _LicenseID = clsLicense.LicenseId;

                lbShowRnewLicense.Enabled = true;

                btnRenew.Enabled = false;

                ctrlDriverLicenseInfowithFilter1.Filter = false;

                lbRLAppID.Text = clsLicense.ApplicationId.ToString();

                lbRenwlicenseID.Text = clsLicense.LicenseId.ToString();

                tbNotes.Text = clsLicense.Notes;

                MessageBox.Show($"License Renewed Successfully With ID = {clsLicense.LicenseId}"
                        , "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Fail To Renew License!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void lbShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int DriverID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.DriverId;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

            frm.ShowDialog();
        }

        private void lbShowRenewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);

            frm.ShowDialog();
        }
    }
}
