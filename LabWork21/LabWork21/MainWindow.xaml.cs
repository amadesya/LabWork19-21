using System.Windows;
using System.Windows.Controls;
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

        // Обработка кнопки запроса
        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            string query = txtQuery.Text.Trim();

            if (string.IsNullOrEmpty(query) || query == "Введите SQL-запрос")
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

        // Обработка кнопки удаления
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int bookId;
            if (int.TryParse(txtBookId.Text, out bookId))
            {
                int affectedRows = _dal.ExecuteDeleteQuery(bookId);
                lblResult.Content = $"{affectedRows} строк(и) удалено.";
            }
            else
            {
                MessageBox.Show("Введите правильный ID книги!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Обработка кнопки добавления
        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            int authorId;
            if (int.TryParse(txtAuthorId.Text, out authorId) && decimal.TryParse(txtPrice.Text, out decimal price) &&
                double.TryParse(txtWeight.Text, out double weight) && int.TryParse(txtYear.Text, out int year) &&
                int.TryParse(txtPages.Text, out int pages))
            {
                int affectedRows = _dal.ExecuteInsertQuery(title, authorId, txtGenre.Text, price, weight, year, pages);
                lblResult.Content = $"{affectedRows} строк(и) добавлено.";
            }
            else
            {
                MessageBox.Show("Введите корректные данные для книги!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Стили для вайба
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Введите SQL-запрос" || textBox.Text == "Введите ID книги" ||
                                    textBox.Text == "Введите название книги" || textBox.Text == "Введите ID автора" ||
                                    textBox.Text == "Введите жанр" || textBox.Text == "Введите цену" ||
                                    textBox.Text == "Введите вес" || textBox.Text == "Введите год выпуска" ||
                                    textBox.Text == "Введите количество страниц"))
            {
                textBox.Text = "";
                textBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "txtQuery")
                    textBox.Text = "Введите SQL-запрос";
                else if (textBox.Name == "txtBookId")
                    textBox.Text = "Введите ID книги";
                else if (textBox.Name == "txtTitle")
                    textBox.Text = "Введите название книги";
                else if (textBox.Name == "txtAuthorId")
                    textBox.Text = "Введите ID автора";
                else if (textBox.Name == "txtGenre")
                    textBox.Text = "Введите жанр";
                else if (textBox.Name == "txtPrice")
                    textBox.Text = "Введите цену";
                else if (textBox.Name == "txtWeight")
                    textBox.Text = "Введите вес";
                else if (textBox.Name == "txtYear")
                    textBox.Text = "Введите год выпуска";
                else if (textBox.Name == "txtPages")
                    textBox.Text = "Введите количество страниц";

                textBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Gray);
            }
        }
    }
}
