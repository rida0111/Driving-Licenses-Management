using Data_Base_of_DVLM;
using System;
using System.Data;
using static Businesses_Access_Layer.clsTestAppointment;




namespace Businesses_Access_Layer
{



    public class clsTestAppointment
    {

        public int TestAppointmentID { get; set; }
        public byte TestTypeID { get; set; }
        public int LDLApplicationID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Single PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        public clsApplication ApplicationInfo { get; set; }

        public clsLocalDrivingLicenseApplication LDLApplicationInfo { get; set; }
        private enum enMode { AddNew = 0, Update }

        private enMode _Mode;

        
        public clsTestType TestTypeInfo { get; set; }

        private clsTestAppointment(int testAppointmentID, byte testTypeID, int lDLAppID, DateTime appointmentDate
       , Single paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;
            LDLApplicationID = lDLAppID;

            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;

            IsLocked = isLocked;
            RetakeTestApplicationID = retakeTestApplicationID;

            _Mode = enMode.Update;

            ApplicationInfo = clsApplication.FindByID(this.RetakeTestApplicationID);

            TestTypeInfo = clsTestType.FindByTestID((clsTestType.enTestType)this.TestTypeID);

            LDLApplicationInfo = clsLocalDrivingLicenseApplication.FindByID(this.LDLApplicationID);

        }

        public clsTestAppointment()
        {

            TestAppointmentID = -1;
            TestTypeID = (int)clsTestType.enTestType.VisionTest;
            LDLApplicationID = -1;

            AppointmentDate = new DateTime();

            PaidFees = 0;

            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;

            _Mode = enMode.AddNew;
        }

        public static DataTable GetSpecificTestAppointment(int LDLAppID, byte TestType)
        {
            return clsTestAppointmentData.GetSpesificTestAppointment(LDLAppID, TestType);
        }

        private bool AddTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentData.AddTestAppointment(this.TestTypeID, this.LDLApplicationID
                , this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, this.TestTypeID, this.LDLApplicationID
                , this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                {
                    if(AddTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                }

                case enMode.Update:
                return UpdateTestAppointment();
                default:
                return false;
            }


        }

        public static clsTestAppointment FindById(int testAppointmentID)
        {
            bool IsLocked = false; 
            byte TestTypeID = 0;
            DateTime AppointmentDate = new DateTime();

            int CreatedByUserID = 0, LDLAppID = 0, RetakeTestApplicationID = -1;
            Single PaidFees = 0;

            bool IsFound = clsTestAppointmentData.FindByid(testAppointmentID, ref TestTypeID, ref LDLAppID, ref AppointmentDate, ref PaidFees
                , ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID);

            if (IsFound)
            {
                return new clsTestAppointment(testAppointmentID, TestTypeID, LDLAppID, AppointmentDate, PaidFees
                , CreatedByUserID, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;
        }

        public static bool IsExistAndLocked(int LDLApplicationID, byte TestTypeID)
        {
            return clsTestAppointmentData.IsExistAndLocked(LDLApplicationID, TestTypeID);
        }



    }




}
