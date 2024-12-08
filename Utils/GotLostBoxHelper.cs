using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Sanatorium.Utils
{
    public  class GotLostBoxHelper
    {
        public GotLostBoxHelper() { }
        public void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Tag is TextBlock hintTextBlock)
            {
                hintTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        public void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Tag is TextBlock hintTextBlock)
            {
                // Проверяем, пустое ли поле текста
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    hintTextBlock.Visibility = Visibility.Visible;
                }
            }
        }


        public void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && passwordBox.Tag is TextBlock hintTextBlock)
            {
                hintTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        public void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox && passwordBox.Tag is TextBlock hintTextBlock)
            {
                if (string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    hintTextBlock.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
