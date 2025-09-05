using DataAccessLayer;
using System;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{



    public class clsTestData
    {


        public static int AddNew(int TestAppointmentID, byte TestResult
            , string Notes, int CreatedByUserID)
        {

            int ID = -1;


            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"INSERT INTO Tests

                       (TestAppointmentID,TestResult,Notes,CreatedByUserID)

                    VALUES

                        (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);select SCOPE_IDENTITY();
                       
                     UPDATE TestAppointments SET  IsLocked=1
                         WHERE TestAppointmentID=@TestAppointmentID";


            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            cmd.Parameters.AddWithValue("@TestResult", TestResult);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            if (string.IsNullOrEmpty(Notes))
                cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Notes", Notes);
            
            try
            {

                connection.Open();

                object Result = cmd.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int result))
                    ID = result;
                else
                    ID = -1;
            }

            catch
            {
                ID = -1;
            }

            finally
            {
                connection.Close();
            }


            return ID;
        }

        public static byte NumberofPassedTest(int ldlApplicationID)
        {

            byte CountPassedTest = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"SELECT  Count(Tests.TestResult)
                FROM TestAppointments 
                INNER JOIN
                Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID 
                INNER JOIN
                LocalDrivingLicenseApplications
                ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID

                where Tests.TestResult=1 AND TestAppointments.LocalDrivingLicenseApplicationID=@ldlApplicationID";


            SqlCommand cmd = new SqlCommand(Query, connection);


            cmd.Parameters.AddWithValue("@ldlApplicationID", ldlApplicationID);


            try
            {

                connection.Open();

                object Result = cmd.ExecuteScalar();

                if (Result != null && byte.TryParse(Result.ToString(), out byte Count))
                    CountPassedTest = Count;
                else
                    CountPassedTest = 0;
            }
            catch
            {
                CountPassedTest = 0;
            }
            finally
            {
                connection.Close();
            }


            return CountPassedTest;
        }

      
     
    }



}
