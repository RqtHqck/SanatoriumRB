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
using Sanatorium.Utils;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для SanatoriumControl.xaml
    /// </summary>
    public partial class SanatoriumControl : UserControl
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        private MainWindow _parentWindow; // Родительское окно
        public SanatoriumControl(MainWindow parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new MainContentControl(_parentWindow));
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
