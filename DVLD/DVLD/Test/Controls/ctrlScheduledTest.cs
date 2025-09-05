using Businesses_Access_Layer;
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
    public partial class ctrlScheduledTest : UserControl
    {

        private  clsTestType.enTestType _TestTypeID = clsTestType.enTestType.WrittenTest;

        private  clsTestAppointment _TestAppointment;

        public clsTestType.enTestType TestType
        {
            get { return _TestTypeID; }
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

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }
     
        private void _FillScheduledTest()
        {
            lblDate.Text = _TestAppointment.AppointmentDate.ToShortDateString();
            lbllocalDrivinglicenseApplicationID.Text = _TestAppointment.LDLApplicationID.ToString();
            lblDrivingClassName.Text = _TestAppointment.LDLApplicationInfo.LicenseClassInfo.ClassName;

            lblName.Text = _TestAppointment.LDLApplicationInfo.ApplicationInfo.PersonInfo.FullName;
            lblTrail.Text = _TestAppointment.LDLApplicationInfo.CountTrialTest(TestType).ToString();
            lblFees.Text = _TestAppointment.TestTypeInfo.TestFees.ToString();         
        }
        private void _ResetScheduledTest()
        {
            lblDate.Text = "[???]";
            lbllocalDrivinglicenseApplicationID.Text = "[???]";
            lblDrivingClassName.Text = "[???]";

            lblName.Text = "[???]";
            lblTrail.Text = "[???]";
            lblFees.Text = "[$$$$]";

            lblTestID.Text = "No Taken Yet";
            gbTestType.Text = "Test Type";
            pbTestType.Image = null;
        }

        public void LoadTakeTestInfo(int TestAppointmentID)
        {

            _TestAppointment = clsTestAppointment.FindById(TestAppointmentID);

            if (_TestAppointment == null)
            {
                _ResetScheduledTest();
                return;
            }

            _FillScheduledTest();       
        }

    }
}
