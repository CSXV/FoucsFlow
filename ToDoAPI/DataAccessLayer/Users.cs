using Npgsql;

namespace DataAccessLayer
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }

        public LoginRequest(string username, string password)
        {
            this.userName = username;
            this.password = password;
        }
    }

    public class UserDTO
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public bool isActive { get; set; }
        public string? profileImage { get; set; }
        public int userType { get; set; }

        public UserDTO(
            int id,
            string username,
            string email,
            string password,
            string firstname,
            string lastname,
            DateTime createdate,
            DateTime updatedate,
            bool isactive,
            string profileImage,
            int userType
        )
        {
            this.ID = id;
            this.userName = username;
            this.email = email;
            this.password = password;
            this.firstName = firstname;
            this.lastName = lastname;
            this.createDate = createdate;
            this.updateDate = updatedate;
            this.isActive = isactive;
            this.profileImage = profileImage;
            this.userType = userType;
        }
    }

    public class UsersData
    {
        static string _connectionString = "Host=localhost;Username=user;Database=ToDoApp";

        public static UserDTO GetUserByID(int id)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (var command = conn.CreateCommand("SELECT * FROM users WHERE id = @id;"))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("username")),
                                reader.GetString(reader.GetOrdinal("email")),
                                reader.GetString(reader.GetOrdinal("passwordhash")),
                                reader.GetString(reader.GetOrdinal("firstname")),
                                reader.GetString(reader.GetOrdinal("lastname")),
                                reader.GetDateTime(reader.GetOrdinal("createdate")),
                                reader.GetDateTime(reader.GetOrdinal("updatedate")),
                                reader.GetBoolean(reader.GetOrdinal("isactive")),
                                reader.IsDBNull(reader.GetOrdinal("profileimage"))
                                    ? (string?)null
                                    : reader.GetString(reader.GetOrdinal("profileimage")),
                                reader.GetInt32(reader.GetOrdinal("usertype"))
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        public static int AddNewUser(UserDTO user)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (
                    var command = conn.CreateCommand(
                        @"INSERT INTO users (
                          username, email, passwordhash, firstname, lastname, createdate,
                          updatedate, isactive, profileimage, usertype
                          ) VALUES (
                            @username, @email, @passwordhash, @firstname, @lastname,
                            @createdate, @updatedate, @isactive, @profileimage, @usertype
                          ) RETURNING ID;"
                    )
                )
                {
                    command.Parameters.AddWithValue("@username", user.userName);
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@passwordhash", user.password);
                    command.Parameters.AddWithValue("@firstname", user.firstName);
                    command.Parameters.AddWithValue("@lastname", user.lastName);
                    command.Parameters.AddWithValue("@createdate", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@updatedate", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@isactive", user.isActive);

                    command.Parameters.AddWithValue(
                        "@profileimage",
                        user.profileImage ?? (object)DBNull.Value
                    );

                    command.Parameters.AddWithValue("@usertype", user.userType);

                    var rowInserted = command.ExecuteScalar();

                    return (int)rowInserted;
                }
            }
        }

        public static bool UpdateUserByID(UserDTO user)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (
                    var command = conn.CreateCommand(
                        "UPDATE users SET username = @username, email = @email, passwordhash = @passwordhash, firstname = @firstname, lastname = @lastname, updatedate = @updatedate, isactive = @isactive, profileimage = @profileimage, usertype = @usertype WHERE id = @id;"
                    //
                    // @"UPDATE users SET
                    // username=@username,
                    // email=@email,
                    // passwordhash=@passwordhash,
                    // firstname=@firstname,
                    // lastname=@lastname,
                    // updatedate=CURRENT_TIMESTAMP,
                    // isactive=@isactive,
                    // profileimage=@profileimage,
                    // usertype=@usertype
                    // WHERE id=@id"
                    )
                )
                {
                    command.Parameters.AddWithValue("@id", user.ID);
                    command.Parameters.AddWithValue("@username", user.userName);
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@passwordhash", user.password);
                    command.Parameters.AddWithValue("@firstname", user.firstName);
                    command.Parameters.AddWithValue("@lastname", user.lastName);
                    command.Parameters.AddWithValue("@updatedate", DateTime.Now);
                    command.Parameters.AddWithValue("@isactive", user.isActive);

                    command.Parameters.AddWithValue(
                        "@profileimage",
                        user.profileImage ?? (object)DBNull.Value
                    );

                    command.Parameters.AddWithValue("@usertype", user.userType);

                    var rowAffected = command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public static bool DeleteUserByID(int id)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (var command = conn.CreateCommand("DELETE FROM users WHERE ID=@id;"))
                {
                    command.Parameters.AddWithValue("@id", id);

                    var rowAffected = command.ExecuteNonQuery();
                    return rowAffected == 1;
                }
            }
        }

        public static UserDTO loginUser(string userName, string password)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (
                    var command = conn.CreateCommand(
                        "SELECT * FROM users WHERE username=@username AND passwordhash=@passwordhash;"
                    )
                )
                {
                    command.Parameters.AddWithValue("@username", userName);
                    command.Parameters.AddWithValue("@passwordhash", password);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new UserDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("username")),
                                reader.GetString(reader.GetOrdinal("email")),
                                reader.GetString(reader.GetOrdinal("passwordhash")),
                                reader.GetString(reader.GetOrdinal("firstname")),
                                reader.GetString(reader.GetOrdinal("lastname")),
                                reader.GetDateTime(reader.GetOrdinal("createdate")),
                                reader.GetDateTime(reader.GetOrdinal("updatedate")),
                                reader.GetBoolean(reader.GetOrdinal("isactive")),
                                reader.IsDBNull(reader.GetOrdinal("profileimage"))
                                    ? (string?)null
                                    : reader.GetString(reader.GetOrdinal("profileimage")),
                                reader.GetInt32(reader.GetOrdinal("usertype"))
                            );
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        //
    }
}
