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
using Sanatorium.Services;


namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        // Обработчик клика для кнопки "Login"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                bool isAuthenticatedUser = Auth.Login(EmailTextBox.Text.Trim(), PasswordBox.Password.Trim());
                if (isAuthenticatedUser)
                {
                    this.Hide();
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }

        }

        // Обработчик клика для кнопки "Register"
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Скрытие текущего окна (LoginWindow)
            var registerWindow = new RegisterWindow(); // Открытие нового окна (RegisterWindow)
            registerWindow.Show();
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
