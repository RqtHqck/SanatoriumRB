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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sanatorium.Services;
using Sanatorium.Utils;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfileEditeControl.xaml
    /// </summary>
    public partial class ProfileEditeControl : UserControl
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        private ProfileWindow _parentWindow;

        public ProfileEditeControl(ProfileWindow parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
        }
        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new ProfileControl(_parentWindow));
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool resultEditing = UserService.EditProfile(UsernameTextBox.Text.Trim(), EmailTextBox.Text.Trim(), PasswordBox.Password.Trim());
            if (resultEditing)
            {
                _parentWindow.Close();
            }
        }

        public void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Window currentWindow = Application.Current.MainWindow; // Сохраняем текущее окно, которое не нужно закрывать

            var loginWindow = new LoginWindow();
            // Закрываем все окна, кроме текущего
            foreach (Window window in Application.Current.Windows)
            {
                if (window != loginWindow)
                {
                    window.Close();
                }
            }

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
