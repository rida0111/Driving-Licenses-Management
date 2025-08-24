using Businesses_Access_Layer;
using DVLD2.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.Test.Schedule_Test.Control
{
    public partial class ctrlScheduleTest : UserControl
    {

        private static int _LDLApplicationID;

        private static int _TestAppointmentID;

        private static byte _TestTypeID;

        private static clsLDLApplication _LDLApplication;

        private enum enMode { AddNew = 1, Update = 2 }

        private enMode _Mode;

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void _UpdateScheduleTestInfo()
        {

            clsTestAppointment TestAppointment = clsTestAppointment.FindById(_TestAppointmentID);

            if (TestAppointment == null)
                return;

            dateTimePicker1.Value = TestAppointment.AppointmentDate;

            dateTimePicker1.MinDate = (dateTimePicker1.Value < DateTime.Now.Date) ? dateTimePicker1.Value : DateTime.Now.Date;

            if (TestAppointment.RetakeTestApplicationID != 0)
            {
                int RetakeTestFees = TestAppointment.ApplicationInfo.PaidFess;

                lbRtakeTestApplicationID.Text = TestAppointment.RetakeTestApplicationID.ToString();

                lbRetakeApplicationFees.Text = RetakeTestFees.ToString();

                lbTotalFees.Text = (TestAppointment.TestTypeInfo.TestFees + RetakeTestFees).ToString();
            }

            if (TestAppointment.IsLocked)
            {
                gbRetakeTest.Enabled = true;
                dateTimePicker1.Enabled = false;

                btnSave.Enabled = false;
                lbtext.Visible = true;
            }

        }

        private void _FillScheduleTestInfo()
        {

            lbDrivingLicenseApplicationID.Text = _LDLApplication.ApplicationInfo.ApplicationID.ToString();

            lbLicenseclassName.Text = _LDLApplication.LicenseClassInfo.ClassName;

            lbName.Text = _LDLApplication.ApplicationInfo.PersonInfo.FullName;

            lbTrail.Text = clsTest.CountFailTest(_LDLApplicationID, _TestTypeID).ToString();

            gbRetakeTest.Enabled = clsTestAppointment.IsExistAndLocked(_LDLApplicationID, _TestTypeID);

            int Fees = clsTestType.FindByTestID(_TestTypeID).TestFees;

            lbFees.Text = Fees.ToString();

            lbTotalFees.Text = Fees.ToString();

            lbRetakeApplicationFees.Text = "0";

            if (_TestAppointmentID != -1)
            {
                _Mode = enMode.Update;
                _UpdateScheduleTestInfo();
            }
            else
            {
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker1.MinDate = DateTime.Now.Date;

                _Mode = enMode.AddNew;
            }

            HandleImageandTestName();

        }

        private void HandleImageandTestName()
        {
            if (_TestTypeID == (byte)clsTestAppointment.enTestType.VisionTest)
            {
                pbTestType.Image = Resources.Vision_512;

                gbTestType.Text = "Vision Test";
                return;
            }

            if (_TestTypeID == (byte)clsTestAppointment.enTestType.WrittenTest)
            {
            
                pbTestType.Image = Resources.Written_Test_512;

                gbTestType.Text = "Written Test";
                return;
            }

            if (_TestTypeID == (byte)clsTestAppointment.enTestType.StreetTest)
            {
                 pbTestType.Image = Resources.driving_test_512;

                gbTestType.Text = "Street Test";
                return;
            }
        }

        private void _ResetScheduleTestInfo()
        {
            lbDrivingLicenseApplicationID.Text = "[???]";
            lbLicenseclassName.Text = "[???]";
            lbName.Text = "[???]";

            lbTrail.Text = "[???]";
            lbFees.Text = "[$$$$]";
            lbTotalFees.Text = "[$$$$]";

            lbRetakeApplicationFees.Text = "[$$$$]";
            lbRtakeTestApplicationID.Text = "[N/A]";

            dateTimePicker1.Value = DateTime.Now;

            gbRetakeTest.Enabled = false;
            lbtext.Enabled = false;

            gbTestType.Text = "Test Type";
            //pbTestType.Image = null;
        }

        public void LoadScheduleTestInfo(int LDLApplicationID, int TestAppointmentID, byte TestTypeID)
        {

            _LDLApplicationID = LDLApplicationID;
            _TestAppointmentID = TestAppointmentID;
            _TestTypeID = TestTypeID;

            _LDLApplication = clsLDLApplication.FindByID(_LDLApplicationID);

            if (_LDLApplication != null)
                _FillScheduleTestInfo();
            else
                _ResetScheduleTestInfo();

        }

        private clsApplication AddNewApplication()
        {

            clsApplication clsApplication = new clsApplication();


            clsApplication.PersonId = _LDLApplication.ApplicationInfo.PersonId;

            clsApplication.ApplicationDate = DateTime.Now;

            clsApplication.ApplicationTypeID = (byte)clsApplication.enApplicationType.RetakeTest;

            clsApplication.ApplicationStatus = _LDLApplication.ApplicationInfo.ApplicationStatus;

            clsApplication.LastStatusDate = DateTime.Now;

            clsApplication.PaidFess = clsApplicationType.FindByID((byte)clsApplication.enApplicationType.RetakeTest).Fees;

            clsApplication.UserID = clsUserInfo.CurrentUser.UserId;

            return clsApplication.Save() ? clsApplication : null;

        }
        private clsTestAppointment AddNewTestAppointment()
        {
            clsTestAppointment clsTestAppointment = new clsTestAppointment();

            clsTestAppointment.TestTypeID = _TestTypeID;

            clsTestAppointment.LDLApplicationID = _LDLApplicationID;

            clsTestAppointment.AppointmentDate = dateTimePicker1.Value;

            clsTestAppointment.PaidFees = clsTestType.FindByTestID(_TestTypeID).TestFees;

            clsTestAppointment.CreatedByUserID = clsUserInfo.CurrentUser.UserId;

            clsTestAppointment.IsLocked = false;

            return clsTestAppointment.Save() ? clsTestAppointment : null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                clsTestAppointment clsTestAppointment = AddNewTestAppointment();

                if (clsTestAppointment == null)
                {
                    MessageBox.Show("Fail To Add TestAppointment", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close_Click();
                }

                if (clsTestAppointment.IsExistAndLocked(_LDLApplicationID, _TestTypeID))
                {
                    clsApplication clsApplication = AddNewApplication();

                    if (clsApplication == null)
                    {
                        MessageBox.Show("Fail To Add Application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close_Click();
                    }

                    clsTestAppointment = clsTestAppointment.FindById(clsTestAppointment.TestAppointmentID);

                    clsTestAppointment.RetakeTestApplicationID = clsApplication.ApplicationID;

                    if (clsTestAppointment.Save())
                        MessageBox.Show("Data saved successfully !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Error To save Data !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                else
                    MessageBox.Show("Data saved successfully !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (_Mode == enMode.Update)
            {
                clsTestAppointment clsTestAppointment = clsTestAppointment.FindById(_TestAppointmentID);

                if (clsTestAppointment == null)
                {
                    MessageBox.Show("Test Appointment is not Found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close_Click();
                }

                clsTestAppointment.AppointmentDate = dateTimePicker1.Value;

                if (clsTestAppointment.Save())
                    MessageBox.Show("Data Updated successfully !", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Error To  Update Data!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close_Click();
        }

        private void Close_Click()
        {
            Form parentForm = this.FindForm();

            if (parentForm != null)
                parentForm.Close();
        }


    }


}
