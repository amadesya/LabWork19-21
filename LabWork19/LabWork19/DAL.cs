using MySql.Data.MySqlClient;

namespace LabWork19
{
    public class DAL
    {
        private string Server = "localhost";
        private string Database = "market";
        private string Port = "3306";

        public bool CheckDatabaseConnection(string username, string password)
        {
            try
            {
                string connectionString = $"Server={Server};Database={Database};User ID={username};Password={password};Port={Port};";
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    return true; 
                }
            }
            catch
            {
                return false; 
            }
        }
    }
}
