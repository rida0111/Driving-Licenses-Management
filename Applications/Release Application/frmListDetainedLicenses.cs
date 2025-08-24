using Businesses_Access_Layer;
using DVLD2.Licenses;
using DVLD2.Licenses.DetainLicense;
using DVLD2.Licenses.Local_License;
using DVLD2.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Applications.Release_Application
{
    public partial class frmListDetainedLicenses : Form
    {

        private static DataTable _dtDetainedLicenses = new DataTable();

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

            lbRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();

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

            cbFilter.SelectedIndex = 0;
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
            tbFilter.Visible = (cbFilter.Text != "None" && cbFilter.Text != "IsReleased");

            cbReleased.Visible = (cbFilter.Text == "IsReleased");

            tbFilter.Text = "";

            tbFilter.Focus();

            if (!cbReleased.Visible)
            {
                _dtDetainedLicenses.DefaultView.RowFilter = "";

                lbRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();
            }
            else
                cbReleased.SelectedIndex = 0;
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


            lbRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {


            string ColumnName = "";

            switch (cbFilter.Text)
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

            if (ColumnName == "None" || tbFilter.Text == "")
                _dtDetainedLicenses.DefaultView.RowFilter = "";
            else if (ColumnName == "FullName" || ColumnName == "NationalNo")
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, tbFilter.Text);
            else
                _dtDetainedLicenses.DefaultView.RowFilter = string.Format("[{0}] = {1}", ColumnName, tbFilter.Text);


            lbRecordsNumber.Text = dgvDetainedLicenses.RowCount.ToString();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Detain.ID" || cbFilter.Text == "License.ID" || cbFilter.Text == "Release Application ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicenses.SelectedCells[1].Value;

            int PersonID = clsLocalLicenses.FindByID(LicenseID).ApplicationInfo.PersonId;

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

            int DriverID = clsLocalLicenses.FindByID(LicenseID).DriverId;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

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
