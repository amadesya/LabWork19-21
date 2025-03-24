using System;
using System.Data;
using System.Windows;
using MySqlX.XDevAPI.Relational;

namespace LabWork20
{
    public partial class MainWindow : Window
    {
        private DatabaseAccessLayer _dal;

        public MainWindow()
        {
            InitializeComponent();
            _dal = new DatabaseAccessLayer("Server=localhost;Database=market;User ID=root;Password=root;Port=3306;");
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
                object result = _dal.ExecuteScalarQuery(query);

                if (result != null)
                {
                    lblResult.Content = $"Результат: {result}";
                }
                else
                {
                    lblResult.Content = "Результат пуст.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnShowTable_Click(object sender, RoutedEventArgs e)
        {
            string query = txtShowTable.Text.Trim();

            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Введите SQL-запрос!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DataTable table = _dal.ExecuteQuery(query);

                if (table.Rows.Count > 0)
                {
                    TableOutput.ItemsSource = table.DefaultView; // Правильный вывод в DataGrid
                }
                else
                {
                    MessageBox.Show("Результат пуст.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка выполнения запроса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
