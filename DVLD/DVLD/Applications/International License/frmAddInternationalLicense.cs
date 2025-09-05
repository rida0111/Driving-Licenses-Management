using Businesses_Access_Layer;
using DVLD.Global;
using System;
using System.Windows.Forms;

namespace DVLD.Applications.International_License
{
    public partial class frmAddInternationalLicense : Form
    {

        private int _LocalLicenseID = -1;

        private int _InternationalLicenseID = -1;
        public frmAddInternationalLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license ?", "Confirm"
                     , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            int ApplicationID = -1;
            int InternationalLicenseID = -1;

            bool IsAdded = ctrlDriverLicenseInfowithFilter1.LicenseInfo.IssueIntenationalLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID, ref InternationalLicenseID);

            if (!IsAdded)
            {
                MessageBox.Show("Failed To Add New International License,Try again! ", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _InternationalLicenseID = InternationalLicenseID;

            lblInterApplicationID.Text = ApplicationID.ToString();
            lblInterlicenseID.Text = InternationalLicenseID.ToString();

            MessageBox.Show($"International License Issued Successfully with ID={InternationalLicenseID}", "license Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ctrlDriverLicenseInfowithFilter1.Filter = false;
            llShowInterLicenseInfo.Enabled = true;
        }

        private void frmAddInternationalLicense_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();

            lblIssueDate.Text = DateTime.Now.ToShortDateString();

            lblExpriationDate.Text = DateTime.Now.AddYears(1).ToShortDateString();

            lblFees.Text = clsApplicationType.Find(clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();

            lblCreatedby.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _LocalLicenseID = obj;

            lblLocalLicenseID.Text = _LocalLicenseID.ToString();

            llShowLicensesHistory.Enabled = (_LocalLicenseID != -1);

            if (_LocalLicenseID == -1)
            {
                return;
            }

          
            clsInternationalLicense clsinterLicense = clsInternationalLicense.FindByLocalLicenseID(_LocalLicenseID);

            if (clsinterLicense != null && clsinterLicense.IsActive)
            {
                MessageBox.Show($"Person already has an active international license with ID =" +
                    $" {clsinterLicense.InterLicenseID}", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _InternationalLicenseID = clsinterLicense.InterLicenseID;
                llShowInterLicenseInfo.Enabled = true;
                btnIssueLicense.Enabled = false;
                return;
            }

            if (!ctrlDriverLicenseInfowithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show("You cannot apply for an international driving license because your license is inactive!"
                   , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("You cannot apply for an international driving license because your license is expired Date!"
                   , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (ctrlDriverLicenseInfowithFilter1.LicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("You cannot apply for an international driving license because your license is Not Ordinary Driving License!"
                         , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnIssueLicense.Enabled = true;
        }

        private void llShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInterLicenseInfo frm = new frmShowInterLicenseInfo(_InternationalLicenseID);

            frm.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ApplicationInfo.PersonID;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);

            frm.ShowDialog();
        }

        private void frmAddInternationalLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfowithFilter1.FilterFocus();
        }


    }

}
