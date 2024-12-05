using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        // Обработчик клика для кнопки "Login"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Скрытие текущего окна (RegisterWindow)
            var loginWindow = new LoginWindow(); // Открытие нового окна (LoginWindow)
            loginWindow.Show();

        }

        // Обработчик клика для кнопки "Register"
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для регистрации
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            // Логика аутентификации пользователя
            MessageBox.Show($"Пользователь: Имя: {username}, Почта: {email}, Пароль: {password}");
        }
    }
}
