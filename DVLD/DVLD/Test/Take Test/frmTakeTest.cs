using Businesses_Access_Layer;
using DVLD.Global;
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
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID;

        private  clsTestType.enTestType _TestTypeID;

        public frmTakeTest(int TestAppointmentID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();

            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you Sure you want to save? After that you cannot change the results After Save."
                 , "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                return;

            clsTest Test = new clsTest();

            Test.Note = txtNotes.Text;
            Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            Test.TestAppointmentID = _TestAppointmentID;

            if (rbPass.Checked)
                Test.TestResult = 1;
            else
                Test.TestResult = 0;

            if (Test.Save())
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);           
            else
                MessageBox.Show("Error to save data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            ctrlScheduledTest1.TestType = _TestTypeID;
            ctrlScheduledTest1.LoadTakeTestInfo(_TestAppointmentID);          
        }


    }

}
