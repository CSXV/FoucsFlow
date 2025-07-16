using DataAccessLayer;

namespace BusinessLayer
{
    public class Users
    {
        public enum enMode
        {
            addNew = 0,
            Update = 1,
        };

        public enMode Mode = enMode.addNew;

        public UserDTO UDTO
        {
            get
            {
                return (
                    new UserDTO(
                        this.ID,
                        this.userName,
                        this.email,
                        this.password,
                        this.firstName,
                        this.lastName,
                        this.createDate,
                        this.updateDate,
                        this.isActive,
                        this.profileImage,
                        this.userType
                    )
                );
            }
        }

        public int ID { get; set; }

        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public string firstName { get; set; }
        public string lastName { get; set; }

        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }

        public bool isActive { get; set; }
        public string profileImage { get; set; }
        public int userType { get; set; }

        public Users(UserDTO UDTO, enMode cMode = enMode.addNew)
        {
            this.ID = UDTO.ID;
            this.userName = UDTO.userName;
            this.email = UDTO.email;
            this.password = UDTO.password;
            this.firstName = UDTO.firstName;
            this.lastName = UDTO.lastName;
            this.createDate = UDTO.createDate;
            this.updateDate = UDTO.updateDate;
            this.isActive = UDTO.isActive;
            this.profileImage = UDTO.profileImage;
            this.userType = UDTO.userType;

            this.Mode = cMode;
        }

        public static Users login(string userName, string password)
        {
            UserDTO UDTO = UsersData.loginUser(userName, password);

            if (UDTO != null)
            {
                return new Users(UDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        public static Users Find(int id)
        {
            UserDTO UDTO = UsersData.GetUserByID(id);

            if (UDTO != null)
            {
                return new Users(UDTO, enMode.Update);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewUser()
        {
            this.ID = UsersData.AddNewUser(UDTO);
            return (this.ID != -1);
        }

        private bool _UpdateUserByID()
        {
            return UsersData.UpdateUserByID(UDTO);
        }

        public static bool DeleteUser(int id)
        {
            return UsersData.DeleteUserByID(id);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.addNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUserByID();
            }

            return false;
        }

        //
    }
}
