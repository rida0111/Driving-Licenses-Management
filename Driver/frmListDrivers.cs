using Businesses_Access_Layer;
using DVLD2.Licenses;
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

namespace DVLD2.Driver
{
    public partial class frmListDrivers : Form
    {

        private static DataTable _dtDrivers = new DataTable();

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbDriver.Text == "Driver ID" || cbDriver.Text == "Person ID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilter.Visible = (cbDriver.Text != "None");

            tbFilter.Text = "";

            tbFilter.Focus();

            _dtDrivers.DefaultView.RowFilter = "";

            lbRecordsNumber.Text = dgvListDrivers.RowCount.ToString();

        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbDriver.Text)
            {
                case "Driver ID":
                    ColumnName = "DriverID";
                    break;
                case "Person ID":
                    ColumnName = "PersonID";
                    break;
                case "National No":
                    ColumnName = "NationalNo";
                    break;
                case "Full Name":
                    ColumnName = "FullName";
                    break;
                default:
                    ColumnName = cbDriver.Text;
                    break;
            }

            if (ColumnName == "None" || tbFilter.Text == "")
                _dtDrivers.DefaultView.RowFilter = "";
            else if (ColumnName == "DriverID" || ColumnName == "PersonID")
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}]={1}", ColumnName, tbFilter.Text.Trim());
            else
                _dtDrivers.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, tbFilter.Text.Trim());

            lbRecordsNumber.Text = dgvListDrivers.RowCount.ToString();

        }

        private void ListDrivers_Load(object sender, EventArgs e)
        {
            _dtDrivers = clsDriver.GetAllDrivers();

            dgvListDrivers.DataSource = _dtDrivers;

            lbRecordsNumber.Text = dgvListDrivers.RowCount.ToString();

            if (dgvListDrivers.RowCount > 0)
            {
                dgvListDrivers.Columns[0].HeaderText = "DriverID";
                dgvListDrivers.Columns[0].Width = 100;

                dgvListDrivers.Columns[1].HeaderText = "PersonID";
                dgvListDrivers.Columns[1].Width = 100;

                dgvListDrivers.Columns[2].HeaderText = "NationalNo";
                dgvListDrivers.Columns[2].Width = 100;

                dgvListDrivers.Columns[3].HeaderText = "FullName";
                dgvListDrivers.Columns[3].Width = 280;

                dgvListDrivers.Columns[4].HeaderText = "CreatedDate";
                dgvListDrivers.Columns[4].Width = 150;

                dgvListDrivers.Columns[5].HeaderText = "NumberOfActiveLicenses";
                dgvListDrivers.Columns[5].Width = 170;

            }

            cbDriver.SelectedIndex = 0;
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvListDrivers.SelectedCells[1].Value;

            frmPersonDetails frm = new frmPersonDetails(PersonID);

            frm.ShowDialog();
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvListDrivers.SelectedCells[0].Value;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

            frm.ShowDialog();
        }

        private void issueInterLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This property is not implemented yet !", "Not Implemented"
              , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }


}
