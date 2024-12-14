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
    using System.Collections.Generic;

    namespace Sanatorium.Views
    {
        /// <summary>
        /// Interaction logic for BookingControl.xaml
        /// </summary>
        public partial class BookingControl : UserControl
        {
            private readonly Database _db;
            private Resort SelectedResort { get; set; }
            private readonly MainWindow _parentWindow;

            public BookingControl(MainWindow parentWindow, Resort selectedResort)
            {
                InitializeComponent();

                RoomComboBox.Text = "Выбор комнаты";
                _parentWindow = parentWindow;

                // Ensure selectedResort is not null
                if (selectedResort == null)
                {
                    MessageBox.Show("Ошибка: Не выбран санаторий.");
                    return;
                }

                SelectedResort = selectedResort;

                DisplayResortInfo();
                LoadRoomsForResort();
            }

            // Показывает инфу по санаториуму
            private void DisplayResortInfo()
            {
                if (SelectedResort != null)
                {
                    ResortNameText.Text = SelectedResort.Name;
                    ResortAddressText.Text = SelectedResort.Address;
                    ResortDescriptionText.Text = SelectedResort.Description;
                    ResortContactsText.Text = SelectedResort.Contacts;
                    ResortRatingText.Text = $"Рейтинг: {SelectedResort.Rating}/5";

                    if (!string.IsNullOrEmpty(SelectedResort.ImageUrl))
                    {
                        ResortImage.Source = new BitmapImage(new Uri(Path.Combine(PathHelper.ProjectRootPath, "Data", "Images", SelectedResort.ImageUrl), UriKind.RelativeOrAbsolute));
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
                    RoomOccupancyText.Text = room.IsBusy == true ? $"В данный момент комната забронирована.\n Ожидайте выселения гостей, либо забронируйте другой номер." : "";
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
            if (SelectedResort.Rooms != null && SelectedResort.Rooms.Any())
            {
                foreach (var room in SelectedResort.Rooms)
                {
                    RoomComboBox.Items.Add(new ComboBoxItem
                    {
                        Content = $"{room.Type.Name} - {room.Price} руб./сутки",
                        Tag = room
                    });
                }

                // Выбираем первый элемент, если он есть
                RoomComboBox.SelectedIndex = 0;  // Это установит первый элемент в качестве выбранного
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
                double totalPrice = CalculateTotalPrice();
                TotalPriceTextBlock.Text = $"Итоговая стоимость: {totalPrice} руб.";
        }

            // Расчет итоговой стоимости, учитывая стоимость комнаты и выбранных услуг
            private double CalculateTotalPrice()
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
                return totalPrice;                
            }

            // Обработчик возврата на основной экран
            private void BackButton_Click(object sender, RoutedEventArgs e)
            {
                _parentWindow.SetMainContent(new SanatoriumsControl(_parentWindow));
            }

            // Обработчик бронирования
            private void BookingButton_Click(object sender, RoutedEventArgs e)
            {
                var _db = new Database();
                if (RoomComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is Room selectedRoom)
                {
                    selectedRoom.Services = GetSelectedServices();
                    SelectedResort = _db.GetResortById(SelectedResort.Id);
                    double totalPrice = CalculateTotalPrice();
                    SanatoriumService.Booking(SelectedResort, selectedRoom, _db, totalPrice);
            }
                else
                {
                    // Если комната не выбрана
                    MessageBox.Show("Пожалуйста, выберите комнату для бронирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        // Получить все выбранные пользователем услуги.
        public List<Service> GetSelectedServices()
        {
            var selectedServices = new List<Service>();

            foreach (var child in ServicesPanel.Children)
            {
                if (child is CheckBox checkBox && checkBox.IsChecked == true && checkBox.Tag is Service service)
                {
                    selectedServices.Add(service);
                }
            }

            return selectedServices;
        }

    }
}
