using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Data_Base_of_DVLM
{

    public class clsDetainLicenseData
    {

        public static bool FindByID(ref int DetainId,int LicenseID,ref DateTime DetainDate,ref float FineFees
            ,ref int CreatedUserID,ref bool IsReleased, ref DateTime ReleaseDate,ref int ReleasedUserID, ref int ReleaseAppID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "select * from DetainedLicenses where LicenseID=@LicenseID and IsReleased=0";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    DetainId = Convert.ToInt16(reader["DetainID"]);

                    DetainDate = (DateTime)reader["DetainDate"];

                    FineFees = Convert.ToSingle(reader["FineFees"]);

                    CreatedUserID = Convert.ToInt16(reader["CreatedByUserID"]);

                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);

                    if(reader["ReleaseDate"]!=DBNull.Value)
                        ReleaseDate = (DateTime)reader["ReleaseDate"];
                    
                    if (reader["ReleaseApplicationID"] != DBNull.Value)
                        ReleaseAppID = Convert.ToInt16(reader["ReleaseApplicationID"]);

                    if (reader["ReleasedByUserID"] != DBNull.Value)
                        ReleasedUserID = Convert.ToInt16(reader["ReleasedByUserID"]);             
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

        public static int AddNewDetainLicense(int LicenseID,  DateTime DetainDate, float FineFees
            , int CreatedUserID,  bool IsReleased,  DateTime ReleaseDate,int ReleasedUserID,  int ReleaseAppID)
        {

            int ID = -1;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses
                 (LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased,ReleaseDate,ReleasedByUserID,ReleaseApplicationID)
                 VALUES
                 (@LicenseID,@DetainDate,@FineFees,@CreatedUserID,@IsReleased,@ReleaseDate,@ReleasedUserID,@ReleaseAppID) 
                 select SCOPE_IDENTITY();";


            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);

            cmd.Parameters.AddWithValue("@FineFees", FineFees);
            cmd.Parameters.AddWithValue("@CreatedUserID", CreatedUserID);

            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);


            if (ReleaseDate == new DateTime())
                cmd.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            
            if (ReleasedUserID == -1)
                cmd.Parameters.AddWithValue("@ReleasedUserID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ReleasedUserID", ReleasedUserID);

            if (ReleaseAppID == -1)
                cmd.Parameters.AddWithValue("@ReleaseAppID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ReleaseAppID", ReleaseAppID);

            try
            {
                connection.Open();

                object Result = cmd.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int result))
                {
                    ID = result;
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
                connection.Close();
            }

            return ID;
        }

        public static bool UpdateDetainLicenseInfo(int DetainID ,int LicenseID, DateTime DetainDate, float FineFees
            , int CreatedUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedUserID, int ReleaseAppID)
        {

            int IsEffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"UPDATE DetainedLicenses

                           SET LicenseID =@LicenseID ,DetainDate =@DetainDate

                           ,FineFees =@FineFees ,CreatedByUserID =@CreatedUserID 

                           ,IsReleased =@IsReleased ,ReleaseDate =@ReleaseDate 

                           ,ReleasedByUserID =@ReleasedUserID ,ReleaseApplicationID =@ReleaseAppID

                           WHERE DetainID=@DetainID;";

            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@DetainID", DetainID);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            cmd.Parameters.AddWithValue("@DetainDate", DetainDate);

            cmd.Parameters.AddWithValue("@FineFees", FineFees);

            cmd.Parameters.AddWithValue("@CreatedUserID", CreatedUserID);

            cmd.Parameters.AddWithValue("@IsReleased", IsReleased);


            if (ReleaseDate == new DateTime())
                cmd.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);

            if (ReleasedUserID == -1)
                cmd.Parameters.AddWithValue("@ReleasedUserID", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ReleasedUserID", ReleasedUserID);
            
            if (ReleaseAppID == -1)
                cmd.Parameters.AddWithValue("@ReleaseAppID", DBNull.Value);           
            else  
                cmd.Parameters.AddWithValue("@ReleaseAppID", ReleaseAppID);
            
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

        public static bool IsLicenseDetained(int LicenseID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select found=1 from DetainedLicenses 
                             where LicenseID=@LicenseID and IsReleased=0";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader read = cmd.ExecuteReader();

                if (read.Read())
                    IsFound = true;
                else
                    IsFound = false;

                read.Close();
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

        public static DataTable GetAllDetainedLicenses()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"SELECT  DetainedLicenses.DetainID, DetainedLicenses.LicenseID, DetainedLicenses.DetainDate, DetainedLicenses.IsReleased, DetainedLicenses.FineFees
                        , DetainedLicenses.ReleaseDate, People.NationalNo,People.FirstName+''+ People.SecondName+''+isNull(People.ThirdName,'')+''+People.LastName as FullName
                        , DetainedLicenses.ReleaseApplicationID
                        FROM            DetainedLicenses INNER JOIN
                                                    Licenses ON DetainedLicenses.LicenseID = Licenses.LicenseID INNER JOIN
                                                    Applications ON Licenses.ApplicationID = Applications.ApplicationID INNER JOIN
                                                    People ON Applications.ApplicantPersonID = People.PersonID
                                    	 order by  IsReleased,DetainID";


            SqlCommand cmd = new SqlCommand(query, connection);

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



    }


}
