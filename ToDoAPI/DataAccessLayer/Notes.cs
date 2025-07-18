using Npgsql;

namespace DataAccessLayer
{
    public class NoteDTO
    {
        public NoteDTO() { }

        public NoteDTO(
            int id,
            int userID,
            int categoryID,
            string title,
            string content,
            DateTime createDate,
            DateTime updateDate,
            string state,
            bool isPinned
        )
        {
            this.ID = id;
            this.userID = userID;
            this.categoryID = categoryID;

            this.title = title;
            this.content = content;

            this.createDate = createDate;
            this.updateDate = updateDate;

            this.state = state;
            this.isPinned = isPinned;
        }

        public int ID { get; set; }
        public int userID { get; set; }
        public int categoryID { get; set; }

        public string title { get; set; }
        public string content { get; set; }

        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }

        public string state { get; set; }
        public bool isPinned { get; set; }
    }

    public class NotesData
    {
        static string _connectionString = "Host=localhost;Username=user;Database=ToDoApp";

        public static List<NoteDTO> GetAllNotes()
        {
            var notesList = new List<NoteDTO>();

            using (var dataSource = NpgsqlDataSource.Create(_connectionString))
            {
                using (var cmd = dataSource.CreateCommand("SELECT * FROM notes;"))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notesList.Add(
                                new NoteDTO(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetInt32(reader.GetOrdinal("userid")),
                                    reader.GetInt32(reader.GetOrdinal("categoryid")),
                                    //
                                    reader.GetString(reader.GetOrdinal("title")),
                                    reader.GetString(reader.GetOrdinal("content")),
                                    //
                                    reader.GetDateTime(reader.GetOrdinal("createdate")),
                                    reader.GetDateTime(reader.GetOrdinal("updatedate")),
                                    //
                                    reader.GetString(reader.GetOrdinal("state")),
                                    reader.GetBoolean(reader.GetOrdinal("ispinned"))
                                )
                            );
                        }
                    }
                }
            }

            return notesList;
        }

        public static List<NoteDTO> GetAllUserNotesForID(int id)
        {
            var notesList = new List<NoteDTO>();

            using (var dataSource = NpgsqlDataSource.Create(_connectionString))
            {
                using (
                    var cmd = dataSource.CreateCommand(
                        "SELECT * FROM notes WHERE userid=@id AND state='to do';"
                    )
                )
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notesList.Add(
                                new NoteDTO(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetInt32(reader.GetOrdinal("userid")),
                                    reader.GetInt32(reader.GetOrdinal("categoryid")),
                                    //
                                    reader.GetString(reader.GetOrdinal("title")),
                                    reader.GetString(reader.GetOrdinal("content")),
                                    //
                                    reader.GetDateTime(reader.GetOrdinal("createdate")),
                                    reader.GetDateTime(reader.GetOrdinal("updatedate")),
                                    //
                                    reader.GetString(reader.GetOrdinal("state")),
                                    reader.GetBoolean(reader.GetOrdinal("ispinned"))
                                )
                            );
                        }
                    }
                }
            }

            return notesList;
        }

        public static NoteDTO GetNoteByID(int id)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (var command = conn.CreateCommand("SELECT * FROM notes WHERE id=@id;"))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NoteDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetInt32(reader.GetOrdinal("userid")),
                                reader.GetInt32(reader.GetOrdinal("categoryid")),
                                //
                                reader.GetString(reader.GetOrdinal("title")),
                                reader.GetString(reader.GetOrdinal("content")),
                                //
                                reader.GetDateTime(reader.GetOrdinal("createdate")),
                                reader.GetDateTime(reader.GetOrdinal("updatedate")),
                                //
                                reader.GetString(reader.GetOrdinal("state")),
                                reader.GetBoolean(reader.GetOrdinal("ispinned"))
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

        public static int AddNewNote(NoteDTO note)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (
                    var command = conn.CreateCommand(
                        "INSERT INTO notes(title, content, userid, categoryid, createdate, updatedate, state, ispinned) VALUES (@title, @content, @userid, @categoryid, @createdate, @updatedate, @state, @ispinned) RETURNING ID;"
                    )
                )
                {
                    command.Parameters.AddWithValue("@userid", note.userID);
                    command.Parameters.AddWithValue("@categoryid", note.categoryID);

                    command.Parameters.AddWithValue("@title", note.title);
                    command.Parameters.AddWithValue("@content", note.content);

                    command.Parameters.AddWithValue("@createdate", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedate", DateTime.Now);

                    command.Parameters.AddWithValue("@state", note.state);
                    command.Parameters.AddWithValue("@ispinned", note.isPinned);

                    var rowInserted = command.ExecuteScalar();

                    return (int)rowInserted;
                }
            }
        }

        public static bool UpdateNoteByID(NoteDTO note)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (
                    var command = conn.CreateCommand(
                        "UPDATE notes SET title=@title, content=@content, categoryid=@categoryid, updatedate=@updatedate, state=@state, ispinned=@ispinned WHERE ID=@id;"
                    )
                )
                {
                    command.Parameters.AddWithValue("@id", note.ID);
                    command.Parameters.AddWithValue("@categoryid", note.categoryID);

                    command.Parameters.AddWithValue("@title", note.title);
                    command.Parameters.AddWithValue("@content", note.content);

                    command.Parameters.AddWithValue("@updatedate", DateTime.Now);

                    command.Parameters.AddWithValue("@state", note.state);
                    command.Parameters.AddWithValue("@ispinned", note.isPinned);

                    var rowAffected = command.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public static bool DeleteNoteByID(int id)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (var command = conn.CreateCommand("DELETE FROM notes WHERE ID=@id;"))
                {
                    command.Parameters.AddWithValue("@id", id);

                    var rowAffected = command.ExecuteNonQuery();
                    return rowAffected == 1;
                }
            }
        }

        //
    }
}
