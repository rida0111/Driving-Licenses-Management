using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{


    public class clsDriverData
    {


        public static bool FindByPersonID(int PersonID, ref int DriverID, ref int CreatedUserID, ref DateTime CreatedDate)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"select * from Drivers where PersonID=@PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    DriverID = (int)reader["DriverID"];

                    PersonID = (int)reader["PersonID"];

                    CreatedUserID = (int)reader["CreatedByUserID"];

                    CreatedDate = (DateTime)reader["CreatedDate"];
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

        public static bool FindByID(ref int PersonID, int DriverID, ref int CreatedUserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"select * from Drivers where DriverID=@DriverID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)reader["PersonID"];

                    CreatedUserID = (int)reader["CreatedByUserID"];

                    CreatedDate = (DateTime)reader["CreatedDate"];
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


        public static int AddDriver(int PersonID, int CreatedUserID )
        {

            int ID = -1;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"INSERT INTO Drivers 
                        (PersonID,CreatedByUserID,CreatedDate)
                         VALUES
                        (@PersonID,@CreatedByUserID,@CreatedDate);select SCOPE_IDENTITY(); ";


            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedUserID);

            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

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

        public static DataTable GetAllDrivers()
        {

            DataTable dt = new DataTable();


            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"SELECT  Drivers.DriverID, People.PersonID,People.NationalNo,
                            (People.FirstName + ' ' + People.SecondName + ' ' + 
                                ISNULL(People.ThirdName, '') + ' ' + People.LastName) AS [FullName],
                            Drivers.CreatedDate,
                                (SELECT COUNT(Licenses.DriverID) 
                                FROM Licenses 
                                WHERE Licenses.IsActive = 1 AND Drivers.DriverID = Licenses.DriverID) 
                        FROM Drivers
                        INNER JOIN People ON People.PersonID = Drivers.PersonID 
                        order by FullName;";


            SqlCommand cmd = new SqlCommand(query, connection);


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



    }



}
