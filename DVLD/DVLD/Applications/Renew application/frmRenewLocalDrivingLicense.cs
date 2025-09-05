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

namespace DVLD.Applications.Renew_application
{
    public partial class frmRenewLocalDrivingLicense : Form
    {
        private  int _LicenseID = -1;

        public frmRenewLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void ResetApplicationInfo()
        {
            lblOldLicenseID.Text = "[???]";

            lblExpirationDate.Text = "[???]";

            lblTotalFees.Text = "[???]";

            lblLicenseFees.Text = "[???]";
        }

        private void FillApplicationInfo()
        {
            lblOldLicenseID.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.LicenseId.ToString();

            lblExpirationDate.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ExpirationDate.ToShortDateString();

            int LicenseFees = ctrlDriverLicenseInfowithFilter1.LicenseInfo.LicenseClassInfo.ClassFess;

            lblLicenseFees.Text = LicenseFees.ToString();

            lblTotalFees.Text = (LicenseFees + 7).ToString();

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo.Notes != "")
                txtNotes.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.Notes;
            else
                txtNotes.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _LicenseID = obj;

            btnRenewLicense.Enabled = false;

            llShowRnewLicense.Enabled = false;

            llShowLicensesHistory.Enabled = (_LicenseID != -1);

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo == null)
            {
                ResetApplicationInfo();
                return;
            }
            else              
                FillApplicationInfo();
            

            if (!ctrlDriverLicenseInfowithFilter1.LicenseInfo.IsLicenseActive())
            {
                MessageBox.Show(@"You can not renew disactive License", "Not Allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo.ExpirationDate >= DateTime.Now)
            {
                MessageBox.Show($"Selected license is not expaired , it will expire on:" +
                    $"{ctrlDriverLicenseInfowithFilter1.LicenseInfo.ExpirationDate.ToShortDateString()}", "Not Allowed"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnRenewLicense.Enabled = false;
                return;
            }

            btnRenewLicense.Enabled = true;
        }

        private void frmRenewLocalDrivingLicense_Load(object sender, EventArgs e)
        {

            lblApplicationDate.Text = DateTime.Now.ToShortDateString();

            lblIssueDate.Text = DateTime.Now.ToShortDateString();

            lblFees.Text = clsApplicationType.Find(clsApplication.enApplicationType.RenewLicense).Fees.ToString();

            lblCreatedby.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are Sure you want to Renew This License ?", "Confirm"
             , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            clsLocalLicenses clsLicense = ctrlDriverLicenseInfowithFilter1.LicenseInfo.RenewLocalLicense(clsGlobal.CurrentUser.UserID, txtNotes.Text.Trim());

            if(clsLicense == null)
            {
                MessageBox.Show("Fail To Renew License!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LicenseID = clsLicense.LicenseId;       
            lblRenewLicenseApplicationID.Text = clsLicense.ApplicationId.ToString();
            lblRenwlicenseID.Text = clsLicense.LicenseId.ToString();

            MessageBox.Show($"License Renewed Successfully With ID = {clsLicense.LicenseId}"
                    , "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            llShowRnewLicense.Enabled = true;
            btnRenewLicense.Enabled = false;
            ctrlDriverLicenseInfowithFilter1.Filter = false;
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ApplicationInfo.PersonID;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);

            frm.ShowDialog();
        }

        private void llShowRenewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_LicenseID);

            frm.ShowDialog();
        }

        private void frmRenewLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfowithFilter1.FilterFocus();
        }


    }

}
