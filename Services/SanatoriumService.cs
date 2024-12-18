﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Sanatorium.Models;
using Sanatorium.Repositories;
using Sanatorium.Views;
using Sanatorium.Utils;

namespace Sanatorium.Services
{
    public class SanatoriumService
    {
        public static List<Resort> SortByName(List<Resort> resorts)
        {
            return resorts.OrderBy(r => r.Name).ToList();
        }

        public static List<Resort> SortByRating(List<Resort> resorts)
        {
            return resorts.OrderByDescending(r => r.Rating).ToList();
        }
        
        public static List<Resort> FilterByOcupancyExists(List<Resort> resorts)
        { 
            return resorts.Where(resort => resort.Rooms.Any(room => room.IsBusy == false)).ToList();
        }
        public static List<Resort> FilterByOcupancNotExists(List<Resort> resorts)
        {
            return resorts.Where(resort => resort.Rooms.All(room => room.IsBusy == true)).ToList();
        }

        public static List<Resort> FilterByCategory(List<Resort> resorts, string categName)
        {
            return resorts.Where(resort => resort.Category.Name.Equals(categName)).ToList();
        }

        public static List<Resort> FilterMyBookings(Database db) 
        {
            List<Resort> resorts = db.GetAllResorts();
            User user = db.GetUserByEmail(UserSession.GetCurrentUser().Email);
            User currentUser = UserSession.GetCurrentUser();
            List<Guid> resortsIds = new List<Guid>();
            List<Booking> bookings = currentUser.Bookings;

            foreach (var booking in bookings)
            {
                resortsIds.Add(booking.ResortId);
            }

            return resorts.Where(r => resortsIds.Contains(r.Id)).ToList();
        }

        public static bool IsBusyRoom(Room room)
        { 
            return room.IsBusy == true ? true : false;
        }

        public static List<Service> GetSelectedServices(Room room)
        {
            List<Service> roomServices = room.Services;
            return roomServices;
        }

        public static void Booking(Resort resort, Room selectedRoom, Database db, double totalPrice)
        {
            try 
            {
                User user = db.GetUserByEmail(UserSession.GetCurrentUser().Email); // Получаем текущего пользователя
                List<Service> selectedServices = GetSelectedServices(selectedRoom); // Список выбранных услуг

                if (selectedRoom == null)
                {   
                    MessageBox.Show("Комната не найдена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (IsBusyRoom(selectedRoom))
                {
                    MessageBox.Show("Эта комната уже забронирована!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newBooking = new Booking(user.Id, selectedRoom.ResortId, selectedRoom.Id, totalPrice, selectedServices);
                user.Bookings.Add(newBooking);
                try
                {
                    db.UpdateUser(user);
                    db.UpdateResort(resort.Id, selectedRoom.Id);
                    MessageBox.Show("Комната успешно забронирована!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при работе с базой данных: \n{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }


        public static void SanatoriumCard_Click(Window parentWindow, object sender)
        {
            // Получаем объект Resort через Tag
            var button = sender as Button;
            var resort = button?.Tag as Resort;

            if (resort != null)
            {
                // Действие при клике на карточку санатория
                if (parentWindow is MainWindow mainWindow)
                {
                    mainWindow.SetMainContent(new BookingControl(mainWindow, resort)); // Передаем выбранный санаторий
                }
                else if (parentWindow is ProfileWindow profileWindow)
                {
                    profileWindow.Close();
                    // Ищем уже открытое окно MainWindow или другое окно, в котором будем открывать новый шаблон
                    foreach (var window in Application.Current.Windows)
                    {
                        if (window is MainWindow existingMainWindow)
                        {
                            // Обновляем содержимое в уже открытом MainWindow
                            existingMainWindow.SetMainContent(new BookingControl(existingMainWindow, resort)); // Передаем выбранный санаторий
                            break;
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("Данные о санатории не найдены.");
            }
        }

    }
}
