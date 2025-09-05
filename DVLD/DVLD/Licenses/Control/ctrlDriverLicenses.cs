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

namespace DVLD
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private  DataTable _dtDriverLocalLicenses;

        private  DataTable _dtDriverInternationalLicenses;

        private int _DriverID = -1;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }
        private void _LoadLocalDrivingLicenses()
        {
            _dtDriverLocalLicenses = clsLocalLicenses.GetSpecificLocalLicenses(_DriverID);

            dgvLocalLicenses.DataSource = _dtDriverLocalLicenses;

            if (dgvLocalLicenses.RowCount > 0)
            {
                dgvLocalLicenses.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicenses.Columns[0].Width = 100;

                dgvLocalLicenses.Columns[1].HeaderText = "App.ID";
                dgvLocalLicenses.Columns[1].Width = 100;

                dgvLocalLicenses.Columns[2].HeaderText = "Class Name";
                dgvLocalLicenses.Columns[2].Width = 250;

                dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicenses.Columns[3].Width = 150;

                dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicenses.Columns[4].Width = 150;

                dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
                dgvLocalLicenses.Columns[5].Width = 60;

            }

            lblLocalLicenseRecordsNumber.Text = dgvLocalLicenses.RowCount.ToString();

        }

        private void _LoadInternationalDrivingLicenses()
        {
            
            _dtDriverInternationalLicenses = clsInternationalLicense.GetSpesificInternationalLicenses(_DriverID);

            dgvInternationalLicenses.DataSource = _dtDriverInternationalLicenses;

            if (dgvInternationalLicenses.RowCount > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.license ID";
                dgvInternationalLicenses.Columns[0].Width = 120;

                dgvInternationalLicenses.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenses.Columns[1].Width = 120;

                dgvInternationalLicenses.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 110;

                dgvInternationalLicenses.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenses.Columns[3].Width = 110;

                dgvInternationalLicenses.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 150;

                dgvInternationalLicenses.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 150;

                dgvInternationalLicenses.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenses.Columns[6].Width = 60;

            }

            lblInternationalRecordsNumber.Text = dgvInternationalLicenses.RowCount.ToString();

        }

        public void LoadDriverLicenses(int PersonID)
        {
            clsDriver Driver = clsDriver.FindByPersonID(PersonID);

            if (Driver == null)
            {
                MessageBox.Show("There no driver with person ID =" + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _DriverID = Driver.DriverID;

            _LoadLocalDrivingLicenses();

            _LoadInternationalDrivingLicenses();
        }

       

        private void showLocalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicenses.SelectedCells[0].Value;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);

            frm.ShowDialog();
        }

        private void showInterLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicenses.SelectedCells[0].Value;

            frmShowInterLicenseInfo frm = new frmShowInterLicenseInfo(InternationalLicenseID);

            frm.ShowDialog();
        }

    }

}
