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
            string username = UsernameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();


            // Логика аутентификации пользователя
            MessageBox.Show($"Пользователь: Имя: {username}, Почта: {email}, Пароль: {password}");
        }


        private void UsernameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernameHintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void UsernameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameHintTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmailHintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                EmailHintTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordHintTextBlock.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordHintTextBlock.Visibility = Visibility.Visible;
            }
        }

    }
}
