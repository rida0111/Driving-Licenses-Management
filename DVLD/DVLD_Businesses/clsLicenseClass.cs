using Data_Base_of_DVLM;
using System.Data;



namespace Businesses_Access_Layer
{



    public class clsLicenseClass
    {

        public byte LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAge { get; set; }
        public int DefaultValidityLength { get; set; }
        public int ClassFess { get; set; }


        public clsLicenseClass()
        {
            LicenseClassID = 0;
            ClassName = "";
            ClassDescription = "";

            MinimumAge = -1;
            DefaultValidityLength = -1;
            ClassFess = -1;

        }

        private clsLicenseClass(byte licenseClassID, string className, string classDescription
            , int minimumAge, int defaultValidityLength, int classFess)
        {

            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;

            MinimumAge = minimumAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFess = classFess;
        }

        public static clsLicenseClass FindByID(byte licenseClassID)
        {
           
            int minimumAge = 0, defaultValidityLength = 0, classFess = 0;

            string classDescription = "", ClassName = "";

            bool IsFound = clsLicenseClassData.FindByID(licenseClassID, ref ClassName, ref classDescription
                , ref minimumAge, ref defaultValidityLength, ref classFess);

            if (IsFound)
            {
                return new clsLicenseClass(licenseClassID, ClassName, classDescription
                , minimumAge, defaultValidityLength, classFess);
            }
            else
                return null;
        }

        public static clsLicenseClass FindByName(string ClassName)
        {

            byte licenseClassID = 0;

            int minimumAge = 0, defaultValidityLength = 0, classFess = 0;

            string classDescription = "";

            bool IsFound = clsLicenseClassData.FindByName(ref licenseClassID, ClassName, ref classDescription
                , ref minimumAge, ref defaultValidityLength, ref classFess);

            if (IsFound)
            {
                return new clsLicenseClass(licenseClassID, ClassName, classDescription
                , minimumAge, defaultValidityLength, classFess);
            }
            else
                return null;
        }

        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }


    }


}