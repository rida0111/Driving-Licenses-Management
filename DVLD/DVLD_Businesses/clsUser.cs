using Data_Base_of_DVLM;
using System.Data;



namespace Businesses_Access_Layer
{



    public class clsUser
    {

        public int UserID { get; set; }
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        private enum Mode { AddNew = 0, Update = 1 }

        private Mode enMode;

        public clsUser()
        {
            UserID = -1;
            PersonId = -1;
            UserName = "";
            Password = "";
            IsActive = false;

            enMode = Mode.AddNew;
        }

        public clsUser(int userId, int personId, string userName, string password, bool isActive)
        {
            UserID = userId;
            PersonId = personId;
            UserName = userName;
            Password = password;
            IsActive = isActive;

            enMode = Mode.Update;
        }

        public static clsUser FindByID(int UserID)
        {

            int PersonID = 0; string UserName = "", Password = "";

            bool IsActive = false;

            bool IsFound = clsUserData.FindById(UserID, ref PersonID, ref UserName, ref Password, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUser FindByName(string UserName)
        {

            int UserID = 0, PersonID = 0;

            string Password = "";
            bool IsActive = false;


            bool IsFound = clsUserData.FindByName(UserName, ref UserID, ref PersonID, ref Password, ref IsActive);

            if (IsFound)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;
        }

        public static clsUser FindUserByUserIDandPassword(string UserName, string Password)
        {

            int PersonID = 0, UserID = 0;

            bool IsActive = false;

            bool Found = clsUserData.FindUserbyNameAndPassword(ref UserID, UserName, Password,ref PersonID,ref IsActive);

            if (Found)
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);      
            else
                return null;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static bool IsPersonUser(int PersonID)
        {
            return clsUserData.IsPersonUser(PersonID);
        }

        private bool UpdateUserInfo()
        {
            return clsUserData.UpdateUserInfo(this.UserID, this.PersonId, this.UserName
                               , this.Password, this.IsActive);
        }

        private bool AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonId, this.UserName, this.Password
                , this.IsActive);

            return (this.UserID != -1);
        }
        public bool Save()
        {

            switch (enMode)
            {
                case Mode.Update:
                    return UpdateUserInfo();
                case Mode.AddNew:
                    {
                        if (AddNewUser())
                        {
                            enMode = Mode.Update;
                            return true;
                        }
                        return false;

                    }
                            
                default:
                    return false;              
            }   

        }
        public static bool DeleteUserInfo(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }



    }



}
