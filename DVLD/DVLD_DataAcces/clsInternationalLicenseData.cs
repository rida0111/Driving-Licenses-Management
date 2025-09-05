using DataAccessLayer;
using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{


    public class clsInternationalLicenseData
    {


        public static bool FindByInterID(int InternationalID, ref int ApplicationID, ref int DriverID, ref int LocalLicenseID
         , ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "select * from InternationalLicenses where InternationalLicenseID=@Id";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Id", InternationalID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ApplicationID = (int)reader["ApplicationID"];

                    DriverID = (int)reader["DriverID"];

                    LocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];

                    ApplicationID = (int)reader["ApplicationID"];

                    IssueDate = (DateTime)reader["IssueDate"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    IsActive = (bool)reader["IsActive"];

                    CreatedUserID = (int)reader["CreatedByUserID"];

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

        public static bool FindByLocalLicenseid(ref int InternationalID, ref int ApplicationID, ref int DriverID, int LocalLicenseID
            , ref DateTime IssueDate,  ref DateTime ExpirationDate,ref bool IsActive, ref int CreatedUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "select * from InternationalLicenses where IssuedUsingLocalLicenseID=@Id";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Id", LocalLicenseID);

            try
            {

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ApplicationID = Convert.ToInt16(reader["ApplicationID"]);

                    DriverID = Convert.ToInt16(reader["DriverID"]);

                    InternationalID = Convert.ToInt16(reader["InternationalLicenseID"]);

                    ApplicationID = Convert.ToInt16(reader["ApplicationID"]);

                    IssueDate = (DateTime)reader["IssueDate"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    IsActive = (bool)reader["IsActive"];

                    CreatedUserID = Convert.ToInt16(reader["CreatedByUserID"]);

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


        public static bool FindByDriverID(ref int InternationalID, ref int ApplicationID, int DriverID,ref int LocalLicenseID
          , ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "select * from InternationalLicenses where DriverID=@Id";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Id", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ApplicationID = Convert.ToInt16(reader["ApplicationID"]);

                    LocalLicenseID = Convert.ToInt16(reader["IssuedUsingLocalLicenseID"]);

                    InternationalID = Convert.ToInt16(reader["InternationalLicenseID"]);

                    ApplicationID = Convert.ToInt16(reader["ApplicationID"]);

                    IssueDate = (DateTime)reader["IssueDate"];

                    ExpirationDate = (DateTime)reader["ExpirationDate"];

                    IsActive = (bool)reader["IsActive"];

                    CreatedUserID = Convert.ToInt16(reader["CreatedByUserID"]);
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


        public static int AddNewInterLicense(int ApplicationID, int DriverID, int LocalLicenseID
                , DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedUserID)
        {

            int ID = -1;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses

                                (ApplicationID,DriverID,IssuedUsingLocalLicenseID

                                ,IssueDate,ExpirationDate,IsActive,CreatedByUserID)

                                VALUES

                                (@ApplicationID,@DriverID,@LocalLicenseID

                                ,@IssueDate,@ExpirationDate,@IsActive,@CreatedUserID);select SCOPE_IDENTITY(); ";


            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            cmd.Parameters.AddWithValue("@LocalLicenseID", LocalLicenseID);

            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);

            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            cmd.Parameters.AddWithValue("@IsActive", IsActive);

            cmd.Parameters.AddWithValue("@CreatedUserID", CreatedUserID);

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

        public static DataTable GetSpesificInterLicenseInfo(int DriverID)
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"select  InternationalLicenseID,ApplicationID,DriverID
                            ,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive           
                             FROM      
                            InternationalLicenses where DriverID=@DriverID";


            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@DriverID", DriverID);


            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

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
                connection.Close();
            }

            return dt;
        }

        public static DataTable GetAllInternationalLicenses()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"select  InternationalLicenseID ,ApplicationID ,DriverID ,IssuedUsingLocalLicenseID 
                            ,IssueDate ,ExpirationDate,IsActive          
                            FROM   InternationalLicenses";


            SqlCommand cmd = new SqlCommand(query, connection);

           
            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows)
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
                connection.Close();
            }

            return dt;
        }



    }




}
