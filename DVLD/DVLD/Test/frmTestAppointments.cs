using Businesses_Access_Layer;
using DVLD.Applications.Local_License.Control;
using DVLD.Properties;
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
    public partial class frmTestAppointments : Form
    {
        private int _LDLApplicationID;

        private clsTestType.enTestType _TestTypeID;

        private DataTable _dtTestAppointment;

        public frmTestAppointments(int LDLApplicationID, clsTestType.enTestType enTestType)
        {
            InitializeComponent();
            _LDLApplicationID = LDLApplicationID;

            _TestTypeID = enTestType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {

            if (clsLocalDrivingLicenseApplication.DoesPassTestType(_LDLApplicationID,(byte) _TestTypeID))
            {

                MessageBox.Show("Person Already Passed this test before, You can only retake  failed Test"
                  , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsLocalDrivingLicenseApplication.IsHadTestAppointment(_LDLApplicationID,(byte) _TestTypeID))
            {

                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new Appointment"
                    , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frmSchedule = new frmScheduleTest(_LDLApplicationID, _TestTypeID);

           frmSchedule.ShowDialog();

            LoadDataGridView();
        }

        private void editTestToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int TestAppointmentID = (int)dgvTestAppointments.SelectedCells[0].Value;

            frmScheduleTest frmSchedule = new frmScheduleTest(_LDLApplicationID, _TestTypeID, TestAppointmentID);

            frmSchedule.ShowDialog();

            LoadDataGridView();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestAppointmentID = (int)dgvTestAppointments.SelectedCells[0].Value;

            clsTestAppointment clsTest = clsTestAppointment.FindById(TestAppointmentID);

            if (clsTest != null && clsTest.IsLocked)
            {
                MessageBox.Show("Person Already Take this test before,you can not Take it"
                 , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTakeTest frm = new frmTakeTest(TestAppointmentID, _TestTypeID);

            frm.ShowDialog();

            frmTestAppointments_Load(null, null);
        }

        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            LoadDataGridView();

            ctrlDrivingLicenseApplicationInfo1.LoadLicenseInfo(_LDLApplicationID);

            if (_TestTypeID == clsTestType.enTestType.VisionTest)
            {
                pbTest.Image = Resources.Vision_512;
                lbText.Text = "Vision Test Appointments";
                this.Text = "Vision Test Appointments";
                return;
            }

            if (_TestTypeID == clsTestType.enTestType.WrittenTest)
            {
                pbTest.Image = Resources.Written_Test_512;
                lbText.Text = "Written Test Appointments";
                this.Text = "Written Test Appointments";
                return;
            }

            if (_TestTypeID == clsTestType.enTestType.StreetTest)
            {
                pbTest.Image = Resources.driving_test_512;
                lbText.Text = "Street Test Appointments";
                this.Text = "Street Test Appointments";
                return;
            }

        }

        private void LoadDataGridView()
        {
            _dtTestAppointment = clsTestAppointment.GetSpecificTestAppointment(_LDLApplicationID, (byte)_TestTypeID);

            dgvTestAppointments.DataSource = _dtTestAppointment;

            if (dgvTestAppointments.RowCount > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvTestAppointments.Columns[0].Width = 200;

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvTestAppointments.Columns[1].Width = 150;

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvTestAppointments.Columns[2].Width = 200;

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvTestAppointments.Columns[3].Width = 150;
            }

            lblRecordsNumber.Text = dgvTestAppointments.RowCount.ToString();

        }

 
    }
}
