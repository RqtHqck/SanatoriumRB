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
            var db = new Database();
            List<Resort> resorts = db.GetAllResorts();
            UserNameTextBlock.Text = currentUser.UserName;
            BookingsTextBlock.Text = $"{(currentUser.Bookings != null && currentUser.Bookings.Count > 0 ? currentUser.Bookings.Count.ToString() : "0")}";
            RenderHighRatingSanatoriums(resorts);
        }

        // Заполнение карточек с санаториями
        private void RenderHighRatingSanatoriums(List<Resort> resorts)
        {
            // Очистить старые элементы
            SanatoriumListPanel.Children.Clear();

            foreach (var resort in resorts)
            {
                if (resort.Rating == 5)
                {
                    // Создаем кнопку, но скрываем рамку и контент, чтобы она не была видна
                    var button = new Button
                    {
                        Width = 600,
                        Height = 150,
                        Margin = new Thickness(10),
                        Background = new SolidColorBrush(Colors.Transparent),
                        BorderBrush = new SolidColorBrush(Colors.Gray),
                        BorderThickness = new Thickness(1),
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalContentAlignment = HorizontalAlignment.Left,
                        VerticalContentAlignment = VerticalAlignment.Top
                    };

                    // Используем Grid для более гибкого управления размещением элементов
                    var grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    // Изображение санатория
                    var image = new Image
                    {
                        Width = 150,
                        Height = 100,
                        Margin = new Thickness(0, 0, 20, 0),
                        Source = new BitmapImage(new Uri(System.IO.Path.Combine(PathHelper.ProjectRootPath, "Data", "Images", resort.ImageUrl), UriKind.RelativeOrAbsolute))
                    };

                    Grid.SetColumn(image, 0); // Устанавливаем изображение в первую колонку
                    grid.Children.Add(image);

                    // Панель для текста
                    var textPanel = new StackPanel
                    {
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    // Название санатория
                    var nameTextBlock = new TextBlock
                    {
                        Text = resort.Name,
                        FontSize = 20,
                        FontWeight = FontWeights.Bold,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    // Описание санатория
                    var descriptionTextBlock = new TextBlock
                    {
                        Text = resort.Description,
                        FontSize = 16,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    // Контакты
                    var contactsTextBlock = new TextBlock
                    {
                        Text = $"Контакты: {resort.Contacts}",
                        FontSize = 14,
                        Margin = new Thickness(0, 10, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    // Рейтинг
                    var ratingTextBlock = new TextBlock
                    {
                        Text = $"Рейтинг: {resort.Rating}",
                        FontSize = 14,
                        Margin = new Thickness(0, 10, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    // Адрес
                    var addressTextBlock = new TextBlock
                    {
                        Text = $"Адрес: {resort.Address}",
                        FontSize = 14,
                        Margin = new Thickness(0, 10, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    // Добавляем все элементы в панель
                    textPanel.Children.Add(nameTextBlock);
                    textPanel.Children.Add(addressTextBlock);
                    textPanel.Children.Add(descriptionTextBlock);
                    textPanel.Children.Add(ratingTextBlock);
                    textPanel.Children.Add(contactsTextBlock);

                    // Устанавливаем текст в колонку 1
                    Grid.SetColumn(textPanel, 1);
                    grid.Children.Add(textPanel);

                    // Устанавливаем Grid как содержимое кнопки
                    button.Content = grid;

                    // Устанавливаем данные санатория в Tag
                    button.Tag = resort;

                    // Добавляем обработчик клика
                    button.Click += SanatoriumCard_Click;

                    // Добавляем кнопку в список
                    SanatoriumListPanel.Children.Add(button);
                }   
            }
        }

        // Нажатие на карту с санаторием
        private void SanatoriumCard_Click(object sender, RoutedEventArgs e)
        {
            SanatoriumService.SanatoriumCard_Click(_parentWindow, sender);
        }

        // Изменить профиль
        public void ProfileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileEditing = new ProfileWindow();

            profileEditing.ShowDialog();
        }

        // Переход в выбор всех санаториев (рисуем окно выбора)
        private void ViewAllSanatoriums_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new SanatoriumsControl(_parentWindow));
        }
    }
}
