using Data_Base_of_DVLM;
using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.InteropServices.WindowsRuntime;
using static Businesses_Access_Layer.clsApplication;



namespace Businesses_Access_Layer
{



    public class clsLocalLicenses
    {

        public int LicenseId { get; set; }
        public int ApplicationId { get; set; }
        public int DriverId { get; set; }
        public byte LicenseClass { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public int UserID { get; set; }
        public clsApplication ApplicationInfo { get; set; }
        public clsLicenseClass LicenseClassInfo { get; set; }
        public clsUser UserInfo { get; set; }

        public clsDetainLicense DetainInfo { get; set; }
        enum enMode { AddNew = 0, Update = 1 }

        enMode _Mode;

        public enum enIssueReason { FirstTime = 1, Renew = 2, Replacementforlost = 4, ReplacementforDamaged = 3 }

        public string IssueReasonName
        {
            get { return IssueReasonText(); }
        }

        public bool IsDetained
        {
            get { return clsDetainLicense.FindByID(this.LicenseId) != null; }
        }

        private string IssueReasonText()
        {
            string IssueReasonName = "";

            switch (this.IssueReason)
            {
                case enIssueReason.FirstTime:
                    IssueReasonName = "First Time";
                    break;
                case enIssueReason.Renew:
                    IssueReasonName = "Renew";
                    break;
                case enIssueReason.ReplacementforDamaged:
                    IssueReasonName = "Replacement for Damaged";
                    break;
                case enIssueReason.Replacementforlost:
                    IssueReasonName = "Replacement for lost";
                    break;

                default:
                    IssueReasonName = "";
                    break;
            }

            return IssueReasonName;
        }

        private clsLocalLicenses(int licenseId, int applicationId, int driverId, byte licenseClass
            , DateTime issueDate, DateTime expirationDate, string notes
            , float paidFees, bool isActive, enIssueReason issueReason, int userID)
        {

            this.LicenseId = licenseId;
            this.ApplicationId = applicationId;
            this.DriverId = driverId;

            this.LicenseClass = licenseClass;
            this.IssueDate = issueDate;
            this.ExpirationDate = expirationDate;

            this.Notes = notes;
            this.PaidFees = paidFees;
            this.IsActive = isActive;

            this.IssueReason = issueReason;
            this.UserID = userID;

            this.ApplicationInfo = clsApplication.FindByID(this.ApplicationId);

            this.LicenseClassInfo = clsLicenseClass.FindByID(this.LicenseClass);

            this.UserInfo = clsUser.FindByID(this.UserID);

            DetainInfo = clsDetainLicense.FindByID(this.LicenseId);

            this._Mode = enMode.Update;
        }


        public clsLocalLicenses()
        {
            this.LicenseId = -1;
            this.ApplicationId = -1;
            this.DriverId = -1;

            this.LicenseClass = 0;
            this.IssueDate = new DateTime();
            this.ExpirationDate = new DateTime();

            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;

            this.IssueReason = enIssueReason.FirstTime;
            this.UserID = -1;

            this._Mode = enMode.AddNew;
        }


        public static clsLocalLicenses FindByID(int licenseId)
        {
            int ApplicationId = 0, driverId = 0, issueReason = 0, userID = 0;
            float paidFees = 0;

            byte licenseClass = 0;
            DateTime issueDate = new DateTime(), expirationDate = new DateTime();
            string notes = "";

            bool isActive = false;

            bool IsFound = clsLicenseData.FindByLicenseID(licenseId, ref ApplicationId, ref driverId, ref licenseClass
                , ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref userID);


            if (IsFound)
                return new clsLocalLicenses(licenseId, ApplicationId, driverId, licenseClass, issueDate, expirationDate
                    , notes, paidFees, isActive, (enIssueReason)issueReason, userID);
            else
                return null;
        }

        public static clsLocalLicenses FindByApplicationID(int ApplicationId)
        {
            int licenseId = 0, driverId = 0, issueReason = 0, userID = 0;
            float paidFees = 0;

            byte licenseClass = 0;

            DateTime issueDate = new DateTime(), expirationDate = new DateTime();

            string notes = "";

            bool isActive = false;

            bool IsFound = clsLicenseData.FindByApplicationID(ref licenseId, ApplicationId, ref driverId, ref licenseClass
                , ref issueDate, ref expirationDate, ref notes, ref paidFees, ref isActive, ref issueReason, ref userID);

            if (IsFound)
                return new clsLocalLicenses(licenseId, ApplicationId, driverId, licenseClass
                , issueDate, expirationDate, notes, paidFees, isActive, (enIssueReason)issueReason, userID);
            else
                return null;
        }

        public static DataTable GetSpecificLocalLicenses(int DriverID)
        {
            return clsLicenseData.GetSpecificLicenses(DriverID);
        }

        private bool _AddNewLocalLicense()
        {
            this.LicenseId = clsLicenseData.AddNew(this.ApplicationId, this.DriverId, this.LicenseClass, this.IssueDate
                , this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (int)this.IssueReason, this.UserID);

            return (this.LicenseId != -1);
        }


        private bool _UpdateLocalLicense()
        {
            return clsLicenseData.UpdateNew(this.LicenseId, this.ApplicationId, this.DriverId, this.LicenseClass
                , this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (int)this.IssueReason, this.UserID);
        }


        public bool Save()
        {

            switch (_Mode)
            {

                case enMode.AddNew:
                    {
                        if (_AddNewLocalLicense())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }

                case enMode.Update:
                    {
                        return _UpdateLocalLicense();
                    }

                default:
                    return false;

            }


        }

        public static bool IsHasLicense(int ApplicationID, byte LicenseClass)
        {
            return clsLicenseData.IsHasLicense(ApplicationID, LicenseClass);
        }

        public clsLocalLicenses RenewLocalLicense(int UserID, string Notes)
        {

            clsApplication clsApplication = new clsApplication();

            clsApplication.PersonID = this.ApplicationInfo.PersonID;

            clsApplication.ApplicationDate = DateTime.Now;

            clsApplication.ApplicationStatus = (byte)clsApplication.enStatus.Completed;

            clsApplication.ApplicationTypeID = (byte)clsApplication.enApplicationType.RenewLicense;

            clsApplication.PaidFess = clsApplicationType.Find((clsApplication.enApplicationType)clsApplication.ApplicationTypeID).Fees;

            clsApplication.UserID = UserID;

            clsApplication.LastStatusDate = DateTime.Now;


            if (clsApplication.Save())
            {
                clsLocalLicenses clsNewLicense = new clsLocalLicenses();

                clsNewLicense.ApplicationId = clsApplication.ApplicationID;

                clsNewLicense.DriverId = this.DriverId;

                clsNewLicense.LicenseClass = this.LicenseClass;

                clsNewLicense.IssueDate = DateTime.Now;

                clsNewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);

                clsNewLicense.Notes = Notes;

                clsNewLicense.PaidFees = this.PaidFees;

                clsNewLicense.IsActive = true;

                clsNewLicense.IssueReason = clsLocalLicenses.enIssueReason.Renew;

                clsNewLicense.UserID = this.UserID;


                if (clsNewLicense.Save())
                {
                    this.IsActive = false;

                    if (this.Save())
                        return clsNewLicense;
                }
            }

            return null;
        }

        public bool IsLicenseActive()
        {
            return this.IsActive;
        }

        public clsLocalLicenses Replace(byte ApplicationTypeID, int UserID)
        {
            clsApplication clsApplication = new clsApplication();

            clsApplication.PersonID = this.ApplicationInfo.PersonID;

            clsApplication.ApplicationDate = DateTime.Now;

            clsApplication.ApplicationTypeID = ApplicationTypeID;

            clsApplication.PaidFess = clsApplicationType.Find((clsApplication.enApplicationType)ApplicationTypeID).Fees;

            clsApplication.ApplicationStatus = (byte)clsApplication.enStatus.Completed;

            clsApplication.LastStatusDate = DateTime.Now;

            clsApplication.UserID = UserID;

            if (clsApplication.Save())
            {
                clsLocalLicenses NewLicense = new clsLocalLicenses();

                NewLicense.ApplicationId = clsApplication.ApplicationID;

                NewLicense.DriverId = this.DriverId;

                NewLicense.LicenseClass = this.LicenseClass;

                NewLicense.IssueDate = this.IssueDate;

                NewLicense.ExpirationDate = this.ExpirationDate;

                NewLicense.Notes = this.Notes;

                NewLicense.PaidFees = this.PaidFees;

                NewLicense.IsActive = this.IsActive;

                NewLicense.IssueReason = (enIssueReason)clsApplication.ApplicationTypeID;

                NewLicense.UserID = UserID;

                if (NewLicense.Save())
                {
                    this.IsActive = false;

                    if (this.Save())
                        return NewLicense;
                }

            }

            return null;
        }

        public int ReleaseDetainLicense(int UserID)
        {
            clsApplication clsApplication = new clsApplication();

            clsApplication.PersonID = this.ApplicationInfo.PersonID;

            clsApplication.ApplicationDate = DateTime.Now;

            clsApplication.LastStatusDate = DateTime.Now;

            clsApplication.ApplicationStatus = (byte)clsApplication.enStatus.Completed;

            clsApplication.ApplicationTypeID = (byte)clsApplication.enApplicationType.ReleaseDetainLicense;

            clsApplication.PaidFess = clsApplicationType.Find(clsApplication.enApplicationType.ReleaseDetainLicense).Fees;

            clsApplication.UserID = UserID;


            if (clsApplication.Save())
            {
                clsDetainLicense clsDetainLicense = clsDetainLicense.FindByID(this.LicenseId);

                clsDetainLicense.IsReleased = true;

                clsDetainLicense.ReleaseDate = DateTime.Now;

                clsDetainLicense.ReleasedUserID = clsApplication.UserID;

                clsDetainLicense.ReleaseApplicationID = clsApplication.ApplicationID;

                if (clsDetainLicense.Save())
                    return clsDetainLicense.ReleaseApplicationID;
            }

            return -1;
        }

        public int DetainLicense(float FineFees, int UserID)
        {
            clsDetainLicense clsDetainLicense = new clsDetainLicense();

            clsDetainLicense.LicenseID = this.LicenseId;

            clsDetainLicense.DetainDate = DateTime.Now;

            clsDetainLicense.FineFees = FineFees;

            clsDetainLicense.CreatedUserID = UserID;

            clsDetainLicense.IsReleased = false;

            return (clsDetainLicense.Save()) ? clsDetainLicense.DetainId : -1;
        }

        public static bool IsHasAnActiveLicense(int ApplicationId, Byte LicenseClass)
        {
            return clsLicenseData.IsHasAnActiveLicense(ApplicationId, LicenseClass);
        }

        public bool IssueIntenationalLicense(int UserID, ref int ApplicationID, ref int InternationnalLicenseID)
        {
            clsInternationalLicense _clsInternationalLicense = new clsInternationalLicense();

            _clsInternationalLicense.PersonID = this.ApplicationInfo.PersonID;
            _clsInternationalLicense.ApplicationDate = DateTime.Now;
            _clsInternationalLicense.LastStatusDate = DateTime.Now;
            _clsInternationalLicense.ApplicationTypeID = (byte)clsApplication.enApplicationType.NewInternationalLicense;

            _clsInternationalLicense.PaidFess = clsApplicationType.Find(clsApplication.enApplicationType.NewInternationalLicense).Fees;
            _clsInternationalLicense.ApplicationStatus = (byte)clsApplication.enStatus.Completed;

            _clsInternationalLicense.UserID = UserID;
            _clsInternationalLicense.ApplicationId = this.ApplicationId;
            _clsInternationalLicense.DriverID = this.DriverId;
            _clsInternationalLicense.LocalLicenseID = this.LicenseId;
            _clsInternationalLicense.IssueDate = DateTime.Now;
            _clsInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _clsInternationalLicense.IsActive = true;
            _clsInternationalLicense.CreatedUserID = UserID;

            if (_clsInternationalLicense.Save())
            {
                ApplicationID = _clsInternationalLicense.ApplicationID;
                InternationnalLicenseID = _clsInternationalLicense.InterLicenseID;
                return true;
            }

            return false;
        }


    }

}




