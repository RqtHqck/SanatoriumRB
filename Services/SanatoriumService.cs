using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Sanatorium.Models;
using Sanatorium.Repositories;
using Sanatorium.Views;

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
            return resorts.Where(resort => resort.Rooms.Any(room => room.IsBusy == 0)).ToList();
        }
        public static List<Resort> FilterByOcupancNotExists(List<Resort> resorts)
        {
            return resorts.Where(resort => resort.Rooms.Any(room => room.IsBusy == 1)).ToList();
        }

        public static void SanatoriumCard_Click(MainWindow _parentWindow, object sender)
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
    }
}
