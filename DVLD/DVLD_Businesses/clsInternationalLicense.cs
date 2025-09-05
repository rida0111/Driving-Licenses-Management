using Data_Base_of_DVLM;
using System;
using System.Data;
using System.Runtime.CompilerServices;


namespace Businesses_Access_Layer
{


    public class clsInternationalLicense:clsApplication
    {

        public int InterLicenseID { get; set; }
        public int ApplicationId { get; set; }
        public int DriverID { get; set; }     
        public int LocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedUserID { get; set; }
    
        public clsLocalLicenses LocalLicenseInfo { get; set; }
        public clsDriver DriverInfo {  get; set; }

        public enum enmode { AddNew=0, Update=1 }

        public enmode mode = enmode.AddNew;

        public clsInternationalLicense()
        {
            InterLicenseID = 0;
            ApplicationId = 0;
            DriverID = 0;
            LocalLicenseID = 0;

            IssueDate = new DateTime();
            ExpirationDate = new DateTime();
            IsActive = false;
            CreatedUserID = 0;
           
        }
      
        private clsInternationalLicense(int interLicenseID, int applicationID, int driverID
            , int localLicenseID, DateTime issueDate, DateTime expirationDate, bool isActive, int createdUserID
            ,float paidFess, DateTime lastStatusDate, byte appStatus, int personId, DateTime ApplicationDate, byte ApplicationTypeID)
        {

            base.ApplicationID = applicationID;
            base.PaidFess = paidFess;
            base.LastStatusDate = lastStatusDate;
            base.ApplicationStatus = appStatus;
            base.PersonID = personId;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = ApplicationTypeID;

            InterLicenseID = interLicenseID;
            ApplicationId = applicationID;
            DriverID = driverID;

            LocalLicenseID = localLicenseID;
            IssueDate = issueDate;
            ExpirationDate = expirationDate;

            IsActive = isActive;
            CreatedUserID = createdUserID;
      
            LocalLicenseInfo = clsLocalLicenses.FindByID(this.LocalLicenseID);

            DriverInfo = clsDriver.FindByID(this.DriverID);
        }

        public static DataTable GetSpesificInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetSpesificInterLicenseInfo( DriverID);
        }

        public static clsInternationalLicense FindByInterID(int InterLicenseID)
        {

            int ApplicationID = 0, DriverID = 0, LocalLicenseID = 0, CreatedUserID = 0;

            DateTime IssueDate = new DateTime(), ExpirationDate = new DateTime();

            bool IsActive = false;

            bool IsFound = clsInternationalLicenseData.FindByInterID(InterLicenseID, ref ApplicationID, ref DriverID, ref LocalLicenseID
                , ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedUserID);
            
            if (IsFound)
            {
                
                clsApplication application = clsApplication.FindByID(ApplicationID);

                return new clsInternationalLicense(InterLicenseID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate
                    , IsActive, CreatedUserID, application.PaidFess, application.LastStatusDate, application.ApplicationStatus
                    , application.PersonID, application.ApplicationDate, application.ApplicationTypeID);
            }
            else
                return null;
        }

        public static clsInternationalLicense FindByLocalLicenseID(int LocalLicenseID)
        {          
            int ApplicationID = 0, DriverID = 0, InterLicenseID = 0, CreatedUserID = 0;

            DateTime IssueDate = new DateTime(), ExpirationDate = new DateTime();

            bool IsActive = false;

            bool IsFound = clsInternationalLicenseData.FindByLocalLicenseid(ref InterLicenseID, ref ApplicationID, ref DriverID, LocalLicenseID
                , ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedUserID);
           
            if (IsFound)
            {
                clsApplication application = clsApplication.FindByID(ApplicationID);

                return new clsInternationalLicense(InterLicenseID, ApplicationID, DriverID, LocalLicenseID, IssueDate, ExpirationDate
                    , IsActive, CreatedUserID, application.PaidFess, application.LastStatusDate, application.ApplicationStatus
                    , application.PersonID, application.ApplicationDate, application.ApplicationTypeID);
            }
            else
                return null;
        }

        private bool _AddInternationalLicense()
        {

            this.InterLicenseID = clsInternationalLicenseData.AddNewInterLicense(
                this.ApplicationID, this.DriverID, this.LocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.CreatedUserID);


            return (this.InterLicenseID != -1);
        }

        public bool Save()
        {
            base.Mode = (clsApplication.enMode)mode;

            if (!base.Save())
                return false;

            switch (mode)
            {
                case enmode.AddNew:
                {
                    if(_AddInternationalLicense())
                    {
                            mode = enmode.Update;
                            return true;
                    }
                    return false;
                }
                default:
                    return false;
            }

        }

        public static DataTable GetAllInternationalLicenses()
        {
           return clsInternationalLicenseData.GetAllInternationalLicenses();
        }




    }




}