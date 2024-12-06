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
    /// Логика взаимодействия для ProfileEditingWindow.xaml
    /// </summary>
    public partial class ProfileEditingWindow : Window
    {
        public ProfileEditingWindow()
        {
            InitializeComponent();
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e) 
        {
            this.Close();
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {

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
