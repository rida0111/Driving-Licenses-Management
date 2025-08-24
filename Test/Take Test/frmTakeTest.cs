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

namespace DVLD2.Test.Take_Test
{
    public partial class frmTakeTest : Form
    {

        private static int _TestAppointmentID;

        private static byte _TestTypeID;

        public frmTakeTest(int TestAppointmentID, byte TestTypeID)
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;

            _TestTypeID = TestTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private clsTest AddNewTest()
        {
            clsTest Test = new clsTest();

            Test.Note = tbNotes.Text;

            Test.CreatedByUserID = clsUserInfo.CurrentUser.UserId;

            Test.TestAppointmentID = _TestAppointmentID;

            if (rbPass.Checked)
                Test.TestResult = 1;
            else
                Test.TestResult = 0;

            return Test.Save() ? Test : null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to save?After that you cannot change the Pass/Fail results After Save."
                 , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            clsTest Test = AddNewTest();

            clsTestAppointment TestAppointment = clsTestAppointment.FindById(_TestAppointmentID);

            if (Test != null && TestAppointment != null)
            {
                TestAppointment.IsLocked = true;


                if (TestAppointment.Save())
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error to save data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("Error to save data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.LoadTakeTestInfo(_TestAppointmentID, _TestTypeID);
        }

    }
}
