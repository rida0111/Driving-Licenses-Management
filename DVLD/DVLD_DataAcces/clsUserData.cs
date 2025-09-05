using DataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;



namespace Data_Base_of_DVLM
{

    public class clsUserData
    {

        public static bool FindById(int UserID, ref int PersonID, ref string UserName
            , ref string Password, ref bool IsActive)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from Users  where UserID=@UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {

                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    PersonID = (int)Reader["PersonID"];

                    UserName = (string)Reader["UserName"];

                    Password = (string)Reader["Password"];

                    IsActive =(bool)Reader["IsActive"];
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

        public static bool FindByName(string UserName,ref int UserID, ref int PersonID
         , ref string Password, ref bool IsActive)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from Users where UserName=@UserName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserName", UserName);

            try
            {

                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;

                    UserID = (int)Reader["UserID"];

                    PersonID = (int)Reader["PersonID"];

                    UserName = (string)Reader["UserName"];

                    Password = (string)Reader["Password"];

                    IsActive = (bool)Reader["IsActive"];
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

        public static bool FindUserbyNameAndPassword(ref int UserID,string Username, string Password,ref int PersonID,ref bool IsActive)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"select * from Users where UserName=@Username and Password=@Password";


            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Username", Username);

            Command.Parameters.AddWithValue("@Password", Password);

            try
            {

                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader.Read())
                {

                    IsFound = true;

                    UserID = (int)reader["UserID"];

                    PersonID = (int)reader["PersonID"];

                    IsActive= (bool)reader["IsActive"];
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
 

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"SELECT  Users.UserID, Users.PersonID,
                    (People.FirstName+' '+ People.SecondName+' '+isnull( People.ThirdName,'')+' '+ People.LastName) as FullName
                    ,Users.UserName, Users.IsActive
                       FROM    People INNER JOIN  Users ON People.PersonID = Users.PersonID

                              order by FullName;";


            SqlCommand Command = new SqlCommand(Query, Connection);


            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

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


        public static bool IsPersonUser(int PersonId)
        {

            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

           
            string Query = @"SELECT found=1 FROM  Users  where Users.PersonID=@Id";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@Id", PersonId);

            try
            {
                Connection.Open();

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
                Connection.Close();
            }

            return IsFound;

        }

        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {

            int Id = -1;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = @"INSERT INTO Users 

                (PersonID,UserName,Password,IsActive)

                VALUES 
                (@PersonID,@UserName,@Password,@IsActive);select SCOPE_IDENTITY();";


            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            Command.Parameters.AddWithValue("@UserName", UserName);

            Command.Parameters.AddWithValue("@Password", Password);

            Command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {

                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int UserId))
                    Id = UserId;
                else
                    Id = -1;

            }
            catch
            {
                Id = -1;
            }
            finally
            {
                Connection.Close();
            }


            return Id;
        }

        public static bool UpdateUserInfo(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {

            int IsEffected = 0;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);


            string Query = @"UPDATE Users

                       SET PersonID =@PersonID,UserName =@UserName,Password =@Password,IsActive =@IsActive

                       WHERE UserID=@UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", UserID);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            Command.Parameters.AddWithValue("@UserName", UserName);

            Command.Parameters.AddWithValue("@Password", Password);

            Command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                Connection.Open();

                IsEffected = Command.ExecuteNonQuery();
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
 
        public static bool DeleteUser(int UserID)
        {

            int IsEffected = 0;

            SqlConnection Connection = new SqlConnection(ConnectionToDataBase.ConnectionString);

            string Query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@UserID", UserID);


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
