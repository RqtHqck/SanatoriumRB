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
            try
            {

                Resorts = SanatoriumService.FilterMyBookings(_database);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                throw;
            }
            
            RenderSanatoriums(Resorts);
        }

        private void RenderSanatoriums(List<Resort> resorts)
        {
            // Очистить старые элементы
            SanatoriumListPanel.Children.Clear();
            try
            {
                foreach (var resort in resorts)
                {
                    
                    var currentUser = _database.GetUserByEmail(UserSession.GetCurrentUser().Email);
                    var userBookings = currentUser.Bookings.Where(b => b.ResortId == resort.Id).ToList();
                    foreach (var booking in userBookings)
                    {

                        var mainButton = new Button
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

                        // Добавляем обработчик для клика на карточку
                        mainButton.Click += (s, e) => SanatoriumCard_Click(s, e);

                        // Используем Grid для карточки
                        var grid = new Grid
                        {
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center,
                        };

                        // Определяем две колонки
                        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Содержимое
                        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Auto) });  // Кнопка удаления

                        // Панель с содержимым карточки
                        var contentPanel = new StackPanel
                        {
                            Orientation = Orientation.Vertical,
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Center
                        };

                        // Добавляем элементы в contentPanel
                        var categoryTextBlock = new TextBlock
                        {
                            Text = $"{resort.Category.Name}", // Используем поле Category из Resort
                            FontSize = 14,
                            FontWeight = FontWeights.Bold,
                            Foreground = new SolidColorBrush(Colors.Gray),
                            HorizontalAlignment = HorizontalAlignment.Left
                        };
                        contentPanel.Children.Add(categoryTextBlock);

                        var nameTextBlock = new TextBlock
                        {
                            Text = resort.Name, // Используем поле Name из Resort
                            FontSize = 20,
                            FontWeight = FontWeights.Bold,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Foreground = new SolidColorBrush(Colors.Black)
                        };
                        contentPanel.Children.Add(nameTextBlock);

                        var ratingTextBlock = new TextBlock
                        {
                            Text = $"Рейтинг: {resort.Rating}", // Используем поле Rating из Resort
                            FontSize = 16,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Foreground = new SolidColorBrush(Colors.Black)
                        };
                        contentPanel.Children.Add(ratingTextBlock);

                        var contactsTextBlock = new TextBlock
                        {
                            Text = $"Контакты: {resort.Contacts}", // Используем поле Contacts из Resort
                            FontSize = 16,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Foreground = new SolidColorBrush(Colors.Black)
                        };
                        contentPanel.Children.Add(contactsTextBlock);

                        var deleteButton = new Button
                        {
                            Content = "Удалить",
                            Width = 100,
                            Height = 30,
                            Background = new SolidColorBrush(Color.FromRgb(255, 77, 77)),
                            Foreground = new SolidColorBrush(Colors.White),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(10, 0, 0, 0)
                        };


                        // Присваиваем ID брони в Tag кнопки
                        deleteButton.Tag = booking.Id; // Присваиваем ID брони в Tag

                        // Обработчик клика по кнопке удаления
                        deleteButton.Click += (s, e) =>
                        {
                            DeleteBooking(s);
                            e.Handled = true; // Предотвращаем распространение события на карточку
                        };

                        // Добавляем содержимое в первую колонку
                        Grid.SetColumn(contentPanel, 0);
                        grid.Children.Add(contentPanel);

                        // Добавляем кнопку во вторую колонку
                        Grid.SetColumn(deleteButton, 1);
                        grid.Children.Add(deleteButton);

                        // Устанавливаем содержимое кнопки как Grid
                        mainButton.Content = grid;

                        // Устанавливаем данные санатория в Tag
                        mainButton.Tag = resort;

                        // Добавляем кнопку в список
                        SanatoriumListPanel.Children.Add(mainButton);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                throw;
            }
        }

        private void DeleteBooking(object sender)
        {
            try
            {
                var button = sender as Button;
                if (button != null)
                {
                    Guid bookingId = (Guid)button.Tag;
                    _database.DeleteBookingById(bookingId); // Удаляем бронирование из базы данных
                    _database = new Database();
                    Resorts = SanatoriumService.FilterMyBookings(_database); // Перезагружаем данные
                    RenderSanatoriums(Resorts); // Перерисовываем интерфейс с обновленными данными
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
                throw;
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
