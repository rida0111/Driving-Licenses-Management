using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Businesses_Access_Layer;

namespace DVLD.Applications.Release_Application
{
    public partial class frmListDetainedLicenses : Form
    {
        private  DataTable _dtDetainedLicenses;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            
            _dtDetainedLicenses = clsDetainLicense.GetAllDetainedLicenses();

            dgvDetainedLicenses.DataSource = _dtDetainedLicenses;

            lblRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();

            if (dgvDetainedLicenses.RowCount > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicenses.Columns[0].Width = 100;

                dgvDetainedLicenses.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicenses.Columns[1].Width = 100;

                dgvDetainedLicenses.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicenses.Columns[2].Width = 160;

                dgvDetainedLicenses.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicenses.Columns[3].Width = 100;

                dgvDetainedLicenses.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicenses.Columns[4].Width = 100;

                dgvDetainedLicenses.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicenses.Columns[5].Width = 160;

                dgvDetainedLicenses.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicenses.Columns[6].Width = 100;

                dgvDetainedLicenses.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicenses.Columns[7].Width = 270;

                dgvDetainedLicenses.Columns[8].HeaderText = "Release App.ID";
                dgvDetainedLicenses.Columns[8].Width = 130;
            }

            cbFilterby.SelectedIndex = 0;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();

            frm.ShowDialog();

            frmListDetainedLicenses_Load(null, null);

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainLicense frm = new frmReleaseDetainLicense();
            frm.ShowDialog();

            frmListDetainedLicenses_Load(null, null);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilterby.Text != "None" && cbFilterby.Text != "IsReleased");

            cbReleased.Visible = (cbFilterby.Text == "IsReleased");

            if (cbReleased.Visible)
            {
                cbReleased.SelectedIndex = 0;
                return;
            }

            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";
                lblRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();
            }
            else
                txtFilter.Text = "";

            txtFilter.Focus();
        }

        private void cbReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterName = "";

            switch (cbReleased.Text)
            {
                case "Yes":
                    FilterName = "1";
                    break;
                case "No":
                    FilterName = "0";
                    break;
                default:
                    FilterName = cbReleased.Text;
                    break;
            }

            if (FilterName == "ALL")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsReleased", FilterName);


            lblRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {


            string ColumnName = "";

            switch (cbFilterby.Text)
            {
                case "Detain.ID":
                    ColumnName = "DetainID";
                    break;
                case "License.ID":
                    ColumnName = "LicenseID";
                    break;
                case "National.No":
                    ColumnName = "NationalNo";
                    break;
                case "Full Name":
                    ColumnName = "FullName";
                    break;
                case "Release Application ID":
                    ColumnName = "ReleaseApplicationID";
                    break;
                default:
                    ColumnName = "None";
                    break;
            }

            if (ColumnName == "None" || txtFilter.Text == "")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else if (ColumnName == "FullName" || ColumnName == "NationalNo")
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, txtFilter.Text);
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, txtFilter.Text);


            lblRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterby.Text == "Detain.ID" || cbFilterby.Text == "License.ID" || cbFilterby.Text == "Release Application ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLocalLicenses.FindByID((int)dgvDetainedLicenses.SelectedCells[1].Value).ApplicationInfo.PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedCells[1].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);

            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedCells[1].Value;

            int PersonID = clsLocalLicenses.FindByID(LicenseID).ApplicationInfo.PersonID;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);

            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedCells[1].Value;

            frmReleaseDetainLicense frm = new frmReleaseDetainLicense(LicenseID);
            frm.ShowDialog();

            frmListDetainedLicenses_Load(null, null);
        }

        private void cmsDetainedLicense_Opening(object sender, CancelEventArgs e)
        {
            bool IsLicenseRelease = (bool)(dgvDetainedLicenses.SelectedCells[3].Value);

            cmsReleaseDLicense.Enabled = !IsLicenseRelease;
        }

    }
}
