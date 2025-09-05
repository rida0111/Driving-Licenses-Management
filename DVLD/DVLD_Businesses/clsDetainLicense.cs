using Data_Base_of_DVLM;
using System;
using System.Data;




namespace Businesses_Access_Layer
{



    public class clsDetainLicense
    {

        public int DetainId { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedUserID { get; set; }
        public int ReleaseApplicationID { get; set; }

        enum enMode { AddNew = 0, Update = 1 }

        enMode _Mode;

        public clsUser UserInfo;

        public clsDetainLicense()
        {
            DetainId = -1;
            LicenseID = -1;
            DetainDate = new DateTime();

            FineFees = -1;
            CreatedUserID = -1;
            IsReleased = false;

            ReleaseDate = new DateTime();
            ReleasedUserID = -1;
            ReleaseApplicationID = -1;

            UserInfo = new clsUser();

            _Mode = enMode.AddNew;
        }

        private clsDetainLicense(int detainId, int licenseID, DateTime detainDate, float fineFees,
                         int createdUserID, bool isReleased, DateTime releaseDate, int releasedUserID, int releaseAppID)
        {
            DetainId = detainId;
            LicenseID = licenseID;
            DetainDate = detainDate;

            FineFees = fineFees;
            CreatedUserID = createdUserID;
            IsReleased = isReleased;

            ReleaseDate = releaseDate;
            ReleasedUserID = releasedUserID;
            ReleaseApplicationID = releaseAppID;

            UserInfo = clsUser.FindByID(this.CreatedUserID);

            _Mode = enMode.Update;
        }

        public static clsDetainLicense FindByID(int licenseID)
        {
            bool isReleased = false;

            int detainId = -1, createdUserID = -1, releasedUserID = -1, releaseAppID = -1;

            float fineFees = -1;

            DateTime releaseDate = new DateTime(), detainDate = new DateTime();

            bool IsFound = clsDetainLicenseData.FindByID(ref detainId, licenseID, ref detainDate, ref fineFees
                    , ref createdUserID, ref isReleased, ref releaseDate, ref releasedUserID, ref releaseAppID);

            if (IsFound)
            {
                return new clsDetainLicense(detainId, licenseID, detainDate, fineFees
                    , createdUserID, isReleased, releaseDate, releasedUserID, releaseAppID);
            }
            else
                return null;

        }

        private bool AddDetainLicense()
        {
            this.DetainId = clsDetainLicenseData.AddNewDetainLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedUserID
                , this.IsReleased, this.ReleaseDate, this.ReleasedUserID, this.ReleaseApplicationID);

            return (this.DetainId != -1);
        }

        private bool UpdateDetainLicense()
        {
            return clsDetainLicenseData.UpdateDetainLicenseInfo(this.DetainId, this.LicenseID, this.DetainDate, this.FineFees
                , this.CreatedUserID, this.IsReleased, this.ReleaseDate, this.ReleasedUserID, this.ReleaseApplicationID);
        }

        public bool Save()
        {

            switch (_Mode)
            {

                case enMode.AddNew:
                    {
                        if (AddDetainLicense())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        return false;
                    }

                case enMode.Update:
                    {
                        return UpdateDetainLicense();
                    }
                default:
                    return false;
            }


        }

        public  bool IsLicenseDetained()
        {
            return clsDetainLicenseData.IsLicenseDetained(this.LicenseID);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainLicenseData.GetAllDetainedLicenses();
        }


    }


}

