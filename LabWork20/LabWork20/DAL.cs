using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

public class DAL
{
    private string connectionString = "server=localhost;database=market;user=root;password=root;";

    // Метод для выборки множества значений
    public DataTable ExecuteQuery(string sqlQuery)
    {
        DataTable table = new DataTable();
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(table);
                MessageBox.Show($"Загружено {table.Rows.Count} записей", "Отладка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка SQL", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        return table;
    }


    // Метод для выборки одного значения
    public object ExecuteScalar(string sqlQuery)
    {
        object result = null;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sqlQuery, connection);
                result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
        return result;
    }
}
