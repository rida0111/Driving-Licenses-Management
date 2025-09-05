using DataAccessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;


namespace Data_Base_of_DVLM
{


    public class clsLicenseData
    {


        public static bool FindByLicenseID(int licenseId,ref int applicationId, ref int driverId, ref byte licenseClass
          , ref DateTime issueDate, ref DateTime expirationDate, ref string notes
          , ref float paidFees, ref bool isActive, ref int issueReason, ref int userID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "SELECT * from Licenses where LicenseID=@licenseId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@licenseId", licenseId);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    applicationId = (int)reader["ApplicationID"];

                    driverId = (int)reader["DriverID"];

                    licenseClass = Convert.ToByte(reader["LicenseClass"]);

                    issueDate = (DateTime)reader["IssueDate"];

                    expirationDate = (DateTime)reader["ExpirationDate"];

                    if (reader["Notes"] == DBNull.Value)
                        notes = "";
                    else
                        notes = (string)reader["Notes"];
                    

                    paidFees = Convert.ToSingle(reader["PaidFees"]);

                    isActive = (bool)reader["IsActive"];

                    issueReason = Convert.ToInt32(reader["IssueReason"]);

                    userID = Convert.ToInt32(reader["CreatedByUserID"]);
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

        public static bool FindByApplicationID(ref int licenseId, int applicationId, ref int driverId, ref byte licenseClass
           , ref DateTime issueDate, ref DateTime expirationDate, ref string notes
           , ref float paidFees, ref bool isActive, ref int issueReason, ref int userID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "SELECT * from Licenses where ApplicationID=@applicationId";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@applicationId", applicationId);


            try
            {

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())
                {
                    IsFound = true;

                    licenseId = (int)reader["LicenseID"];

                    driverId = (int)reader["DriverID"];

                    licenseClass = Convert.ToByte(reader["LicenseClass"]);

                    issueDate = (DateTime)reader["IssueDate"];

                    expirationDate = (DateTime)reader["ExpirationDate"];

                    notes = Convert.ToString(reader["Notes"]);

                    paidFees = Convert.ToSingle(reader["PaidFees"]);

                    isActive = (bool)reader["IsActive"];

                    issueReason = Convert.ToInt32(reader["IssueReason"]);

                    userID = (int)reader["CreatedByUserID"];
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
       
        public static DataTable GetSpecificLicenses(int DriverID)
        {

            DataTable dt = new DataTable();


            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"select 

                            LicenseID ,ApplicationID , LicenseClasses.ClassName

                            ,IssueDate ,ExpirationDate ,IsActive

                            from Licenses  

                            INNER JOIN

                            LicenseClasses 

                            ON Licenses.LicenseClass = LicenseClasses.LicenseClassID 

                            where DriverID=@DriverID";


            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@DriverID", DriverID);


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

        public static int AddNew(int ApplicationID, int DriverID, byte LicenseClass
           , DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees
            , bool IsActive, int IssueReason, int UserID)
        {

            int ID = -1;


            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"INSERT INTO Licenses

                            (ApplicationID,DriverID,LicenseClass,IssueDate,ExpirationDate,Notes,PaidFees

                            ,IsActive,IssueReason,CreatedByUserID)

                            VALUES
                            (@ApplicationID,@DriverID,@LicenseClass ,@IssueDate,@ExpirationDate,@Notes,@PaidFees

                            ,@IsActive,@IssueReason,@UserID);select SCOPE_IDENTITY(); ";


            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            cmd.Parameters.AddWithValue("@IssueDate", IssueDate);

            cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);

            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            cmd.Parameters.AddWithValue("@IsActive", IsActive);

            cmd.Parameters.AddWithValue("@IssueReason", IssueReason);

            cmd.Parameters.AddWithValue("@UserID", UserID);


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

        public static bool IsHasLicense(int ApplicationID, byte LicenseClass)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"select found=1 from Licenses

                            where ApplicationID=@ApplicationID and LicenseClass=@LicenseClass";


            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            cmd.Parameters.AddWithValue("@LicenseClass", LicenseClass);


            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

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
                connection.Close();
            }


            return IsFound;
        }


        public static bool UpdateNew(int LicenseID,int ApplicationID, int DriverID, byte LicenseClass
           , DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees
            , bool IsActive, int IssueReason, int UserID)
        {

            int isEffected = 0;


            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"UPDATE Licenses SET 

                            ApplicationID=@ApplicationID,DriverID=@DriverID

                            ,LicenseClass=@LicenseClass ,IssueDate=@IssueDate

                            ,ExpirationDate=@ExpirationDate ,Notes=@Notes

                            ,PaidFees=@PaidFees ,IsActive=@IsActive 

                            ,IssueReason=@IssueReason ,CreatedByUserID=@UserID

                            where LicenseID=@LicenseID";



            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@LicenseID", LicenseID);

            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            sqlCommand.Parameters.AddWithValue("@DriverID", DriverID);

            sqlCommand.Parameters.AddWithValue("@LicenseClass", LicenseClass);

            sqlCommand.Parameters.AddWithValue("@IssueDate", IssueDate);

            sqlCommand.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            sqlCommand.Parameters.AddWithValue("@Notes", Notes);

            sqlCommand.Parameters.AddWithValue("@PaidFees", PaidFees);

            sqlCommand.Parameters.AddWithValue("@IsActive", IsActive);

            sqlCommand.Parameters.AddWithValue("@IssueReason", IssueReason);

            sqlCommand.Parameters.AddWithValue("@UserID", UserID);


            try
            {
                Connection.Open();

                isEffected = sqlCommand.ExecuteNonQuery();
            }
            catch
            {
                isEffected = 0;
            }
            finally
            {
                Connection.Close();
            }


            return (isEffected > 0);
        }


        public static bool IsHasAnActiveLicense(int ApplicationID,byte LicenseClassID)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"select found=1 from Licenses 
                         where ApplicationID=@ApplicationID and LicenseClass=@LicenseClass and IsActive=1;";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);
           
            sqlCommand.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            
            sqlCommand.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

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
