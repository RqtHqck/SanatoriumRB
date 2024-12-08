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
using Sanatorium.Views;

namespace Sanatorium
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            User currentUser = UserSession.GetCurrentUser();
            UserNameTextBlock.Text = currentUser.UserName;
            BookingsTextBlock.Text = $"{(string.IsNullOrEmpty(currentUser.Bookings) ? "0" : currentUser.Bookings)}";


        }


        public void ProfileMenuItem_Click(object sender, RoutedEventArgs e) 
        {
            ProfileEditingWindow profileEditing = new ProfileEditingWindow();

            profileEditing.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ваш код обработки выбора в ComboBox
        }

    }
}
