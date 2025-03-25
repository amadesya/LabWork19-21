using System.Windows;
using System.Windows.Controls;

namespace LabWork19
{
    public partial class MainWindow : Window
    {
        private DAL dal = new DAL(); 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblMessage.Text = "Введите логин и пароль!";
                lblMessage.Visibility = Visibility.Visible;
                return;
            }

            if (dal.CheckDatabaseConnection(username, password)) 
            {
                MessageBox.Show($"Здравствуйте, {username}!", "Успешный вход", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Title = "Главное окно";
                lblMessage.Visibility = Visibility.Hidden;
                txtUsername.Visibility = Visibility.Hidden;
                txtPassword.Visibility = Visibility.Hidden;
                btnLogin.Visibility = Visibility.Hidden;

                TextBlock welcomeText = new TextBlock
                {
                    Text = "Добро пожаловать!",
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                this.Content = welcomeText;
            }
            else
            {
                lblMessage.Text = "Ошибка входа! Неверный логин или пароль.";
                lblMessage.Visibility = Visibility.Visible;
            }
        }
    }
}
