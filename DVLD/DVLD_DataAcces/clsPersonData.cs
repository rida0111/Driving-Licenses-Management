using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Text;


namespace DataAccessLayer
{



    public class clsPersonData
    {


        public static bool FindByid(int PersonId, ref string FirstName, ref string LastName, ref string SecondName, ref string ThirdName,
            ref string NationalNumber, ref byte Gendor, ref DateTime DateOfBirth, ref string Address, ref string Phone, ref string Email,
            ref byte CountryID, ref string ImagePath)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "select * FROM People where PersonID=@Id";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Id", PersonId);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    FirstName = (string)reader["FirstName"];

                    LastName = (string)reader["LastName"];

                    SecondName = (string)reader["SecondName"];
         
                    Address = (string)reader["Address"];

                    Phone = (string)reader["Phone"];

                    NationalNumber = (string)reader["NationalNo"];

                    if (reader["ThirdName"] == DBNull.Value)
                        ThirdName = "";
                    else
                        ThirdName = (string)reader["ThirdName"];

                    if (reader["Email"] == DBNull.Value)
                        Email = "";
                    else
                        Email = (string)reader["Email"];

                    if (reader["ImagePath"] == DBNull.Value)
                        ImagePath = "";
                    else
                        ImagePath = (string)reader["ImagePath"];

                    CountryID = Convert.ToByte(reader["NationalityCountryID"]);

                    Gendor = Convert.ToByte(reader["Gendor"]);

                    DateOfBirth = (DateTime)reader["DateOfBirth"];

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

        public static bool FindByNationalNo(string NationalNumber,ref int PersonId, ref string FirstName, ref string LastName, ref string SecondName, ref string ThirdName,
         ref byte Gendor, ref DateTime DateOfBirth, ref string Address, ref string Phone, ref string Email,
        ref byte CountryID, ref string ImagePath)
        {

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = "select * FROM People where NationalNo=@NationalNumber";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@NationalNumber", NationalNumber);


            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;

                    PersonId = (int)reader["PersonID"];

                    FirstName = (string)reader["FirstName"];

                    LastName = (string)reader["LastName"];

                    SecondName = (string)reader["SecondName"];

                    Address = (string)reader["Address"];

                    Phone = (string)reader["Phone"];

                    NationalNumber = (string)reader["NationalNo"];

                    if (reader["ThirdName"] == DBNull.Value)
                        ThirdName = "";
                    else
                        ThirdName = (string)reader["ThirdName"];

                    if (reader["Email"] == DBNull.Value)
                        Email = "";
                    else
                        Email = (string)reader["Email"];

                    if (reader["ImagePath"] == DBNull.Value)
                        ImagePath = "";
                    else
                        ImagePath = (string)reader["ImagePath"];

                    CountryID = Convert.ToByte(reader["NationalityCountryID"]);

                    Gendor = Convert.ToByte(reader["Gendor"]);

                    DateOfBirth = (DateTime)reader["DateOfBirth"];
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

        public static int AddNewPerson(string FirstName, string LastName, string SecondName, string ThirdName,
             string NationalNo, byte Gendor, DateTime DateOfBirth, string Address, string Phone, string Email,
            int CountryID, string ImagePath)
        {

            int ID = -1;


            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"INSERT INTO People

                            (NationalNo,FirstName,SecondName,ThirdName,LastName

                            ,DateOfBirth,Gendor,Address,Phone,Email,NationalityCountryID,ImagePath)

                            VALUES

                            (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName

                            ,@DateOfBirth,@Gendor,@Address,@Phone,@Email,@CountryID,@ImagePath);select SCOPE_IDENTITY();";


            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);

            cmd.Parameters.AddWithValue("@SecondName", SecondName);
          
            cmd.Parameters.AddWithValue("@LastName", LastName);

            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            cmd.Parameters.AddWithValue("@Gendor", Gendor);

            cmd.Parameters.AddWithValue("@Address", Address);

            cmd.Parameters.AddWithValue("@Phone", Phone);

            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            if (String.IsNullOrEmpty(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);

            if (string.IsNullOrEmpty(ImagePath))
              cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);       
            else         
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);
            
            if(String.IsNullOrEmpty(Email))
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Email", Email);

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


        public static bool UpdatePerson(int Id, string FirstName, string LastName, string SecondName, string ThirdName,
             string NationalNo, byte Gendor, DateTime DateOfBirth, string Address, string Phone, string Email,
           int CountryID, string ImagePath)
        {

            int IsEffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string query = @"UPDATE People 

                SET NationalNo=@NationalNo,FirstName=@FirstName,SecondName=@SecondName,ThirdName=@ThirdName

                ,LastName=@LastName,DateOfBirth =@DateOfBirth,Gendor =@Gendor,Address =@Address

                ,Phone =@Phone,Email =@Email,NationalityCountryID =@CountryID,ImagePath =@ImagePath 

                where PersonID=@Id;";



            SqlCommand cmd = new SqlCommand(query, connection);


            cmd.Parameters.AddWithValue("@Id", Id);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

            cmd.Parameters.AddWithValue("@FirstName", FirstName);

            cmd.Parameters.AddWithValue("@SecondName", SecondName);
           
            cmd.Parameters.AddWithValue("@LastName", LastName);

            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);

            cmd.Parameters.AddWithValue("@Gendor", Gendor);

            cmd.Parameters.AddWithValue("@Address", Address);

            cmd.Parameters.AddWithValue("@Phone", Phone);
      
            cmd.Parameters.AddWithValue("@CountryID", CountryID);

            if (string.IsNullOrEmpty(ImagePath))
                cmd.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

            if (string.IsNullOrEmpty(ThirdName))
                cmd.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);

            if (string.IsNullOrEmpty(Email))
                cmd.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@Email", Email);

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


        public static bool DeletePerson(int PersonID)
        {
            int IsEffected = 0;

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"DELETE FROM People where PersonID=@PersonID;";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetAllPerson()
        {

            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string query = @"select PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName

                ,(select CAST(case when Gendor=1 then 'Female' else 'Male'end as nvarchar)) as Gendor

                ,DateOfBirth,Countries.CountryName as Nationality,Phone,Email  FROM People 
                INNER JOIN
                Countries 
                ON
                People.NationalityCountryID = Countries.CountryID
                order by FirstName";


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