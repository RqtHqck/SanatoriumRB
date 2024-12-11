using System;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Sanatorium.Models;
using Sanatorium.Repositories;
using Sanatorium.Services;
using Sanatorium.Utils;

namespace Sanatorium.Views
{
    /// <summary>
    /// Interaction logic for BookingControl.xaml
    /// </summary>
    public partial class BookingControl : UserControl
    {
        private readonly Database _db;
        private readonly Resort _selectedResort;
        private readonly MainWindow _parentWindow;

        public BookingControl(MainWindow parentWindow, Resort selectedResort)
        {
            InitializeComponent();
            _parentWindow = parentWindow;

            // Ensure selectedResort is not null
            if (selectedResort == null)
            {
                MessageBox.Show("Ошибка: Не выбран санаторий.");
                return;
            }

            _selectedResort = selectedResort;
            _db = new Database();

            DisplayResortInfo();
            LoadRoomsForResort();
        }

        // Показывает инфу по санаториуму
        private void DisplayResortInfo()
        {
            if (_selectedResort != null)
            {
                ResortNameText.Text = _selectedResort.Name;
                ResortAddressText.Text = _selectedResort.Address;
                ResortDescriptionText.Text = _selectedResort.Description;
                ResortContactsText.Text = _selectedResort.Contacts;
                ResortRatingText.Text = $"Рейтинг: {_selectedResort.Rating}/5";

                if (!string.IsNullOrEmpty(_selectedResort.ImageUrl))
                {
                    ResortImage.Source = new BitmapImage(new Uri(Path.Combine(PathHelper.ProjectRootPath, "Data", "Images", _selectedResort.ImageUrl), UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ResortImage.Source = null;
                }
            }
        }

        // Показывает инфу по выбранной комнате
        private void DisplayRoomInfo(Room room)
        {
            if (room != null)
            {
                RoomTypeText.Text = $"Тип комнаты: {room.Type.Name}";
                RoomPriceText.Text = $"Стоимость: {room.Price} руб./сутки";
                RoomCapacityText.Text = $"Вместимость: {room.Capacity} чел.";
                RoomServicesText.Text = $"Услуги: {string.Join(", ", room.Services.Select(s => s.Name))}";
            }
            else
            {
                RoomTypeText.Text = "Тип комнаты: -";
                RoomPriceText.Text = "Стоимость: -";
                RoomCapacityText.Text = "Вместимость: -";
                RoomServicesText.Text = "Услуги: -";
            }
        }

        // Заполняет ComboBox комнатами
        private void LoadRoomsForResort()
        {
            RoomComboBox.Items.Clear();
            if (_selectedResort.Rooms != null && _selectedResort.Rooms.Any())
            {
                foreach (var room in _selectedResort.Rooms)
                {
                    RoomComboBox.Items.Add(new ComboBoxItem
                    {
                        Content = $"{room.Type.Name} - {room.Price} руб./сутки",
                        Tag = room
                    });
                }
            }
            else
            {
                MessageBox.Show("Нет доступных комнат для выбранного санатория.");
            }
        }

        // Обработчик изменения выбранного элемента в ComboBox с комнатами
        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is Room selectedRoom)
            {
                DisplayRoomInfo(selectedRoom);
                DisplayServices(selectedRoom);
            }
            else
            {
                DisplayRoomInfo(null);
            }
        }

        // Отображение услуг, доступных для выбранной комнаты
        private void DisplayServices(Room room)
        {
            ServicesPanel.Children.Clear();
            foreach (var service in room.Services)
            {
                var checkBox = new CheckBox
                {
                    Content = $"{service.Name} - {service.Price} руб.",
                    Tag = service,
                    FontSize = 16
                };

                checkBox.Checked += Service_Checked;
                checkBox.Unchecked += Service_Unchecked;
                ServicesPanel.Children.Add(checkBox);
            }
        }

        // Обработчик события, когда услуга выбрана
        private void Service_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalPrice();
        }

        // Обработчик события, когда услуга отменена
        private void Service_Unchecked(object sender, RoutedEventArgs e)
        {
            CalculateTotalPrice();
        }

        // Обработчик нажатия кнопки для расчета итоговой стоимости
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateTotalPrice();
        }

        // Расчет итоговой стоимости, учитывая стоимость комнаты и выбранных услуг
        private void CalculateTotalPrice()
        {
            var totalPrice = 0.0;

            if (RoomComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is Room selectedRoom)
            {
                totalPrice += selectedRoom.Price;

                foreach (var child in ServicesPanel.Children)
                {
                    if (child is CheckBox checkBox && checkBox.IsChecked == true && checkBox.Tag is Service service)
                    {
                        totalPrice += service.Price;
                    }
                }
            }

            TotalPriceTextBlock.Text = $"Итоговая стоимость: {totalPrice} руб.";
        }

        // Обработчик возврата на основной экран
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new SanatoriumsControl(_parentWindow));
        }

        // Обработчик бронирования
        private void BookingButton_Click(object sender, RoutedEventArgs e)
        {
            // Логика для бронирования
        }

    }
}
