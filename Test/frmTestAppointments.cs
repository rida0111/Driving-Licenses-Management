using Businesses_Access_Layer;
using DVLD2.Properties;
using DVLD2.Test.Schedule_Test;
using DVLD2.Test.Take_Test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Test
{
    public partial class frmTestAppointments : Form
    {

        private static int _LDLApplicationID;

        private static byte _TestTypeID;

        private static DataTable _dtTestAppointment = new DataTable();


        public frmTestAppointments(int LDLApplicationID, clsTestAppointment.enTestType enTestType)
        {
            InitializeComponent();

            _LDLApplicationID = LDLApplicationID;

            _TestTypeID = (byte)enTestType;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {

            if (clsTest.IsTestPassed(_LDLApplicationID, _TestTypeID))
            {

                MessageBox.Show("Person Already Passed this test before, You can only retake  failed Test"
                  , "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsTestAppointment.IsHadTestAppointment(_LDLApplicationID, _TestTypeID))
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


            ctrlDrivingLicenseApplicationInfo1.LoadLicenseAppInfo(_LDLApplicationID);

            if (_TestTypeID == (byte)clsTestAppointment.enTestType.VisionTest)
            {
                 pbTest.Image = Resources.Vision_512;
                lbText.Text = "Vision Test Appointments";
                this.Text = "Vision Test Appointments";
                return;
            }

            if (_TestTypeID == (byte)clsTestAppointment.enTestType.WrittenTest)
            {
                pbTest.Image = Resources.Written_Test_512;
                lbText.Text = "Written Test Appointments";
                this.Text = "Written Test Appointments";
                return;
            }

            if (_TestTypeID == (byte)clsTestAppointment.enTestType.StreetTest)
            {
                 pbTest.Image = Resources.Cars_48;
                lbText.Text = "Street Test Appointments";
                this.Text = "Street Test Appointments";
                return;
            }

        }

        private void LoadDataGridView()
        {
            _dtTestAppointment = clsTestAppointment.CetSpesificTestAppointment(_LDLApplicationID, _TestTypeID);

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

            lbCount.Text = dgvTestAppointments.RowCount.ToString();

        }


    }
}
