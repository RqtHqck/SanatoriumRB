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
                    ResortImage.Source = null; // Clear the image if the URL is empty
                }
            }
        }

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

        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Tag is Room selectedRoom)
            {
                DisplayServices(selectedRoom);
            }
        }

        private void DisplayServices(Room room)
        {
            ServicesPanel.Children.Clear();
            foreach (var service in room.Services)
            {
                var checkBox = new CheckBox
                {
                    Content = $"{service.Name} - {service.Price} руб.",
                    Tag = service
                };

                checkBox.Checked += Service_Checked;
                checkBox.Unchecked += Service_Unchecked;
                ServicesPanel.Children.Add(checkBox);
            }
        }

        private void Service_Checked(object sender, RoutedEventArgs e)
        {
            CalculateTotalPrice();
        }

        private void Service_Unchecked(object sender, RoutedEventArgs e)
        {
            CalculateTotalPrice();
        }

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

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            CalculateTotalPrice();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _parentWindow.SetMainContent(new MainContentControl(_parentWindow));
        }
    }
}
