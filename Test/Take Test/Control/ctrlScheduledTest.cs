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

namespace DVLD2.Test.Take_Test.Control
{
    public partial class ctrlScheduledTest : UserControl
    {

        private static byte _TestTypeID;

        private static clsTestAppointment _TestAppointment;

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }
        private void HandleImageandTestName()
        {
         
            if (_TestTypeID == (byte)clsTestAppointment.enTestType.VisionTest)
            {
                 pbTest.Image = Resources.Vision_512;
                gbTestType.Text = "Vision Test";
                return;
            }

            if (_TestTypeID == (byte)clsTestAppointment.enTestType.WrittenTest)
            {
                pbTest.Image = Resources.Written_Test_512;
                gbTestType.Text = "Written Test";
                return;
            }

            if (_TestTypeID == (byte)clsTestAppointment.enTestType.StreetTest)
            {
                 pbTest.Image = Resources.Cars_48;
                gbTestType.Text = "Street Test";
                return;
            }
        }

        private void _FillScheduledTest()
        {
            lbDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();

            lblDlApplicationID.Text = _TestAppointment.LDLApplicationID.ToString();

            lbDrivingClassName.Text = _TestAppointment.LDLApplicationInfo.LicenseClassInfo.ClassName;

            lbName.Text = _TestAppointment.LDLApplicationInfo.ApplicationInfo.PersonInfo.FullName;

            lbTrail.Text = clsTest.CountFailTest(_TestAppointment.LDLApplicationInfo.LdlApplicationID, _TestAppointment.TestTypeID).ToString();

            lbFees.Text = _TestAppointment.TestTypeInfo.TestFees.ToString();

            HandleImageandTestName();

        }

        private void _ResetScheduledTest()
        {
            lbDate.Text = "[???]";

            lblDlApplicationID.Text = "[???]";

            lbDrivingClassName.Text = "[???]";

            lbName.Text = "[???]";

            lbTrail.Text = "[???]";

            lbFees.Text = "[$$$$]";

            lbTestID.Text = "No Taken Yet";

            gbTestType.Text = "Test Type";

             pbTest.Image = null;
        }

        public void LoadTakeTestInfo(int TestAppointmentID, byte TestTypeID)
        {
            _TestTypeID = TestTypeID;

            _TestAppointment = clsTestAppointment.FindById(TestAppointmentID);

            if (_TestAppointment != null)
                _FillScheduledTest();
            else
                _ResetScheduledTest();

        }


    }
}
