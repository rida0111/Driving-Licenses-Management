using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{


    public class clsCountryData
    {

        public static DataTable GetAllCountries()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"select CountryName from Countries";


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

        public static bool FindCountryByName(string CountryName,ref byte CountryID)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"SELECT * from Countries where CountryName = @CountryName;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    CountryName = Convert.ToString(reader["CountryName"]);

                    CountryID = Convert.ToByte(reader["CountryID"]);
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


        public static bool FindCountryByID(byte CountryID,ref string CountryName)
        {
            bool IsFound = false;
         
            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"SELECT * from Countries where CountryID=@CountryID";

            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@CountryID", CountryID);


            try
            {

                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    CountryID = Convert.ToByte(reader["CountryID"]);

                    CountryName = (string)reader["CountryName"];
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


    }



}
