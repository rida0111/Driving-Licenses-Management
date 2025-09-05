
using Data_Base_of_DVLM;
using System;
using System.Runtime.CompilerServices;


namespace Businesses_Access_Layer
{

    public class clsApplication 
    {

        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
       
        public byte ApplicationTypeID { get; set; }
        public byte ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public Single PaidFess { get; set; }
        public int UserID { get; set; }

        public clsPerson PersonInfo { get; set; }

        public clsUser UserInfo { get; set; }

        public clsApplicationType ApplicationsTypeInfo { get; set; }

        public enum enMode { AddNew = 0, Update = 1 }

        public  enMode Mode;

        public enum enApplicationType{NewLocalDrivingLicense=1,RenewLicense=2,ReplacementforLost=3
                , ReplacementforDamaged=4,ReleaseDetainLicense=5,NewInternationalLicense=6,RetakeTest=7}

        public enum enStatus { New = 1, Cancelled = 2, Completed = 3 }

        public string StatusName { get { return _StatusName(); } }

        public clsApplication()
        {
           
            ApplicationID = -1;
            PersonID = -1;
            ApplicationDate = new DateTime();
            ApplicationStatus = 0;
            LastStatusDate = new DateTime();
            PaidFess = 0;
            UserID = 0;   
            Mode = enMode.AddNew;
        }

        private clsApplication(int appID, int personId, DateTime appDate, byte appType, byte appStatus,
              DateTime lastStatusDate, Single paidFess, int userID)
        {
            ApplicationID = appID;
            PersonID = personId;
            ApplicationDate = appDate;

            ApplicationTypeID = appType;
            ApplicationStatus = appStatus;
            LastStatusDate = lastStatusDate;

            PaidFess = paidFess;
            UserID = userID;

            PersonInfo = clsPerson.FindById(this.PersonID);

            ApplicationsTypeInfo = clsApplicationType.Find((clsApplication.enApplicationType)this.ApplicationTypeID);

            UserInfo = clsUser.FindByID(this.UserID);

            Mode = enMode.Update;
        }

        public static clsApplication FindByID(int AppID)
        {

            DateTime AppDate = new DateTime(), LastStatusDate = new DateTime();

            int PersonId = -1, UserID = -1;
            Single PaidFess = 0;
            byte AppStatus = 0, AppType = 0;


            if (clsApplicationData.FindbyID(AppID, ref PersonId, ref AppDate, ref AppType
                , ref AppStatus, ref LastStatusDate, ref PaidFess, ref UserID))

            {
                return new clsApplication(AppID, PersonId, AppDate, AppType
                    , AppStatus, LastStatusDate, PaidFess, UserID);
            }

            return null;
        }

        private bool AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.PersonID, this.ApplicationTypeID, this.ApplicationStatus, this.PaidFess, this.UserID);

            return (this.ApplicationID != -1);
        }

        private bool UpdateApplication()
        {
            return clsApplicationData.UpdateNew(this.ApplicationID, this.ApplicationStatus);
        }

        public bool Save()
        {
            switch (Mode)
            {

                case enMode.AddNew:
                {
                    if(AddNewApplication())
                    {
                       Mode = enMode.Update;
                        return true;
                    }
                    return false;
                }

                case enMode.Update:
                {
                    return UpdateApplication();
                }

                default:
                    {
                        return false;
                    }
            }


        }

        public static bool SetComplete(int ldlAppID)
        {
            return clsApplicationData.UpdateStatus(ldlAppID);
        }

        public static bool DeleteApplication(int LDLAppID)
        {
            int ApplicationID = clsLocalDrivingLicenseApplication.FindByID(LDLAppID).ApplicationID;

           
            if (clsLocalDrivingLicenseApplicationData.DeleteLocalLicenseApplication(LDLAppID))
                return clsApplicationData.DeleteApplication(ApplicationID);
            else
                return false;
        }

        private string _StatusName()
        {
            string StatusName = "";

            switch (this.ApplicationStatus)
            {
                case 1:
                    StatusName = "New";
                    break;
                case 2:
                    StatusName = "Cancelled";
                    break;
                case 3:
                    StatusName = "Completed";
                    break;
                default:
                    StatusName = "";
                    break;
            }

            return StatusName;

        }



    }



}