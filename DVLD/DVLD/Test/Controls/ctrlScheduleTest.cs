using Businesses_Access_Layer;
using DVLD.Global;
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
using System.Xml.Linq;
using static Businesses_Access_Layer.clsTestAppointment;
using static Businesses_Access_Layer.clsTestType;

namespace DVLD
{
    public partial class ctrlScheduleTest : UserControl
    {

        private enum enMode { AddNew = 1, Update = 2 }

        private enMode _Mode;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;

        public clsTestType.enTestType TestType
        {
            get {  return _TestTypeID; }
            set 
            { 
                _TestTypeID = value;

                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                    {
                        pbTestType.Image = Resources.Vision_512;
                        gbTestType.Text = "Vision Test";
                        break;
                    }
                    case clsTestType.enTestType.WrittenTest:
                    {
                        pbTestType.Image = Resources.Written_Test_512;
                        gbTestType.Text = "Written Test";
                        break;
                    }
                    case clsTestType.enTestType.StreetTest:
                    {
                        pbTestType.Image = Resources.driving_test_512;
                        gbTestType.Text = "Street Test";
                        break;
                    }
                }

            }
        }

        private int _LDLApplicationID = -1;

        private int _TestAppointmentID = -1;

        private clsLocalDrivingLicenseApplication _LDLApplication;


        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void _LoadData()
        {

            clsTestAppointment TestAppointment = clsTestAppointment.FindById(_TestAppointmentID);

            if (TestAppointment == null)
                return;

            dtpDateTest.Value = TestAppointment.AppointmentDate;
            dtpDateTest.MinDate = (dtpDateTest.Value < DateTime.Now.Date) ? dtpDateTest.Value : DateTime.Now.Date;

            if (TestAppointment.RetakeTestApplicationID != 0)
            {
                float RetakeTestFees = TestAppointment.ApplicationInfo.PaidFess;

                lblRtakeTestApplicationID.Text = TestAppointment.RetakeTestApplicationID.ToString();
                lblRetakeApplicationFees.Text = RetakeTestFees.ToString();
                lblTotalFees.Text = (TestAppointment.TestTypeInfo.TestFees + RetakeTestFees).ToString();
            }

            if (TestAppointment.IsLocked)
            {
                gbRetakeTest.Enabled = true;
                dtpDateTest.Enabled = false;
                btnSave.Enabled = false;
                lblUserMessage.Visible = true;
            }
            else
                lblUserMessage.Visible = false;
        }

        private void _FillScheduleTestInfo()
        {

            lblDrivingLicenseApplicationID.Text = _LDLApplication.ApplicationInfo.ApplicationID.ToString();
            lblLicenseclassName.Text = _LDLApplication.LicenseClassInfo.ClassName;
            lblName.Text = _LDLApplication.ApplicationInfo.PersonInfo.FullName;

            lblTrail.Text = _LDLApplication.CountTrialTest(TestType).ToString();
            gbRetakeTest.Enabled = clsTestAppointment.IsExistAndLocked(_LDLApplicationID, (byte)_TestTypeID);
            float Fees = clsTestType.FindByTestID(TestType).TestFees;

            lblFees.Text = Fees.ToString();
            lblTotalFees.Text = Fees.ToString();
            lblRetakeApplicationFees.Text = "0";

            if (_TestAppointmentID != -1)
            {
                _Mode = enMode.Update;
                _LoadData();
            }
            else
            {
                dtpDateTest.Value = DateTime.Now;
                dtpDateTest.MinDate = DateTime.Now.Date;
                lblUserMessage.Visible = false;
                _Mode = enMode.AddNew;
            }


        }

        private void _ResetScheduleTestInfo()
        {
            lblDrivingLicenseApplicationID.Text = "[???]";
            lblLicenseclassName.Text = "[???]";
            lblName.Text = "[???]";

            lblTrail.Text = "[???]";
            lblFees.Text = "[$$$$]";
            lblTotalFees.Text = "[$$$$]";

            lblRetakeApplicationFees.Text = "[$$$$]";
            lblRtakeTestApplicationID.Text = "[N/A]";

            gbTestType.Text = "Test Type";
            dtpDateTest.Value = DateTime.Now;

            gbRetakeTest.Enabled = false;
            lblUserMessage.Enabled = false;
            pbTestType.Image = null;
        }

        public void LoadScheduleTestInfo(int LDLApplicationID,int TestAppointmentID = -1)
        {

            _LDLApplicationID = LDLApplicationID;
            _TestAppointmentID = TestAppointmentID;
          
            _LDLApplication = clsLocalDrivingLicenseApplication.FindByID(_LDLApplicationID);

            if (_LDLApplication == null)
            {
                _ResetScheduleTestInfo();
                return;
            }

            _FillScheduleTestInfo();      
        }

        private clsApplication AddNewApplication()
        {

            clsApplication clsApplication = new clsApplication();

            clsApplication.PersonID = _LDLApplication.ApplicationInfo.PersonID;
            clsApplication.ApplicationDate = DateTime.Now;
            clsApplication.ApplicationTypeID = (byte)clsApplication.enApplicationType.RetakeTest;

            clsApplication.ApplicationStatus = _LDLApplication.ApplicationInfo.ApplicationStatus;
            clsApplication.LastStatusDate = DateTime.Now;
            clsApplication.PaidFess = clsApplicationType.Find(clsApplication.enApplicationType.RetakeTest).Fees;
            clsApplication.UserID = clsGlobal.CurrentUser.UserID;

            return clsApplication.Save() ? clsApplication : null;

        }
        private clsTestAppointment AddNewTestAppointment()
        {
            clsTestAppointment clsTestAppointment = new clsTestAppointment();

            clsTestAppointment.TestTypeID = (byte)TestType;
            clsTestAppointment.LDLApplicationID = _LDLApplicationID;
            clsTestAppointment.AppointmentDate = dtpDateTest.Value;

            clsTestAppointment.PaidFees = clsTestType.FindByTestID(TestType).TestFees;
            clsTestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
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

                if (clsTestAppointment.IsExistAndLocked(_LDLApplicationID,(byte) _TestTypeID))
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

                clsTestAppointment.AppointmentDate = dtpDateTest.Value;

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
