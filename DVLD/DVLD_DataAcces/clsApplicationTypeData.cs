using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data_Base_of_DVLM
{


    public class clsApplicationTypeData
    {


        public static bool FindbyID(int ID, ref string Title, ref Single Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = "select * from ApplicationTypes where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@ApplicationTypeID", ID);

            try
            {
                Connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    Title = (string)reader["ApplicationTypeTitle"];

                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
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


        public static bool FindByTitle(ref int ID, string Title, ref Single Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from ApplicationTypes where ApplicationTypeTitle = @Title;";

            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@Title", Title);

            try
            {
                Connection.Open();

                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    ID = Convert.ToInt32(reader["ApplicationTypeID"]);

                    Fees = Convert.ToSingle(reader["ApplicationFees"]);
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


        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from ApplicationTypes 
                                    order by ApplicationTypeTitle ;";

            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

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


   
        public static bool UpadateAppTypesInfo(int ID, string Title, Single Fees)
        {

            int IsEffected = 0;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"UPDATE ApplicationTypes 

                            SET ApplicationTypeTitle =@Title ,ApplicationFees =@Fees 

                            WHERE ApplicationTypeID=@ID";


            SqlCommand sqlCommand = new SqlCommand(Query, Connection);

            sqlCommand.Parameters.AddWithValue("@ID", ID);

            sqlCommand.Parameters.AddWithValue("@Title", Title);

            sqlCommand.Parameters.AddWithValue("@Fees", Fees);

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




    }




}
