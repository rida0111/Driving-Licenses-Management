using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{



    public class clsLocalDrivingLicenseApplicationData
    {


        public static bool FindById(int localdrivinglicenseApplicationID, ref int ApplicationID, ref byte LicenseClassID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from LocalDrivingLicenseApplications

                        where LocalDrivingLicenseApplicationID = @ldlID;";


            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ldlID", localdrivinglicenseApplicationID);

            try
            {

                connection.Open();

                SqlDataReader Reader = cmd.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    ApplicationID = Convert.ToInt16(Reader["ApplicationID"]);

                    LicenseClassID = Convert.ToByte(Reader["LicenseClassID"]);
                }
                else
                    IsFound = false;

                Reader.Close();
            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }

        public static int AddNew(int ApplicationID, byte LicenseClassID)
        {

            int LDLApplicationID = -1;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"INSERT INTO 

                            LocalDrivingLicenseApplications(ApplicationID, LicenseClassID) 

                            VALUES

                            (@ApplicationID,@LicenseClassID);select SCOPE_IDENTITY(); ";


            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {

                connection.Open();

                object Result = cmd.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int ldlApplicationID))
                    LDLApplicationID = ldlApplicationID;
                else
                    LDLApplicationID = -1;
            }
            catch
            {
                LDLApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }


            return LDLApplicationID;
        }

        public static bool UpdateInfo(int LDLApp,int ApplicationID,int LicenseClassID)
        {

            int IsEffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"UPDATE LocalDrivingLicenseApplications

                            SET ApplicationID = @ApplicationID ,LicenseClassID = @LicenseClassID

                        where LocalDrivingLicenseApplicationID=@LDLApp";



            SqlCommand cmd = new SqlCommand(query, connection);


           
            cmd.Parameters.AddWithValue("@LDLApp", LDLApp);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {
                connection.Open();

                IsEffected = cmd.ExecuteNonQuery();
            }
            catch
            {
                IsEffected = 0;
            }
            finally
            {
                connection.Close();
            }


            return (IsEffected > 0);
        }

        public static DataTable GetAllLocalDrivingApplication()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from LocalDrivingLicenseApplications_View
                                  order by ApplicationDate  desc;";

            SqlCommand cmd = new SqlCommand(Query, connection);

            try
            {
                connection.Open();

                SqlDataReader Reader = cmd.ExecuteReader();

                if (Reader.HasRows)
                    dt.Load(Reader);
                else
                    dt = null;              

                Reader.Close();
            }
            catch
            {
                dt = null;
            }
            finally
            {
                connection.Close();
            }

           
            return dt;
        }

        public static bool DeleteLocalLicenseApplication(int LDLAppID)
        {

            int Effected = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"DELETE FROM  LocalDrivingLicenseApplications
                                  
                            WHERE LocalDrivingLicenseApplicationID=@LDLAppID";


            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            try
            {
                connection.Open();

                Effected = cmd.ExecuteNonQuery();
            }
            catch
            {
                Effected = 0;
            }
            finally
            {
                connection.Close();
            }


            return (Effected > 0);
        }

        public static int IsApplicationExist(int PersonID, byte AppStatus, string ClassName)
        {

            int LDLApplicationID = -1;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"SELECT  Applications.ApplicationID

                            FROM            
                            Applications  
                            INNER JOIN 
                            LocalDrivingLicenseApplications 
                            ON 
                            Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID 
                            INNER JOIN
                            LicenseClasses 
                            ON 
                            LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID

                            where

                            Applications.ApplicationTypeID=1 and Applications.ApplicantPersonID=@PersonID 
                            and LicenseClasses.ClassName=@ClassName and Applications.ApplicationStatus=@AppStatus";


            SqlCommand cmd = new SqlCommand(Query, connection);


            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            cmd.Parameters.AddWithValue("@ClassName", ClassName);

            cmd.Parameters.AddWithValue("@AppStatus", AppStatus);

            try
            {
                connection.Open();

                SqlDataReader Reader = cmd.ExecuteReader();

                if (Reader.Read())
                    LDLApplicationID = Convert.ToInt32(Reader["ApplicationID"]);
                else
                    LDLApplicationID = -1;

                Reader.Close();
            }
            catch
            {
                LDLApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }


            return LDLApplicationID;
        }

        public static bool DoesPassTestType(int ldlApplicationID, byte TestTpeID)
        {

            bool isFound = false;


            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"SELECT  Passed=1 FROM  TestAppointments 
                    INNER JOIN
                    LocalDrivingLicenseApplications
                    ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID
                    INNER JOIN
                    Tests 
                    ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID

                    where TestResult=1 and TestAppointments.LocalDrivingLicenseApplicationID=@ldlApplicationID and TestTypeID=@TestTpeID";


            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ldlApplicationID", ldlApplicationID);

            cmd.Parameters.AddWithValue("@TestTpeID", TestTpeID);


            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                    isFound = true;
                else
                    isFound = false;

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }

        public static int CountTrialTest(int ldlApplicationID, byte TestTpeID)
        {

            int Count = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"SELECT count(TestResult)
                    FROM   TestAppointments 
                    INNER JOIN
                    LocalDrivingLicenseApplications ON TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID INNER JOIN
                    Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                    where Tests.TestResult=0 and TestAppointments.LocalDrivingLicenseApplicationID=@ldlApplicationID and 
                    TestAppointments.TestTypeID=@TestTpeID";


            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ldlApplicationID", ldlApplicationID);
            cmd.Parameters.AddWithValue("@TestTpeID", TestTpeID);

            try
            {
                connection.Open();

                object Result = cmd.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int Count_Fail))
                    Count = Count_Fail;
                else
                    Count = 0;
            }
            catch
            {
                Count = 0;
            }
            finally
            {
                connection.Close();
            }

            return Count;
        }

        public static bool IsHasTestAppointment(int LDLAppID, byte TestTypeID)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"select found=1 from TestAppointments

                       where LocalDrivingLicenseApplicationID=@LDLAppID  and IsLocked=0 and TestTypeID=@TestTypeID";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                Connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                    IsFound = true;
                else
                    IsFound = false;

                reader.Close();
            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }


            return IsFound;
        }

        public static bool IsHasTestAppointment(int LDLAppID)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"select found=1 from TestAppointments where LocalDrivingLicenseApplicationID=@LDLAppID";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);


            sqlCommand.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            try
            {

                Connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                    IsFound = true;
                else
                    IsFound = false;

                reader.Close();
            }
            catch
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }


            return IsFound;
        }

    }



}