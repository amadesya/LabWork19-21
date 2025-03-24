using System.Windows;
using MySql.Data.MySqlClient;

namespace LabWork20
{
    public partial class MainWindow : Window
    {
        private DatabaseAccessLayer _dal;

        public MainWindow()
        {
            InitializeComponent();

            string connectionString = "Server=localhost;Database=market;User ID=root;Password=root;Port=3306;";
            _dal = new DatabaseAccessLayer(connectionString);
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            string query = txtQuery.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Введите SQL-запрос!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                int affectedRows = _dal.ExecuteNonQuery(query);

                lblResult.Content = $"Изменено строк: {affectedRows}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
