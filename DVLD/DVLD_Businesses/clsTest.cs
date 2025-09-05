using Data_Base_of_DVLM;



namespace Businesses_Access_Layer
{




    public class clsTest
    {

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public byte TestResult { get; set; }
        public string Note { get; set; }
        public int CreatedByUserID { get; set; }



        public clsTest()
        {
            TestID = -1;
            TestAppointmentID = -1;
            TestResult = 0;
            Note = "";
            CreatedByUserID = -1;
        }


        private bool AddNew()
        {
            this.TestID = clsTestData.AddNew(this.TestAppointmentID, this.TestResult, this.Note, this.CreatedByUserID);

            return (this.TestID != -1);
        }


        public bool Save()
        {
            return AddNew();
        }


        public static byte NumberPassedTests(int ldlAppID)
        {
            return clsTestData.NumberofPassedTest(ldlAppID);
        }


  

    

    }



}
