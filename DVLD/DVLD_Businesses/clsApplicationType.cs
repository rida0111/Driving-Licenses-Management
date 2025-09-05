using Data_Base_of_DVLM;
using System;
using System.Data;



namespace Businesses_Access_Layer
{


    public class clsApplicationType
    {

        public int ApplicationTypeID { get; set; }

        public string Title { get; set; }

        public Single Fees { get; set; }

        public clsApplicationType()
        {
            ApplicationTypeID = 0;
            Title = "";
            Fees = 0;
        }

        clsApplicationType(int applicationTypeID, string title, Single fees)
        {
            ApplicationTypeID = applicationTypeID;
            Title = title;
            Fees = fees;
        }

        public static clsApplicationType Find(clsApplication.enApplicationType ApplicationTypeID)
        {
            string Title = "";
            Single Fees = 0;

            bool IsFound = clsApplicationTypeData.FindbyID((int)ApplicationTypeID, ref Title, ref Fees);

            if (IsFound)
                return new clsApplicationType((int)ApplicationTypeID, Title, Fees);
            else
                return null;
        }


        public static clsApplicationType FindByTitle(string Title)
        {
            Single Fees = 0;
            int ApplicationTypeID = 0;

            bool IsFound = clsApplicationTypeData.FindByTitle(ref ApplicationTypeID, Title, ref Fees);

            if (IsFound)
                return new clsApplicationType(ApplicationTypeID, Title, Fees);
            else
                return null;

        }


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }

        private bool _Update()
        {
            return clsApplicationTypeData.UpadateAppTypesInfo(this.ApplicationTypeID, this.Title, this.Fees);
        }
  
        public bool Save()
        {
            return _Update();
        }

    }




}
