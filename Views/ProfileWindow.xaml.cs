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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sanatorium.Models;
using Sanatorium.Services;
using Sanatorium.Utils;
using Sanatorium.Views;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfileEditingWindow.xaml
    /// </summary>
    /// 

public partial class ProfileWindow : Window
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        public ProfileWindow()
        {
            InitializeComponent();
            ProfileContent.Content = new ProfileControl(this);
        }
        public void SetMainContent(UserControl newContent)
        {
            ProfileContent.Content = newContent;
        }
    }
}
