using DataAccessLayer;
using System;
using System.Data.SqlClient;


namespace Data_Base_of_DVLM
{


    public class clsApplicationData
    {


        public static bool FindbyID(int AppID, ref int PersonId, ref DateTime AppDate, ref byte AppType
            , ref byte AppStatus, ref DateTime LastStatusDate, ref Single PaidFess, ref int UserID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from Applications where ApplicationID=@AppID;";

            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@AppID", AppID);

            try
            {
                Connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {

                    IsFound = true;

                    PersonId = (int)reader["ApplicantPersonID"];
                    AppDate = (DateTime)reader["ApplicationDate"];
                    AppType = Convert.ToByte(reader["ApplicationTypeID"]);

                    AppStatus = Convert.ToByte(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];

                    UserID = (int)reader["CreatedByUserID"];

                    PaidFess = Convert.ToSingle(reader["PaidFees"]);

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
                Connection.Close();
            }


            return IsFound;
        }

        public static int AddNewApplication(int PersonId,byte AppType, byte AppStatus, Single PaidFess, int UserID)
        {

            int ID = -1;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"INSERT INTO Applications (ApplicantPersonID,ApplicationDate,ApplicationTypeID
                        ,ApplicationStatus,LastStatusDate,PaidFees,CreatedByUserID)
                         VALUES
                     (@PersonId,@AppDate,@AppType,@AppStatus,@LastStatusDate,@PaidFess,@UserID);select SCOPE_IDENTITY();";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);


            sqlCommand.Parameters.AddWithValue("@PersonId", PersonId);
            sqlCommand.Parameters.AddWithValue("@AppDate", DateTime.Now);
            sqlCommand.Parameters.AddWithValue("@AppType", AppType);

            sqlCommand.Parameters.AddWithValue("@AppStatus", AppStatus);
            sqlCommand.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            sqlCommand.Parameters.AddWithValue("@PaidFess", PaidFess);
            sqlCommand.Parameters.AddWithValue("@UserID", UserID);

            try
            {

                Connection.Open();

                object Result = sqlCommand.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int AppID))
                {
                    ID = AppID;
                }
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
        public static bool UpdateNew(int ApplicationId, byte AppStatus)
        {

            int isEffected = 0;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"UPDATE Applications SET ApplicationStatus =@AppStatus 
                              ,LastStatusDate=@LastStatusDate
                               where ApplicationID=@ApplicationId";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@ApplicationId", ApplicationId);
            sqlCommand.Parameters.AddWithValue("@AppStatus", AppStatus);
            sqlCommand.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

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
       
        public static bool UpdateStatus(int LDLApplicationID)
        {

            int isEffected = 0;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"UPDATE Applications SET  ApplicationStatus =3,LastStatusDate=@DateTime

                            FROM            
                            LocalDrivingLicenseApplications 
                            INNER JOIN
                            Applications
                            ON 
                            LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID

                            where LocalDrivingLicenseApplicationID=@ldlAppID";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);


            sqlCommand.Parameters.AddWithValue("@ldlAppID", LDLApplicationID);

            sqlCommand.Parameters.AddWithValue("@DateTime", DateTime.Now);

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

        public static bool DeleteApplication(int AppID)
        {

            int Effected = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"DELETE FROM [dbo].[Applications] WHERE ApplicationID=@AppID";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@AppID", AppID);

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



    }


}