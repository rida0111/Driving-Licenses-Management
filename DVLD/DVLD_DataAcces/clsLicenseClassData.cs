using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;


namespace Data_Base_of_DVLM
{

    public class clsLicenseClassData
    {

        public static bool FindByName(ref byte LicenseClassID, string className, ref string ClassDescription
            , ref int MinimumAge, ref int DefaultValidityLength, ref int ClassFess)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from LicenseClasses where ClassName=@className;";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@className", className);

            try
            {
                connection.Open();
            
                SqlDataReader Reader = cmd.ExecuteReader();

                if (Reader.Read())
                {
                    isFound = true;

                    MinimumAge = Convert.ToInt32(Reader["MinimumAllowedAge"]);

                    DefaultValidityLength = Convert.ToInt16(Reader["DefaultValidityLength"]);

                    ClassFess = Convert.ToInt32(Reader["ClassFees"]);

                    LicenseClassID = Convert.ToByte(Reader["LicenseClassID"]);

                    ClassDescription = (string)Reader["ClassDescription"];
                }
                else
                    isFound = false;


                Reader.Close();
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


        public static bool FindByID(byte LicenseClassID, ref string className, ref string ClassDescription
           , ref int MinimumAge, ref int DefaultValidityLength, ref int ClassFess)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from LicenseClasses where LicenseClassID=@LicenseClassID;";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {

                connection.Open();

                SqlDataReader Reader = cmd.ExecuteReader();


                if (Reader.Read())
                {
                    IsFound = true;

                    className = (string)Reader["className"];

                    ClassDescription = (string)Reader["ClassDescription"];

                    MinimumAge = Convert.ToInt32(Reader["MinimumAllowedAge"]);

                    DefaultValidityLength = Convert.ToInt32(Reader["DefaultValidityLength"]);

                    ClassFess = Convert.ToInt32(Reader["ClassFees"]);
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


        public static DataTable GetAllLicenseClasses()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select ClassName from LicenseClasses;";

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



    }


}
