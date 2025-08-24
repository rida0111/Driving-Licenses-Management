using Businesses_Access_Layer;
using DVLD2.Licenses.International_Licenses;
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

namespace DVLD2.Licenses.Control
{
    public partial class ctrlDriverLicenses : UserControl
    {

        private static DataTable _dtLocalLicenses = new DataTable();

        private static DataTable _dtInternationalLicenses = new DataTable();

        private void LoadLocalDrivingLicenses(int DriverID)
        {

            _dtLocalLicenses = clsLocalLicenses.GetSpesificLocalLicenses(DriverID);

            dgvLocalLicenses.DataSource = _dtLocalLicenses;

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

            lbLocalLicenseRecordsNumber.Text = dgvLocalLicenses.RowCount.ToString();

        }

        private void LoadInternationalDrivingLicenses(int DriverID)
        {

            _dtInternationalLicenses = clsInternationalLicense.GetSpesificInternationalLicenses(DriverID);

            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;

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

            lbInternationalRecordsNumber.Text = dgvInternationalLicenses.RowCount.ToString();

        }

        public void LoadDriverLicenses(int DriverID)
        {
            LoadLocalDrivingLicenses(DriverID);

            LoadInternationalDrivingLicenses(DriverID);
        }

        public ctrlDriverLicenses()
        {
            InitializeComponent();
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
