using System;
using System.IO;
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
using Sanatorium.Utils;
using Sanatorium.Services;

namespace Sanatorium.Views
{
    /// <summary>
    /// Логика взаимодействия для SanatoriumControl.xaml
    /// </summary>
    public partial class SanatoriumsControl : UserControl
    {
        private readonly GotLostBoxHelper _hintHelper = new GotLostBoxHelper();
        private MainWindow _parentWindow; // Родительское окно
        private Database _database = new Database();
        private List<Resort> _resorts;

        public SanatoriumsControl(MainWindow parentWindow)
        {
            InitializeComponent();

            SortingComboBox.SelectedIndex = 0; 
            FiltersComboBox.SelectedIndex = 0;

            var db = new Database();
            List<Resort> resorts = db.GetAllResorts();
            _parentWindow = parentWindow;

            // Отображаем санатории
            RenderSanatoriums(resorts);
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
                    Height = Double.NaN,
                    Padding = new Thickness(15),
                    Background = new SolidColorBrush(Colors.White),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Top
                };

                // Используем Grid для более гибкого управления размещением элементов
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) }); // Колонка для изображения
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Колонка для текста с растяжением

                var image = new Image
                {
                    Width = 150,
                    Height = 100,
                    Margin = new Thickness(0, 0, 20, 0),
                    Source = new BitmapImage(new Uri(System.IO.Path.Combine(PathHelper.ProjectRootPath, "Data", "Images", resort.ImageUrl), UriKind.RelativeOrAbsolute)) // Если есть изображение
                };

                Grid.SetColumn(image, 0); // Устанавливаем изображение в первую колонку
                grid.Children.Add(image);

                var textPanel = new StackPanel
                {
                    VerticalAlignment = VerticalAlignment.Top,  
                    HorizontalAlignment = HorizontalAlignment.Left  
                };

                var nameTextBlock = new TextBlock
                {
                    Text = resort.Name,
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                var descriptionTextBlock = new TextBlock
                {
                    Text = resort.Description,
                    FontSize = 16,
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                var contactsTextBlock = new TextBlock
                {
                    Text = $"Контакты: {resort.Contacts}",
                    FontSize = 16,
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                var ratingTextBlock = new TextBlock
                {
                    Text = $"Рейтинг: {resort.Rating}",
                    FontSize = 16,
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                var addressTextBlock = new TextBlock
                {
                    Text = $"Адрес: {resort.Address}",
                    FontSize = 16,
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                textPanel.Children.Add(nameTextBlock);
                textPanel.Children.Add(addressTextBlock);
                textPanel.Children.Add(descriptionTextBlock);
                textPanel.Children.Add(ratingTextBlock);
                textPanel.Children.Add(contactsTextBlock);

                Grid.SetColumn(textPanel, 1); // Устанавливаем текст в вторую колонку
                grid.Children.Add(textPanel);

                button.Content = grid; // Устанавливаем Grid как содержимое кнопки
                button.Tag = resort;  // Устанавливаем данные санатория в Tag

                // Устанавливаем обработчик события для клика
                button.Click += SanatoriumCard_Click;

                // Добавляем кнопку в список
                SanatoriumListPanel.Children.Add(button);
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new MainContentControl(_parentWindow));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем ComboBox
            ComboBox comboBox = sender as ComboBox;

            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                // Получаем значение Tag
                string tag = selectedItem.Tag as string;

                // Логика сортировки или фильтрации
                _resorts = _database.GetAllResorts();
                List<Resort> resortsSorted;

                // Проверка по выбранному ComboBox
                if (comboBox.Name == "SortingComboBox") // Для сортировки
                {
                    switch (tag)
                    {
                        case "All":
                            RenderSanatoriums(_resorts);
                            break;
                        case "Name":
                            resortsSorted = SanatoriumService.SortByName(_resorts);
                            RenderSanatoriums(resortsSorted);
                            break;
                        case "Rating":
                            resortsSorted = SanatoriumService.SortByRating(_resorts);
                            RenderSanatoriums(resortsSorted);
                            break;
                        case "IsNotBusy":
                            resortsSorted = SanatoriumService.FilterByOcupancyExists(_resorts);
                            RenderSanatoriums(resortsSorted);
                            break;
                        case "IsBusy":
                            resortsSorted = SanatoriumService.FilterByOcupancNotExists(_resorts);
                            RenderSanatoriums(resortsSorted);
                            break;
                    }
                }
                else if (comboBox.Name == "FiltersComboBox") // Для фильтрации
                {
                    switch (tag)
                    {
                        case "Cardiovascular":
                            resortsSorted = SanatoriumService.FilterByCategory(_resorts, "Сердечно-сосудистые");
                            RenderSanatoriums(resortsSorted);
                            break;
                        case "Musculoskeletal":
                            resortsSorted = SanatoriumService.FilterByCategory(_resorts, "Опорно-двигательные");
                            RenderSanatoriums(resortsSorted);
                            break;
                        case "PostTraumatic":
                            resortsSorted = SanatoriumService.FilterByCategory(_resorts, "Посттравматическая реабилитация");
                            RenderSanatoriums(resortsSorted);
                            break;
                        case "Strengthening":
                            resortsSorted = SanatoriumService.FilterByCategory(_resorts, "Укрепляющая терапия");
                            RenderSanatoriums(resortsSorted);
                            break;
                        case "Respiratory":
                            resortsSorted = SanatoriumService.FilterByCategory(_resorts, "Респираторные заболевания");
                            RenderSanatoriums(resortsSorted);
                            break;
                    }
                }
            }
        }


        private void SanatoriumCard_Click(object sender, RoutedEventArgs e)
        {
            SanatoriumService.SanatoriumCard_Click(_parentWindow, sender);
        }
    }
}
