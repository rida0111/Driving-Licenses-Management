using DataAccessLayer;
using System;
using System.Data;



namespace Businesses_Access_Layer
{


    public class clsPerson
    {

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public DateTime DateofBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte NationalCountryId { get; set; }
        public string ImagePath { get; set; }
        private enum enMode { AddNew = 0, Update = 1 };

        private  enMode _Mode;

        public string FullName
        {
            get { return this.FirstName + ' ' + this.SecondName + ' ' + this.ThirdName + ' ' + this.LastName; }
        }

        public string CountryName
        {
            get { return clsCountry.FindByID(this.NationalCountryId).CountryName; }
        }


        public clsPerson()
        {
            PersonID = -1;
            NationalCountryId = 0;
            Gendor = 0;

            NationalNo = "";
            FirstName = "";
            LastName = "";

            ThirdName = "";
            SecondName = "";
            DateofBirth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            Email = "";
            Phone = "";
            Address = "";

            ImagePath = "";

            _Mode = enMode.AddNew;
        }


        private clsPerson(int PersonId, string nationalNo, string firstName, string lastName, string secondName
            , string thirdName, DateTime dateofBirth, byte gendor, string address, string phone, string email
            , byte CountryId, string imagePath)
        {

            PersonID = PersonId;
            NationalCountryId = CountryId;
            Gendor = gendor;
            NationalNo = nationalNo;
            FirstName = firstName;
            LastName = lastName;
            ThirdName = thirdName;
            SecondName = secondName;
            DateofBirth = dateofBirth;
            Email = email;
            Phone = phone;
            Address = address;
            ImagePath = imagePath;

            _Mode = enMode.Update;
        }


        public static clsPerson FindById(int PersonId)
        {

            DateTime dateofBirth = new DateTime();

            byte CountryId = 0, gendor = 0;

            string nationalNo = "", firstName = "", lastName = "", secondName = ""

           , thirdName = "", address = "", phone = "", email = "", imagePath = "";

            bool IsFound = clsPersonData.FindByid(PersonId, ref firstName, ref lastName, ref secondName, ref thirdName, ref nationalNo
                , ref gendor, ref dateofBirth, ref address, ref phone, ref email, ref CountryId, ref imagePath);

            if (IsFound)
            {
                return new clsPerson(PersonId, nationalNo, firstName, lastName, secondName, thirdName
                    , dateofBirth, gendor, address, phone, email, CountryId, imagePath);
            }
            else
                return null;
        }

        public static clsPerson FindByNationalNo(string nationalNo)
        {

            int PersonId = 0;

            DateTime dateofBirth = new DateTime();

            byte CountryId = 0, gendor = 0;

            string firstName = "", lastName = "", secondName = ""

           , thirdName = "", address = "", phone = "", email = "", imagePath = "";


            bool IsFound = clsPersonData.FindByNationalNo(nationalNo, ref PersonId, ref firstName, ref lastName, ref secondName
                 , ref thirdName, ref gendor, ref dateofBirth, ref address, ref phone, ref email
                   , ref CountryId, ref imagePath);

            if (IsFound) 
            {
                return new clsPerson(PersonId, nationalNo, firstName, lastName, secondName, thirdName
                    , dateofBirth, gendor, address, phone, email, CountryId, imagePath);
            }
            else
                return null;
        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.LastName, this.SecondName, this.ThirdName
                , this.NationalNo, this.Gendor, this.DateofBirth, this.Address, this.Phone, this.Email
                , this.NationalCountryId, this.ImagePath);

            return (PersonID != -1);
        }


        private bool UpdaePersonInfo()
        {
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.LastName, this.SecondName, this.ThirdName
                , this.NationalNo, this.Gendor, this.DateofBirth, this.Address, this.Phone, this.Email
                , this.NationalCountryId, this.ImagePath);
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                {
                    if(_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    return false;
                }                                        
                case enMode.Update:
                    return UpdaePersonInfo();
                default:
                    return false;
            }
        }

        public static bool DeletePerson(int PersonId)
        {
            return clsPersonData.DeletePerson(PersonId);
        }

        public static DataTable GetAllPerson()
        {
            return clsPersonData.GetAllPerson();
        }

        public int AddNewLDLApplication(string LicenseClassName, int UserID)
        {
            clsApplication Application = new clsApplication();

            Application.PersonID = this.PersonID;

            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (byte)clsApplication.enApplicationType.NewLocalDrivingLicense;

            Application.LastStatusDate = DateTime.Now;

            Application.ApplicationStatus = (byte)clsApplication.enStatus.New;

            Application.PaidFess = clsApplicationType.Find(clsApplication.enApplicationType.NewLocalDrivingLicense).Fees;

            Application.UserID = UserID;

            if (Application.Save())
            {
                clsLocalDrivingLicenseApplication LdLApplication = new clsLocalDrivingLicenseApplication();

                LdLApplication.ApplicationID = Application.ApplicationID;

                LdLApplication.LicenseClassID = clsLicenseClass.FindByName(LicenseClassName).LicenseClassID;

                if (LdLApplication.Save())
                    return LdLApplication.LdlApplicationID;
            }

            return -1;
        }



    }


}
