using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{



    public class clsTestAppointmentData
    {


        public static bool FindByid(int TestAppointmentID, ref byte TestTypeID, ref int LDLApplicationID
            , ref DateTime AppointmentDate, ref Single PaidFees, ref int CreatedByUserID, ref bool IsLocked
            , ref int RetakeTestApplicationID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"select * from TestAppointments  where TestAppointmentID=@TestAppointmentID ";


            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    TestTypeID = Convert.ToByte(reader["TestTypeID"]);

                    LDLApplicationID = Convert.ToInt16(reader["LocalDrivingLicenseApplicationID"]);

                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);

                    CreatedByUserID = Convert.ToInt16(reader["CreatedByUserID"]);

                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = 0;
                    else
                        RetakeTestApplicationID = Convert.ToInt16(reader["RetakeTestApplicationID"]);
                }
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
                connection.Close();
            }

            return IsFound;
        }

        public static DataTable GetSpesificTestAppointment(int LDLAppID, byte TestTypeID)
        {

            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"SELECT  TestAppointmentID,AppointmentDate ,PaidFees , IsLocked 

                            FROM   
                            TestAppointments

                            where LocalDrivingLicenseApplicationID=@LDLAppID 

                            and TestAppointments.TestTypeID=@TestTypeID
                            order by AppointmentDate desc;";



            SqlCommand sqlCommand = new SqlCommand(Query, Connection);


            sqlCommand.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {

                Connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);
                else
                    dt = null;
                
                reader.Close();
            }
            catch
            {
                dt = null;
            }
            finally
            {
                Connection.Close();
            }


            return dt;
        }

        public static int AddTestAppointment(byte TestTypeID, int LDLAppID, DateTime AppointmentDate
            , Single PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {

            int ID = -1;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"INSERT INTO TestAppointments

                           (TestTypeID,LocalDrivingLicenseApplicationID

                           ,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)

                           VALUES

                           (@TestTypeID,@LDLAppID,@AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked

                           ,@RetakeTestApplicationID);select SCOPE_IDENTITY();";



            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            sqlCommand.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            sqlCommand.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

            sqlCommand.Parameters.AddWithValue("@PaidFees", PaidFees);

            sqlCommand.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            sqlCommand.Parameters.AddWithValue("@IsLocked", IsLocked);


            if (RetakeTestApplicationID == -1)           
                sqlCommand.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else         
                sqlCommand.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            

            try
            {

                Connection.Open();

                object Result = sqlCommand.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int TestAppointmentID))
                    ID = TestAppointmentID;
                else
                    ID = -1;              
            }
            catch
            {
                ID = -1;
            }
            finally
            {
                Connection.Close();
            }

            return ID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, byte TestTypeID, int LDLAppID, DateTime AppointmentDate
            , Single PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {

            int IsEffected = 0;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"UPDATE TestAppointments

                          SET 

                        TestTypeID = @TestTypeID,LocalDrivingLicenseApplicationID = @LDLAppID

                        ,AppointmentDate = @AppointmentDate,PaidFees = @PaidFees ,CreatedByUserID = @CreatedByUserID 

                        ,IsLocked = @IsLocked,RetakeTestApplicationID=@RetakeTestApplicationID

                        where TestAppointmentID=@TestAppointmentID";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            sqlCommand.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            sqlCommand.Parameters.AddWithValue("@LDLAppID", LDLAppID);

            sqlCommand.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);

            sqlCommand.Parameters.AddWithValue("@PaidFees", PaidFees);

            sqlCommand.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            sqlCommand.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == 0)
                sqlCommand.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                sqlCommand.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            

            try
            {
                Connection.Open();
                IsEffected = sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                IsEffected = 0;
            }
            finally
            {
                Connection.Close();
            }


            return (IsEffected > 0);
        }
    
        public static bool IsExistAndLocked(int LDLAppID, byte TestTypeID)
        {

            bool IsFound = false;


            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"select found=1 from TestAppointments

               where LocalDrivingLicenseApplicationID=@LDLAppID  and IsLocked=1 and TestTypeID=@TestTypeID";


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