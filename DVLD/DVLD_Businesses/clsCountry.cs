using Data_Base_of_DVLM;
using System.Data;




namespace Businesses_Access_Layer
{



    public class clsCountry
    {

        public byte CountryID { get; set; }
        public string CountryName { get; set; }

        clsCountry()
        {
            CountryID = 0;
            CountryName = "";
        }

        clsCountry(byte countyID,string countryName)
        {
            CountryID = countyID;
            CountryName = countryName;  
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }

        public static clsCountry FindByID(byte CountryID)
        {
            string CountryName = "";

            bool IsFound = clsCountryData.FindCountryByID(CountryID,ref CountryName);

            if (IsFound)
                return new clsCountry(CountryID, CountryName);       
            else
                return null;
        }

        public static clsCountry FindByName(string CountryName)
        {
            byte CountryID = 0;

            bool IsFound = clsCountryData.FindCountryByName(CountryName,ref CountryID);

            if (IsFound)
                return new clsCountry(CountryID, CountryName);
            else
                return null;
        }
      

    }




}
