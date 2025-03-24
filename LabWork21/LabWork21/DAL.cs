using System.Data;
using System.Windows.Media.Media3D;
using MySql.Data.MySqlClient;

namespace LabWork20
{
    public class DatabaseAccessLayer
    {
        private string _connectionString;

        public DatabaseAccessLayer(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Метод выполнения запроса DataTable
        public DataTable ExecuteQuery(string query)
        {
            DataTable table = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        table.Load(reader);
                    }
                }
            }

            return table;
        }

        // Метод возврата одного значения
        public object ExecuteScalarQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return command.ExecuteScalar();
                }
            }
        }

        // Возврат количества измененных строк
        public int ExecuteNonQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        // Форма удаления записи
        public int ExecuteDeleteQuery(int bookId)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM book WHERE id = @bookId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@bookId", bookId);
                    return command.ExecuteNonQuery();
                }
            }
        }
        // Форма добавления новой записи
        public int ExecuteInsertQuery(string title, int authorId, string genre, decimal price, double weight, int year, int pages)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO book (title, author_id, genre, price, weight, year_publication, pages) " +
                                          "VALUES (@title, @authorId, @genre, @price, @weight, @year, @pages)";

                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@authorId", authorId);
                    command.Parameters.AddWithValue("@genre", genre);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@weight", weight);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@pages", pages);

                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
