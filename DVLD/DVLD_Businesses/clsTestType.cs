using Data_Base_of_DVLM;
using System;
using System.Data;




namespace Businesses_Access_Layer
{



    public class clsTestType
    {

        public int TestTypeID { get; set; }

        public string TestTitle { get; set; }

        public string TestDescription { get; set; }

        public float TestFees { get; set; }


        public  enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }

        public clsTestType()
        {
            TestTypeID = 0;
            TestTitle = "";
            TestDescription = "";
            TestFees = 0;
        }


        public clsTestType(int testTypeID, string testTitle, string testDescription, float testFees)
        {
            TestTypeID = testTypeID;
            TestTitle = testTitle;
            TestDescription = testDescription;
            TestFees = testFees;
        }


        public static clsTestType FindByTestID(clsTestType.enTestType TestType)
        {
            Single testFees = 0;

            string testDescription = "", TestTitle = "";

            bool IsFound = clsTestTypeData.FindByID((int)TestType, ref TestTitle, ref testDescription, ref testFees);

            if (IsFound)
                return new clsTestType((int)TestType, TestTitle, testDescription, testFees);
            else
                return null;
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypeData.GetAllTestTypes();
        }

        public bool Save()
        {
            return clsTestTypeData.EditTestTypesInfo(this.TestTypeID, this.TestTitle,this.TestDescription, this.TestFees);
        }



    }




}
