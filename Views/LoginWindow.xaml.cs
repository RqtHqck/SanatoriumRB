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
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            Console.WriteLine(email, password);

            // Логика аутентификации пользователя
            MessageBox.Show($"Пользователь: {email}, Пароль: {password}");
        }

        // Обработчик клика для кнопки "Register"
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Скрытие текущего окна (LoginWindow)
            var registerWindow = new RegisterWindow(); // Открытие нового окна (RegisterWindow)
            registerWindow.Show();
        }

    }
}
