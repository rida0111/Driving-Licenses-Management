using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{



    public class clsTestTypeData
    {

        public static bool FindByID(int TestID, ref string Title, ref string Description, ref Single Fees)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = "select * from TestTypes where TestTypeID=@TestID";

            SqlCommand cmd = new SqlCommand(Query, Connection);


            cmd.Parameters.AddWithValue("@TestID", TestID);


            try
            {
                Connection.Open();

                SqlDataReader Reader = cmd.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    Title = (string)Reader["TestTypeTitle"];

                    Description = (string)Reader["TestTypeDescription"];

                    Fees = Convert.ToSingle(Reader["TestTypeFees"]);
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
                Connection.Close();
            }


            return IsFound;
        }


        public static DataTable GetAllTestTypes()
        {

            DataTable dt = new DataTable();


            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"select  TestTypeID ,TestTypeTitle,TestTypeDescription ,TestTypeFees

                            from TestTypes";


            SqlCommand cmd = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();

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
                Connection.Close();
            }


            return dt;
        }

        public static bool EditTestTypesInfo(int TestID, string Title, string Description, Single Fees)
        {

            int IsEffected = 0;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"UPDATE TestTypes SET TestTypeTitle =@Title,TestTypeDescription =@Description

                                ,TestTypeFees =@Fees  WHERE TestTypeID=@TestID";


            SqlCommand cmd = new SqlCommand(Query, Connection);


            cmd.Parameters.AddWithValue("@TestID", TestID);

            cmd.Parameters.AddWithValue("@Title", Title);

            cmd.Parameters.AddWithValue("@Description", Description);

            cmd.Parameters.AddWithValue("@Fees", Fees);


            try
            {
                Connection.Open();

                IsEffected = cmd.ExecuteNonQuery();
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
