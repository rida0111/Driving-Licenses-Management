using Data_Base_of_DVLM;
using System;
using System.Data;


namespace Businesses_Access_Layer
{



    public class clsLocalDrivingLicenseApplication
    {

        public int ApplicationID { get; set; }
        public byte LicenseClassID { get; set; }
        public int LdlApplicationID { get; set; }

        private enum enMode { AddNew = 0, Update = 1 }

        private enMode _Mode;

        public clsApplication ApplicationInfo { get; set; }

        public clsLicenseClass LicenseClassInfo { get; set; }

        public clsLocalLicenses LocalLicenseInfo { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            ApplicationID = -1;
            LdlApplicationID = 0;
            LicenseClassID = 0;
       
            _Mode = enMode.AddNew;
          
        }


        private clsLocalDrivingLicenseApplication(int ldlApplicationID, int applicationID, byte licenceClassID)
        {
            ApplicationID = applicationID;
            LicenseClassID = licenceClassID;
            LdlApplicationID = ldlApplicationID;           
            ApplicationInfo = clsApplication.FindByID(this.ApplicationID);

            LicenseClassInfo = clsLicenseClass.FindByID(this.LicenseClassID);

            LocalLicenseInfo = clsLocalLicenses.FindByApplicationID(this.ApplicationID);

            _Mode = enMode.Update;
        }


        public static clsLocalDrivingLicenseApplication FindByID(int LdlApplicationID)
        {
            byte LicenceClassID = 0;
            int ApplicationID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.FindById(LdlApplicationID, ref ApplicationID, ref LicenceClassID);

            if (IsFound)
                return new clsLocalDrivingLicenseApplication(LdlApplicationID, ApplicationID, LicenceClassID);
            else
                return null;
        }


        public static bool IsHadTestAppointment(int LDLApplicationID, byte TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.IsHasTestAppointment(LDLApplicationID, TestTypeID);
        }

        public static bool IsHasTestAppointment(int LDLApplicationID)
        {
            return clsLocalDrivingLicenseApplicationData.IsHasTestAppointment(LDLApplicationID);
        }
        private bool _AddLocalDrivingLicenseApp()
        {
            this.LdlApplicationID = clsLocalDrivingLicenseApplicationData.AddNew(this.ApplicationID, this.LicenseClassID);

            return (this.LdlApplicationID != -1);
        }


        public static bool DoesPassTestType(int ldlApplicationID, byte TestTpeID)
        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(ldlApplicationID, TestTpeID);
        }

        public int CountTrialTest(clsTestType.enTestType TestTpeID)
        {
            return clsLocalDrivingLicenseApplicationData.CountTrialTest(this.LdlApplicationID,(byte) TestTpeID);
        }
        private bool _Update()
        {

            return clsLocalDrivingLicenseApplicationData.UpdateInfo(this.LdlApplicationID, this.ApplicationID, this.LicenseClassID);

        }

        public bool Save()
        {

            switch (_Mode)
            {

                case enMode.AddNew:
                {
                    if(_AddLocalDrivingLicenseApp())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                }

                case enMode.Update:
                {
                    return _Update();
                }

                default:
                {
                    return false;
                }

            }

        }

        public static DataTable GetAllLocalDrivingApplication()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingApplication();
        }

        public bool IsApplicationExist(int PersonID, byte AppStatuts, string ClassName)
        {
            this.ApplicationID = clsLocalDrivingLicenseApplicationData.IsApplicationExist(PersonID, AppStatuts, ClassName);

            return (this.ApplicationID != -1);
        }

        public  byte PassedTest()
        {
            return clsTest.NumberPassedTests(this.LdlApplicationID);
        }

        public bool IsPassedAllTests()
        {
            return (PassedTest() == 3);
        }

        public bool IsHasAnActiveLicense()
        {
            return clsLocalLicenses.IsHasAnActiveLicense(this.ApplicationID, this.LicenseClassID);         
        }

        public  int IssueLicenseForTheFirstTime(string Note, int UserID)
        {

          
            clsDriver clsDriver = clsDriver.FindByPersonID(this.ApplicationInfo.PersonID);

            if (clsDriver == null)
            {
                clsDriver = new clsDriver();

                clsDriver.PersonID = this.ApplicationInfo.PersonID;

                clsDriver.CreatedUserID = UserID;

                if (!clsDriver.Save())
                    return -1;
            }

            clsLocalLicenses clsLicense = new clsLocalLicenses();

            clsLicense.IsActive = true;

            clsLicense.PaidFees = this.LicenseClassInfo.ClassFess;

            clsLicense.UserID = UserID;

            clsLicense.LicenseClass = this.LicenseClassID;

            clsLicense.ApplicationId = this.ApplicationID;

            clsLicense.DriverId = clsDriver.DriverID;

            clsLicense.IssueDate = DateTime.Now;

            clsLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);

            clsLicense.Notes = Note;

            clsLicense.IssueReason = clsLocalLicenses.enIssueReason.FirstTime;

            if (clsLicense.Save())
            {
                if (clsApplication.SetComplete(this.LdlApplicationID))
                    return clsLicense.LicenseId;
            }
            return -1;

        }


        public bool IsHasLicense()
        {
            return clsLocalLicenses.IsHasLicense(this.ApplicationID,this.LicenseClassID);
        }
    }



}