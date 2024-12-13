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
using Sanatorium.Repositories;
using Sanatorium.Services;
using Sanatorium.Utils;
using Sanatorium.Views;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для ProfileMyBookingControl.xaml
    /// </summary>
    public partial class ProfileMyBookingControl : UserControl
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        private ProfileWindow _parentWindow;
        private Database _database = new Database();
        private List<Resort> Resorts { get; set; }
        public ProfileMyBookingControl(ProfileWindow parentWindow)
        {
            InitializeComponent();
            _parentWindow = parentWindow;
            var db = new Database();
            Resorts = SanatoriumService.FilterMyBookings(db.GetAllResorts());
            RenderSanatoriums(Resorts);
        }

        private void RenderSanatoriums(List<Resort> resorts)
        {
            // Очистить старые элементы
            SanatoriumListPanel.Children.Clear();

            foreach (var resort in resorts)
            {
                var button = new Button
                {
                    Width = Double.NaN,
                    Height = 150, // Установите фиксированную высоту для кнопки
                    Padding = new Thickness(15),
                    Background = new SolidColorBrush(Colors.White),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 0, 0, 10), // Добавляем отступ между карточками
                    BorderBrush = new SolidColorBrush(Colors.LightGray), // Легкая граница
                    BorderThickness = new Thickness(1)
                };

                // Используем StackPanel для вертикального размещения элементов внутри кнопки
                var stackPanel = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                };

                // Добавим строку с категорией
                var categoryTextBlock = new TextBlock
                {
                    Text = $"Категория: {resort.Category.Name}", // Используем поле Category из Resort
                    FontSize = 14,
                    FontWeight = FontWeights.Bold,
                    Foreground = new SolidColorBrush(Colors.Gray),
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                stackPanel.Children.Add(categoryTextBlock);

                // Название санатория
                var nameTextBlock = new TextBlock
                {
                    Text = resort.Name, // Используем поле Name из Resort
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Foreground = new SolidColorBrush(Colors.Black)
                };
                stackPanel.Children.Add(nameTextBlock);

                // Описание санатория
                var descriptionTextBlock = new TextBlock
                {
                    Text = resort.Description, // Используем поле Description из Resort
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Foreground = new SolidColorBrush(Colors.Black)
                };
                stackPanel.Children.Add(descriptionTextBlock);

                // Рейтинг санатория
                var ratingTextBlock = new TextBlock
                {
                    Text = $"Рейтинг: {resort.Rating}", // Используем поле Rating из Resort
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Foreground = new SolidColorBrush(Colors.Black)
                };
                stackPanel.Children.Add(ratingTextBlock);

                // Контакты санатория
                var contactsTextBlock = new TextBlock
                {
                    Text = $"Контакты: {resort.Contacts}", // Используем поле Contacts из Resort
                    FontSize = 16,
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Foreground = new SolidColorBrush(Colors.Black)
                };
                stackPanel.Children.Add(contactsTextBlock);

                // Устанавливаем содержимое кнопки как StackPanel
                button.Content = stackPanel;

                button.Tag = resort; // Устанавливаем данные санатория в Tag
                button.Click += SanatoriumCard_Click; // Устанавливаем обработчик события для клика

                // Добавляем кнопку в список
                SanatoriumListPanel.Children.Add(button);
            }
        }




        private void SanatoriumCard_Click(object sender, RoutedEventArgs e)
        {
            SanatoriumService.SanatoriumCard_Click(_parentWindow, sender);
        }


        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new ProfileControl(_parentWindow));
        }
    }
}
