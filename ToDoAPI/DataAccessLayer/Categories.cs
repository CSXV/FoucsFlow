using Npgsql;

namespace DataAccessLayer
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string iconName { get; set; }

        public CategoryDTO(int id, string name, string iconname)
        {
            this.ID = id;
            this.name = name;
            this.iconName = iconname;
        }
    }

    public class CategoriesData
    {
        static string _connectionString = "Host=localhost;Username=user;Database=ToDoApp";

        public static List<CategoryDTO> GetAllCategories()
        {
            var categoriesList = new List<CategoryDTO>();

            using (var dataSource = NpgsqlDataSource.Create(_connectionString))
            {
                using (var cmd = dataSource.CreateCommand("SELECT * FROM categories;"))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoriesList.Add(
                                new CategoryDTO(
                                    reader.GetInt32(reader.GetOrdinal("id")),
                                    reader.GetString(reader.GetOrdinal("name")),
                                    reader.GetString(reader.GetOrdinal("iconName"))
                                )
                            );
                        }
                    }
                }
            }

            return categoriesList;
        }

        public static CategoryDTO GetCategoryByID(int id)
        {
            using (var conn = NpgsqlDataSource.Create(_connectionString))
            {
                using (var command = conn.CreateCommand("SELECT * FROM categories WHERE id=@id;"))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CategoryDTO(
                                reader.GetInt32(reader.GetOrdinal("id")),
                                reader.GetString(reader.GetOrdinal("name")),
                                reader.GetString(reader.GetOrdinal("iconName"))
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
