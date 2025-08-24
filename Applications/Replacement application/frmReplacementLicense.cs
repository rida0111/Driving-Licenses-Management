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
using static Businesses_Access_Layer.clsLocalLicenses;

namespace DVLD2.Applications.Replacement_application
{
    public partial class frmReplacementLicense : Form
    {

        private static int _LicenseID;

        public frmReplacementLicense()
        {
            InitializeComponent();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            byte ApplicationTypeID = 0;

            if (MessageBox.Show("Are you Sure you want to Issue a Replacement for the License ?"
             , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (rbDamagedLicense.Checked)
                ApplicationTypeID = (byte)clsLocalLicenses.enIssueReason.ReplacementforDamaged;
            else
                ApplicationTypeID = (byte)clsLocalLicenses.enIssueReason.Replacementforlost;


            clsLocalLicenses clsLicense = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ReplacementLicense(ApplicationTypeID, clsUserInfo.CurrentUser.UserId);

            if (clsLicense != null)
            {
                _LicenseID = clsLicense.LicenseId;

                gbReplacement.Enabled = false;

                LinklNewLicenseInfo.Enabled = true;

                btnIssueReplacement.Enabled = false;

                ctrlDriverLicenseInfowithFilter1.Filter = false;

                lbRenewApplicationID.Text = clsLicense.ApplicationId.ToString();

                lbRenewLicenseID.Text = clsLicense.LicenseId.ToString();

                MessageBox.Show($"Licensed Replaced Successfully with ID = {clsLicense.LicenseId}"
                                 , "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Fail To Replace License!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            int Fees = clsApplicationType.FindByID((int)clsApplication.enApplicationType.ReplacementforDamaged).Fees;

            lbFees.Text = Fees.ToString();

            lbReplacementName.Text = "Replacement for a Damaged License";

            this.Text = "Replacement for a Damaged";
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            int Fees = clsApplicationType.FindByID((int)clsApplication.enApplicationType.ReplacementforLost).Fees;

            lbFees.Text = Fees.ToString();

            lbReplacementName.Text = "Replacement for a Lost";
            this.Text = "Replacement for a Lost";
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int DriverID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.DriverId;

            frmShowLicenseHistory frmLicenseHistory = new frmShowLicenseHistory(DriverID);

            frmLicenseHistory.ShowDialog();
        }

        private void LinklNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frmLicenseInfo = new frmShowLicenseInfo(_LicenseID);

            frmLicenseInfo.ShowDialog();
        }

        private void frmReplacementLicense_Load(object sender, EventArgs e)
        {
            this.AcceptButton = ctrlDriverLicenseInfowithFilter1.btnSearch;

            int Fees = clsApplicationType.FindByID((int)clsApplication.enApplicationType.ReplacementforDamaged).Fees;

            lbFees.Text = Fees.ToString();

            lbApplicationDate.Text = DateTime.Now.ToShortDateString();

            lbCreatedby.Text = clsUserInfo.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _LicenseID = obj;

            lbOldLicenseID.Text = _LicenseID.ToString();

            LinklNewLicenseInfo.Enabled = false;

            btnIssueReplacement.Enabled = false;

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo == null)
            {
                llShowLicenseHistory.Enabled = false;

                return;
            }

            if (!ctrlDriverLicenseInfowithFilter1.LicenseInfo.IsLicenseActive())
            {
                MessageBox.Show("Selected License is not active,choose an active license."
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                btnIssueReplacement.Enabled = true;


            llShowLicenseHistory.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
