using Businesses_Access_Layer;
using DVLD2.Licenses;
using DVLD2.Licenses.Local_License;
using DVLD2.Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Applications.Local_License
{
    public partial class frmListLocalDrivingLicenseApp : Form
    {

        private static DataTable _dtLDLApplication = new DataTable();

        public frmListLocalDrivingLicenseApp()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicenseApp_Load(object sender, EventArgs e)
        {
            _dtLDLApplication = clsLDLApplication.GetAllLocalDrivingApplication();

            dgvLocalLicenseApplication.DataSource = _dtLDLApplication;

            lbRecordsNumber.Text = dgvLocalLicenseApplication.RowCount.ToString();

            if (dgvLocalLicenseApplication.RowCount > 0)
            {
                dgvLocalLicenseApplication.Columns[0].HeaderText = "LDLApplicationID";
                dgvLocalLicenseApplication.Columns[0].Width = 160;

                dgvLocalLicenseApplication.Columns[1].HeaderText = "ClassName";
                dgvLocalLicenseApplication.Columns[1].Width = 250;

                dgvLocalLicenseApplication.Columns[2].HeaderText = "NationalNo";
                dgvLocalLicenseApplication.Columns[2].Width = 90;

                dgvLocalLicenseApplication.Columns[3].HeaderText = "FullName";
                dgvLocalLicenseApplication.Columns[3].Width = 300;

                dgvLocalLicenseApplication.Columns[4].HeaderText = "ApplicationDate";
                dgvLocalLicenseApplication.Columns[4].Width = 150;

                dgvLocalLicenseApplication.Columns[5].HeaderText = "PassedTestCount";
                dgvLocalLicenseApplication.Columns[5].Width = 130;

                dgvLocalLicenseApplication.Columns[6].HeaderText = "Status";
                dgvLocalLicenseApplication.Columns[6].Width = 90;
            }

            cbFilter.SelectedIndex = 0;
        }

        private void tbFilter_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (cbFilter.Text == "L.D.L.AppID")
                e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar));
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            string ColumnName = "";

            switch (cbFilter.Text)
            {
                case "L.D.L.AppID":
                    ColumnName = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No.":
                    ColumnName = "NationalNo";
                    break;
                case "FullName":
                    ColumnName = "FullName";
                    break;
                case "Status":
                    ColumnName = "Status";
                    break;
                default:
                    ColumnName = "None";
                    break;
            }

            if (tbFilter.Text == "" || ColumnName == "None")
                _dtLDLApplication.DefaultView.RowFilter = "";
            else if (ColumnName == "LocalDrivingLicenseApplicationID")
                _dtLDLApplication.DefaultView.RowFilter = string.Format("[{0}]={1}", ColumnName, tbFilter.Text);
            else
                _dtLDLApplication.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", ColumnName, tbFilter.Text);

            lbRecordsNumber.Text = dgvLocalLicenseApplication.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFilter.Visible = (cbFilter.Text != "None");

            tbFilter.Text = "";
            tbFilter.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            frmAddUpdateLocalDrivingLicenseApp frm = new frmAddUpdateLocalDrivingLicenseApp();

            frm.ShowDialog();

            frmListLocalDrivingLicenseApp_Load(null, null);
        }

        private void _TestAppointment(clsTestAppointment.enTestType enTestType)
        {
            int LDLApplicationID = (int)dgvLocalLicenseApplication.SelectedCells[0].Value;

            frmTestAppointments frm = new frmTestAppointments(LDLApplicationID, enTestType);

            frm.ShowDialog();

            frmListLocalDrivingLicenseApp_Load(null, null);
        }

        private void cmsLDLApplication_Opening(object sender, CancelEventArgs e)
        {
            string Status = (string)dgvLocalLicenseApplication.SelectedCells[6].Value;

            int PassedTestCount = (int)dgvLocalLicenseApplication.SelectedCells[5].Value;

            int LDLApplicationID = (int)dgvLocalLicenseApplication.SelectedCells[0].Value;

            clsLDLApplication lDLApplication = clsLDLApplication.FindByID(LDLApplicationID);

            bool IsHasTestAppointment = clsTestAppointment.IsHasTestAppointment(lDLApplication.LdlApplicationID);
           
            ShowLicense.Enabled = (Status == "Completed");

            cancelApplication.Enabled = (Status == "New");

            IssueDriverLicense.Enabled = (Status == "New" && PassedTestCount == 3);

            DeleteApplication.Enabled = (Status == "New");

            EditApplication.Enabled = (Status == "New" && !IsHasTestAppointment);

            SechduleTest.Enabled = (Status == "New" && PassedTestCount != 3);
    
            if(SechduleTest.Enabled)
            {
                VisionTest.Enabled = (PassedTestCount == 0);
                WrittenTest.Enabled = (PassedTestCount == 1);
                StreetTest.Enabled = (PassedTestCount == 2);
            }
            
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalLicenseApplication.SelectedCells[0].Value;

            frmAddUpdateLocalDrivingLicenseApp frm = new frmAddUpdateLocalDrivingLicenseApp(LDLApplicationID);

            frm.ShowDialog();

            frmListLocalDrivingLicenseApp_Load(null, null);
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do want to delete this Application?", "Confirm"
           , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (clsApplication.DeleteApplication((int)dgvLocalLicenseApplication.SelectedCells[0].Value))
            {
                MessageBox.Show("Application Deleted Successfully", "Deleted"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmListLocalDrivingLicenseApp_Load(null, null);
            }
            else
                MessageBox.Show("Application was Not Deleted Beacause it has Data Linked to it !", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this Application ?", "Confirm", MessageBoxButtons.YesNo
        , MessageBoxIcon.Question) == DialogResult.No)
                return;

            int LDLApplicationID = (int)dgvLocalLicenseApplication.SelectedCells[0].Value;

            clsApplication clsApplication = clsLDLApplication.FindByID(LDLApplicationID).ApplicationInfo;

            clsApplication.LastStatusDate = DateTime.Now;

            clsApplication.ApplicationStatus = (byte)clsApplication.enStatus.Cancelled;

            if (clsApplication.Save())
            {
                MessageBox.Show("data Saved Successfully", "Saved", MessageBoxButtons.OK
                  , MessageBoxIcon.Information);

                frmListLocalDrivingLicenseApp_Load(null, null);
            }
            else
                MessageBox.Show("Fail to save data", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalLicenseApplication.SelectedCells[0].Value;

            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LDLApplicationID);

            frm.ShowDialog();

            frmListLocalDrivingLicenseApp_Load(null, null);
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvLocalLicenseApplication.SelectedCells[0].Value;

            int LicenseID = clsLDLApplication.FindByID(ApplicationID).LocalLicenseInfo.LicenseId;

            frmShowLicenseInfo frm = new frmShowLicenseInfo(LicenseID);

            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsLDLApplication.FindByID((int)dgvLocalLicenseApplication.SelectedCells[0].Value).ApplicationInfo.PersonId;

            int DriverID = clsDriver.FindByPersonID(PersonID).DriverID;

            frmShowLicenseHistory frm = new frmShowLicenseHistory(DriverID);

            frm.ShowDialog();
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LDLApplicationID = (int)dgvLocalLicenseApplication.SelectedCells[0].Value;

            frmLocalDrivingLicenseDetails frm = new frmLocalDrivingLicenseDetails(LDLApplicationID);

            frm.ShowDialog();
        }

        private void VisionTest_Click(object sender, EventArgs e)
        {
            _TestAppointment(clsTestAppointment.enTestType.VisionTest);
        }

        private void writtenTest_Click(object sender, EventArgs e)
        {
            _TestAppointment(clsTestAppointment.enTestType.WrittenTest);
        }

        private void StreetTest_Click(object sender, EventArgs e)
        {
            _TestAppointment(clsTestAppointment.enTestType.StreetTest);
        }
    }
}
