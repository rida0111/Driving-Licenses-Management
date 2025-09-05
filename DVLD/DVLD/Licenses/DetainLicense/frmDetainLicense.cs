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

namespace DVLD
{
    public partial class frmDetainLicense : Form
    {
        private int _SelectedLicenseID = -1;

        public frmDetainLicense()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "FineFees is Empty !");
            }
            else
                errorProvider1.SetError(txtFineFees, "");
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToShortDateString();

            lblCreatedby.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            btnDetain.Enabled = false;
            llLicenseHistory.Enabled = (_SelectedLicenseID != -1);
            llShowLicenseInfo.Enabled = false;

            if (_SelectedLicenseID == -1)
                return;
 
            if (!ctrFilterLocalLicense1.LicenseInfo.IsActive)
            {
                MessageBox.Show("This License is not active, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ctrFilterLocalLicense1.LicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License is already Detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFineFees.Focus();
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


            float FineFees = Convert.ToSingle(txtFineFees.Text.Trim());

            int DetainLicenseID = ctrFilterLocalLicense1.LicenseInfo.DetainLicense(FineFees, clsGlobal.CurrentUser.UserID);

            if (DetainLicenseID == -1)
            {
                MessageBox.Show($"Failed To Detain License!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ctrFilterLocalLicense1.Filter = false;
            llShowLicenseInfo.Enabled = true;
            btnDetain.Enabled = false;

            txtFineFees.Enabled = false;
            lblDetainID.Text = DetainLicenseID.ToString();

            MessageBox.Show($"License Detained Successfully with ID= {DetainLicenseID}" , "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);             
        }

        private void llLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrFilterLocalLicense1.LicenseInfo.ApplicationInfo.PersonID;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);

            frm.ShowDialog();
        }

        private void llShowLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {         
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);

            frm.ShowDialog();
        }

        private void frmDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrFilterLocalLicense1.FilterFocus();
        }

    }
}
