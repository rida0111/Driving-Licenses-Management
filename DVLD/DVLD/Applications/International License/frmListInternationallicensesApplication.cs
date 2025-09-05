using Businesses_Access_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.International_License
{
    public partial class frmListInternationallicensesApplication : Form
    {

        private  DataTable _dtInternationalLicense;

        public frmListInternationallicensesApplication()
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

            dgvInternationalLicense.DataSource = _dtInternationalLicense;

            lblRecordsNumber.Text = dgvInternationalLicense.RowCount.ToString();


            if (dgvInternationalLicense.RowCount > 0)
            {
                dgvInternationalLicense.Columns[0].HeaderText = "Int.license ID";
                dgvInternationalLicense.Columns[0].Width = 100;

                dgvInternationalLicense.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicense.Columns[1].Width = 120;

                dgvInternationalLicense.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicense.Columns[2].Width = 100;

                dgvInternationalLicense.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicense.Columns[3].Width = 100;

                dgvInternationalLicense.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicense.Columns[4].Width = 150;

                dgvInternationalLicense.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicense.Columns[5].Width = 150;

                dgvInternationalLicense.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicense.Columns[6].Width = 80;

                dgvInternationalLicense.RowTemplate.Height = 25;

            }

            cbFilterby.SelectedIndex = 0;
        }

        private void txtFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsActive.Visible = (cbFilterby.Text == "Is Active");

            txtFilter.Visible = (cbFilterby.Text != "None" && cbFilterby.Text != "Is Active");

            txtFilter.Text = "";

            txtFilter.Focus();

            if (cbIsActive.Visible)
            {
                cbIsActive.SelectedIndex = 0;
                return;
            }

            _dtInternationalLicense.DefaultView.RowFilter = "";
            lblRecordsNumber.Text = dgvInternationalLicense.RowCount.ToString();      
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

            lblRecordsNumber.Text = dgvInternationalLicense.RowCount.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbFilterby.Text)
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

            if (txtFilter.Text == "" || ColumnName == "None")
                _dtInternationalLicense.DefaultView.RowFilter = "";
            else
                _dtInternationalLicense.DefaultView.RowFilter = string.Format("[{0}]={1}", ColumnName, txtFilter.Text.Trim());


            lblRecordsNumber.Text = dgvInternationalLicense.RowCount.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
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
           
            int PersonID = clsInternationalLicense.FindByInterID((int)dgvInternationalLicense.SelectedCells[0].Value).PersonID;

            frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InterLicenseID = (int)dgvInternationalLicense.SelectedCells[0].Value;

           frmShowInterLicenseInfo frm = new frmShowInterLicenseInfo(InterLicenseID);

           frm.ShowDialog();
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
           int ApplicationID = (int)dgvInternationalLicense.SelectedCells[1].Value;

            int PersonID = clsApplication.FindByID(ApplicationID).PersonID;

           frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);

            frm.ShowDialog();
        }

    
    }
}
