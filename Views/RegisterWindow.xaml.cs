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
using Sanatorium.Utils;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        public RegisterWindow()
        {
            InitializeComponent();
        }

        // Обработчик клика для кнопки "Register"
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isAuthenticatedUser = UserService.Register(UsernameTextBox.Text.Trim(), EmailTextBox.Text.Trim(), PasswordBox.Password.Trim());
                if (isAuthenticatedUser)
                {
                    LoginButton_Click(sender, e);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // Обработчик клика для кнопки "Login"
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide(); // Скрытие текущего окна (RegisterWindow)
            var loginWindow = new LoginWindow(); // Открытие нового окна (LoginWindow)
            loginWindow.Show();
        }

        // Обработчик фокуса для TextBox
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _hintHelper.TextBox_GotFocus(sender, e);
        }

        // Обработчик потери фокуса для TextBox
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _hintHelper.TextBox_LostFocus(sender, e);
        }

        // Обработчик фокуса для PasswordBox
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _hintHelper.PasswordBox_GotFocus(sender, e);
        }

        // Обработчик потери фокуса для PasswordBox
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _hintHelper.PasswordBox_LostFocus(sender, e);
        }
    }
}
