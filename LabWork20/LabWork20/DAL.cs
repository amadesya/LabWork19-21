using System;
using System.Data;
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

        // Метод получения DataTable
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
    }
}
