using Businesses_Access_Layer;
using DVLD2.Licenses;
using DVLD2.Licenses.International_Licenses;
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

namespace DVLD2.Applications.International_License
{
    public partial class frmListInterlicensesApplication : Form
    {

        private static DataTable _dtInternationalLicense = new DataTable();

        public frmListInterlicensesApplication()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListInterlicensesApplication_Load(object sender, EventArgs e)
        {
            _dtInternationalLicense = clsInternationalLicense.GetAllInternationalLicenses();

            dgvInterLicense.DataSource = _dtInternationalLicense;

            lbRecordsNumber.Text = dgvInterLicense.RowCount.ToString();


            if (dgvInterLicense.RowCount > 0)
            {
                dgvInterLicense.Columns[0].HeaderText = "Int.license ID";
                dgvInterLicense.Columns[0].Width = 100;

                dgvInterLicense.Columns[1].HeaderText = "Application ID";
                dgvInterLicense.Columns[1].Width = 120;

                dgvInterLicense.Columns[2].HeaderText = "Driver ID";
                dgvInterLicense.Columns[2].Width = 100;

                dgvInterLicense.Columns[3].HeaderText = "L.License ID";
                dgvInterLicense.Columns[3].Width = 100;

                dgvInterLicense.Columns[4].HeaderText = "Issue Date";
                dgvInterLicense.Columns[4].Width = 150;

                dgvInterLicense.Columns[5].HeaderText = "Expiration Date";
                dgvInterLicense.Columns[5].Width = 150;

                dgvInterLicense.Columns[6].HeaderText = "Is Active";
                dgvInterLicense.Columns[6].Width = 80;

                dgvInterLicense.RowTemplate.Height = 25;

            }

            cbFilter.SelectedIndex = 0;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsActive.Visible = (cbFilter.Text == "Is Active");

            tbFilter.Visible = (cbFilter.Text != "None" && cbFilter.Text != "Is Active");

            tbFilter.Text = "";

            tbFilter.Focus();

            if (!cbIsActive.Visible)
            {
                _dtInternationalLicense.DefaultView.RowFilter = "";

                lbRecordsNumber.Text = dgvInterLicense.RowCount.ToString();
            }
            else
                cbIsActive.SelectedIndex = 0;
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Value = "";

            switch (cbIsActive.Text)
            {
                case "Yes":
                    Value = "1";
                    break;
                case "No":
                    Value = "0";
                    break;
                default:
                    Value = "";
                    break;
            }

            if (Value == "")
                _dtInternationalLicense.DefaultView.RowFilter = "";
            else
                _dtInternationalLicense.DefaultView.RowFilter = string.Format("[{0}]={1}", "IsActive", Value);

            lbRecordsNumber.Text = dgvInterLicense.RowCount.ToString();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbFilter.Text)
            {
                case "Is Active":
                    ColumnName = "IsActive";
                    break;
                case "inter.License ID":
                    ColumnName = "InternationalLicenseID";
                    break;
                case "L.License ID":
                    ColumnName = "IssuedUsingLocalLicenseID";
                    break;
                case "Driver ID":
                    ColumnName = "DriverID";
                    break;
                case "Application ID":
                    ColumnName = "ApplicationID";
                    break;
                default:
                    ColumnName = "None";
                    break;
            }

            if (tbFilter.Text == "" || ColumnName == "None")
                _dtInternationalLicense.DefaultView.RowFilter = "";
            else
                _dtInternationalLicense.DefaultView.RowFilter = string.Format("[{0}]={1}", ColumnName, tbFilter.Text.Trim());


            lbRecordsNumber.Text = dgvInterLicense.RowCount.ToString();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddInternationalLicense frm = new frmAddInternationalLicense();

            frm.ShowDialog();

            frmListInterlicensesApplication_Load(null, null);
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInterLicense.SelectedCells[0].Value;

            int PersonID = clsInternationalLicense.FindByInterID(InternationalLicenseID).ApplicationInfo.PersonId;

            frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InterLicenseID = (int)dgvInterLicense.SelectedCells[0].Value;

            frmShowInterLicenseInfo frm = new frmShowInterLicenseInfo(InterLicenseID);

            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInterLicense.SelectedCells[2].Value;
            
            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

            frm.ShowDialog();
        }
    }


}
