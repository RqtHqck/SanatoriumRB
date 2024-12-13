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
using Sanatorium.Views;
using Sanatorium;
using Sanatorium.Services;
using Sanatorium.Utils;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfileControl.xaml
    /// </summary>
    public partial class ProfileControl : UserControl
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        private ProfileWindow _parentWindow;
        public ProfileControl(ProfileWindow parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
        }

        public void ProfileEdit_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new ProfileEditeControl(_parentWindow));
        }

        public void MyBooking_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new ProfileMyBookingControl(_parentWindow));
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.Close();
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
