using MySql.Data.MySqlClient;
using System.Data;

namespace LabWork19
{
    public static class DAL
    {
        public static string Server = "localhost";
        public static string Port = "3306";
        public static string Database = "market";
        public static string UserID = "root";
        public static string Password = "password";

        public static string GetConnectionString()
        {
            var stringBuilder = new MySqlConnectionStringBuilder
            {
                Server = Server,
                Port = uint.Parse(Port),
                Database = Database,
                UserID = UserID,
                Password = Password
            };
            return stringBuilder.ConnectionString;
        }
    }
}
