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

namespace DVLD.Applications.Release_Application
{
    public partial class frmReleaseDetainLicense : Form
    {

        private int _SelectedLicenseID = -1;
       
        public frmReleaseDetainLicense()
        {
            InitializeComponent();
        }

        public frmReleaseDetainLicense(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
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

            int ReleaseApplicationID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ReleaseDetainLicense(clsGlobal.CurrentUser.UserID);

            if (ReleaseApplicationID == -1)
            {
                MessageBox.Show($"Fail To Detain This License!", "Fail"
              , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lbReleaseApplicationID.Text = ReleaseApplicationID.ToString();

            MessageBox.Show($"Detained License released Successfully"
                        , "Detained License released", MessageBoxButtons.OK, MessageBoxIcon.Information);

            llLicenseInfo.Enabled = true;

            btnRelease.Enabled = false;

            ctrlDriverLicenseInfowithFilter1.Filter = false;           
        }

        private void _FillReleaseLicenseInfo()
        {
            lbLDetainID.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.DetainInfo.DetainId.ToString();

            lbLDetainDate.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.DetainInfo.DetainDate.ToShortDateString();

            lbLApplicationFees.Text = clsApplicationType.Find(clsApplication.enApplicationType.ReleaseDetainLicense).Fees.ToString();

            lblFineFees.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.DetainInfo.FineFees.ToString();

            lbLTotalFees.Text = (Convert.ToSingle(lbLApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            lbLCreatedby.Text = ctrlDriverLicenseInfowithFilter1.LicenseInfo.DetainInfo.UserInfo.UserName;
        }

        private void ctrlDriverLicenseInfowithFilter1_OnLicenseComplete(int obj)
        {
            _SelectedLicenseID = obj;

            lbLicenseID.Text = _SelectedLicenseID.ToString();

            btnRelease.Enabled = false;

            llLicenseInfo.Enabled = false;

            llLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)
                return;


            if (!ctrlDriverLicenseInfowithFilter1.LicenseInfo.IsDetained)
            {
              
                MessageBox.Show("Selected License is not Detained , choose another one."
                  , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnRelease.Enabled = true;

            _FillReleaseLicenseInfo();
        }

        private void frmReleaseDetainLicense_Load(object sender, EventArgs e)
        {

            if (_SelectedLicenseID!=-1)
            {
                ctrlDriverLicenseInfowithFilter1.LoadLicenseInfo(_SelectedLicenseID);
                ctrlDriverLicenseInfowithFilter1.Filter = false;
            }
        }

        private void linkLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);

            frm.ShowDialog();
        }

        private void LinkLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLicenseInfowithFilter1.LicenseInfo.ApplicationInfo.PersonID;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);

            frm.ShowDialog();
        }

        private void frmReleaseDetainLicense_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfowithFilter1.FilterFocus();
        }
    }
}
