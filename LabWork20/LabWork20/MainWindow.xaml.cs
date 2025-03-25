using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MySql.Data.MySqlClient;

namespace LabWork20
{
    public partial class MainWindow : Window
    {
        private DAL dal = new DAL(); 

        public MainWindow()
        {
            InitializeComponent();
        }
        //Обработчик кнопки запроса
        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            string query = txtQuery.Text.Trim();

            if (query == "Введите SQL-запрос" || string.IsNullOrWhiteSpace(query))
            {
                MessageBox.Show("Введите SQL-запрос.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (query.StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable resultTable = dal.ExecuteQuery(query);

                    if (resultTable.Rows.Count == 0)
                    {
                        lblResult.Content = "Результат: Нет данных";
                        TableOutput.ItemsSource = null; 
                    }
                    else
                    {
                        lblResult.Content = "Результат: Загружено " + resultTable.Rows.Count + " записей";
                        TableOutput.ItemsSource = resultTable.DefaultView; 
                    }
                }
                else
                {
                    object result = dal.ExecuteScalar(query);
                    lblResult.Content = "Результат: " + (result != null ? result.ToString() : "Нет данных");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка выполнения запроса: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Обработчик кнопки таблицы
        private void btnShowTable_Click(object sender, RoutedEventArgs e)
        {
            string tableName = txtShowTable.Text.Trim();

            if (tableName == "Введите название таблицы" || string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("Введите корректное название таблицы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (tableName.Contains(" ") || tableName.Contains(";") || tableName.ToUpper().Contains("SELECT"))
            {
                MessageBox.Show("Введите только название таблицы, без SQL-запросов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            tableName = tableName.Replace("market.", "").Trim();

            string query = $"SELECT * FROM `{tableName}`"; 

            try
            {
                DataTable resultTable = dal.ExecuteQuery(query);

                if (resultTable.Rows.Count == 0)
                {
                    lblResult.Content = "Результат: Таблица пуста или не найдена";
                    TableOutput.ItemsSource = null;
                }
                else
                {
                    lblResult.Content = $"Результат: Загружено {resultTable.Rows.Count} записей";
                    TableOutput.ItemsSource = resultTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки таблицы: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Тут просто оформлние
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Введите SQL-запрос" || textBox.Text == "Введите название таблицы"))
            {
                textBox.Text = "";
                textBox.Foreground = new SolidColorBrush(Colors.White); 
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "txtQuery")
                    textBox.Text = "Введите SQL-запрос";
                else if (textBox.Name == "txtShowTable")
                    textBox.Text = "Введите название таблицы";

                textBox.Foreground = new SolidColorBrush(Colors.Gray); 
            }
        }
    }
}
