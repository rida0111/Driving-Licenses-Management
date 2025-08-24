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

namespace DVLD2.Licenses.DetainLicense
{
    public partial class frmDetainLicense : Form
    {

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(tbFineFees, "FineFees is Empty !");
            }

            else
                errorProvider1.SetError(tbFineFees, "");
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            this.AcceptButton = ctrFilterLocalLicense1.btnSearch;

            lbDetainDate.Text = DateTime.Now.ToShortDateString();

            lbCreatedby.Text = clsUserInfo.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            int _LicenseID = obj;

            lbLicenseID.Text = _LicenseID.ToString();

            btnDetain.Enabled = false;

            linkLicenseHistory.Enabled = false;

            LinkLShowLicense.Enabled = false;

            if (ctrFilterLocalLicense1.LicenseInfo == null)
                return;
            else
                linkLicenseHistory.Enabled = true;


            if (!ctrFilterLocalLicense1.LicenseInfo.IsActive)
            {
                MessageBox.Show("This License is not active, choose another one."
                     , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsDetainLicense.IsLicenseDetained(_LicenseID))
            {
                MessageBox.Show("Selected License is already Detained, choose another one."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnDetain.Enabled = true;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error"
                  , "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you Sure you want to Detain This License ?", "Confirm"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            int FineFees = Convert.ToInt32(tbFineFees.Text.Trim());

            int DetainLicenseID = ctrFilterLocalLicense1.LicenseInfo.DetainLicense(FineFees, clsUserInfo.CurrentUser.UserId);


            if (DetainLicenseID != -1)
            {
                ctrFilterLocalLicense1.Filter = false;

                LinkLShowLicense.Enabled = true;

                btnDetain.Enabled = false;

                lbDetainID.Text = DetainLicenseID.ToString();

                MessageBox.Show($"License Detained Successfully with ID= {DetainLicenseID}"
                                     , "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show($"Fail To Detain License!", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void linkLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int DriverID = ctrFilterLocalLicense1.LicenseInfo.DriverId;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

            frm.ShowDialog();
        }

        private void LinkLShowLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = ctrFilterLocalLicense1.LicenseInfo.LicenseId;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);

            frm.ShowDialog();
        }
    }



}
