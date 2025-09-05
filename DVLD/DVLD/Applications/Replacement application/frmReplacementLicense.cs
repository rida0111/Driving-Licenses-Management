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

namespace DVLD.Applications.Replacement_application
{
    public partial class frmReplacementLicense : Form
    {

        private int _NewLicenseID = -1;

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


            clsLocalLicenses clsLicense = ctrlDriverLicenseInfowithFilter1.LicenseInfo.Replace(ApplicationTypeID, clsGlobal.CurrentUser.UserID);

            if (clsLicense == null)
            {
                MessageBox.Show("Fail To Replace License!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _NewLicenseID = clsLicense.LicenseId;

            lblRenewApplicationID.Text = clsLicense.ApplicationId.ToString();

            lblRenewLicenseID.Text = clsLicense.LicenseId.ToString();

            MessageBox.Show($"Licensed Replaced Successfully with ID = {clsLicense.LicenseId}"
                             , "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            gbReplacement.Enabled = false;
            llLicenseInfo.Enabled = true;
            btnIssueReplacement.Enabled = false;
            ctrlDriverLicenseInfowithFilter1.Filter = false;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            float Fees = clsApplicationType.Find(clsApplication.enApplicationType.ReplacementforDamaged).Fees;

            lblFees.Text = Fees.ToString();

            lbReplacementType.Text = "Replacement for a Damaged License";
            this.Text = lbReplacementType.Text;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            float Fees = clsApplicationType.Find(clsApplication.enApplicationType.ReplacementforLost).Fees;

            lblFees.Text = Fees.ToString();

            lbReplacementType.Text = "Replacement for a Lost";
            this.Text = lbReplacementType.Text;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ApplicationInfo.PersonID;

            frmShowLicenseHistory frmLicenseHistory = new frmShowLicenseHistory(PersonID);

            frmLicenseHistory.ShowDialog();
        }

        private void llNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frmLicenseInfo = new frmShowLicenseInfo(_NewLicenseID);

            frmLicenseInfo.ShowDialog();
        }

        private void frmReplacementLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();

            lblCreatedby.Text = clsGlobal.CurrentUser.UserName;

            rbDamagedLicense.Checked = true;
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            int SelectedLicense = obj;

            lblOldLicenseID.Text = SelectedLicense.ToString();

            llLicenseInfo.Enabled = false;

            btnIssueReplacement.Enabled = false;

            llShowLicenseHistory.Enabled = (SelectedLicense != -1);

            if (SelectedLicense == -1)
                return;
            
            if (!ctrlDriverLicenseInfowithFilter1.LicenseInfo.IsLicenseActive())
            {
                MessageBox.Show("Selected License is not active,choose an active license."
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReplacementLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfowithFilter1.FilterFocus();
        }
    }
}
