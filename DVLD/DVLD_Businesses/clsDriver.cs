using Data_Base_of_DVLM;
using System;
using System.Data;



namespace Businesses_Access_Layer
{



    public class clsDriver
    {

        public int PersonID { get; set; }
        public int DriverID { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        public clsPerson PersonInfo { get; set; }
        public clsDriver()
        {
            PersonID = 0;
            DriverID = 0;
            CreatedUserID = 0;
            CreatedDate = new DateTime();
        }


        private clsDriver(int driverID, int personID, int createdUserID, DateTime createdDate)
        {
            PersonID = personID;
            DriverID = driverID;
            CreatedUserID = createdUserID;
            CreatedDate = createdDate;

            this.PersonInfo = clsPerson.FindById(this.PersonID);
        }


        public static clsDriver FindByPersonID(int personID)
        {
            int driverID = 0, createdUserID = 0;

            DateTime createdDate = new DateTime();

            bool IsFound = clsDriverData.FindByPersonID(personID, ref driverID, ref createdUserID, ref createdDate);

            if (IsFound)         
                return new clsDriver(driverID, personID, createdUserID, createdDate);
            else
                return null;
        }

        public static clsDriver FindByID(int driverID)
        {

            int personID = 0, createdUserID = 0;

            DateTime createdDate = new DateTime();

            bool IsFound = clsDriverData.FindByID(ref personID, driverID, ref createdUserID, ref createdDate);
           
            if (IsFound)          
                return new clsDriver(driverID, personID, createdUserID, createdDate);
            else
                return null;
        }
        private bool AddDriverInfo()
        {
            this.DriverID = clsDriverData.AddDriver(this.PersonID, this.CreatedUserID);

            return (this.DriverID != -1);
        }

        public bool Save()
        {
            return AddDriverInfo();
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

    
    }



}
