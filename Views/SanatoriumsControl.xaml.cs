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
                    Width = Double.NaN,  // Кнопка на всю ширину
                    Height = Double.NaN, // Кнопка на всю высоту
                    Padding = new Thickness(15),  // Устанавливаем паддинг
                    Background = new SolidColorBrush(Colors.White)  // Устанавливаем белый цвет фона
                };

                // Наполняем карточку контентом
                var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };

                var image = new Image
                {
                    Width = 150,
                    Height = 100,
                    Margin = new Thickness(0, 0, 20, 0),
                    Source = new BitmapImage(new Uri(System.IO.Path.Combine(PathHelper.ProjectRootPath, "Data", "Images", resort.ImageUrl), UriKind.RelativeOrAbsolute)) // Если есть изображение
                };

                var textPanel = new StackPanel { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Left };
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

                var priceTextBlock = new TextBlock
                {
                    Text = $"Цена: {resort.getTotalPrice()} руб./сутки",
                    FontSize = 14,
                    Margin = new Thickness(0, 10, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                textPanel.Children.Add(nameTextBlock);
                textPanel.Children.Add(descriptionTextBlock);
                textPanel.Children.Add(priceTextBlock);

                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textPanel);

                button.Content = stackPanel;
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
                string sortType = selectedItem.Tag as string;

                // Логика сортировки
                _resorts = _database.GetAllResorts();
                List<Resort> resortsSorted;
                var db = new Database();
                switch (sortType)
                {
                    case "Name":
                        resortsSorted = SanatoriumService.SortByName(_resorts);
                        RenderSanatoriums(resortsSorted);
                        break;
                    case "Rating":
                        resortsSorted = SanatoriumService.SortByRating(_resorts);
                        RenderSanatoriums(resortsSorted);
                        break;
                }
            }
        }

        private void SanatoriumCard_Click(object sender, RoutedEventArgs e)
        {
            // Получаем объект Resort через Tag
            var button = sender as Button;
            var resort = button?.Tag as Resort;

            if (resort != null)
            {
                // Действие при клике на карточку санатория
                _parentWindow.SetMainContent(new BookingControl(_parentWindow, resort)); // Передаем выбранный санаторий
            }
            else
            {
                MessageBox.Show("Данные о санатории не найдены.");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _hintHelper.TextBox_GotFocus(sender, e);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            _hintHelper.TextBox_LostFocus(sender, e);
        }
    }
}
