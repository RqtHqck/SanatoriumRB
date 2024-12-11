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
using Sanatorium.Models;
using Sanatorium.Services;
using Sanatorium.Utils;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для MainContentControl.xaml
    /// </summary>
    public partial class MainContentControl : UserControl
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        private MainWindow _parentWindow; // Родительское окно

        public MainContentControl(MainWindow parentWindow)
        {
            InitializeComponent();

            _parentWindow = parentWindow;
            User currentUser = UserSession.GetCurrentUser();
            UserNameTextBlock.Text = currentUser.UserName;
            BookingsTextBlock.Text = $"{(currentUser.Bookings != null && currentUser.Bookings.Count > 0 ? currentUser.Bookings.Count.ToString() : "0")}";

        }
        public void ProfileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ProfileEditingWindow profileEditing = new ProfileEditingWindow();

            profileEditing.ShowDialog();
        }

        private void ViewAllSanatoriums_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new SanatoriumsControl(_parentWindow));
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

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
    }
}
