using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Sanatorium.Models;
using System.Windows;
using Sanatorium.Utils;
using Sanatorium.Services;
using System.Security.RightsManagement;

namespace Sanatorium.Repositories
{
    public class Database
    {
        private static string DbFilePath = Path.Combine(PathHelper.ProjectRootPath, "Data", "DatabaseTest.json"); // Путь к файлу
        public List<User> Users { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Resort> Resorts { get; set; }
        public List<Room> Rooms { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<ResortCategory> ResortCategories { get; set; }

        // Конструктор
        public Database()
        {
            var data = LoadData(DbFilePath);
            Users = data?.Users ?? new List<User>(); // Если данных нет, создаем пустой список пользователей
            Bookings = data?.Bookings ?? new List<Booking>();
            Resorts = data?.Resorts ?? new List<Resort>();
            Rooms = data?.Rooms ?? new List<Room>();
            RoomTypes = data?.RoomTypes ?? new List<RoomType>();
            ResortCategories = data?.ResortCategories ?? new List<ResortCategory>();
        }

        // Загрузка данных из JSON файла
        private DatabaseData LoadData(string filePath)
        {
            if (File.Exists(filePath))
            {
                // Если есть бд
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<DatabaseData>(json); // Десериализация в List<User>
            }
            else
            {
                // Если нету бд
                return new DatabaseData
                {
                    Users = new List<User>(),
                    Resorts = new List<Resort>(),
                    RoomTypes = new List<RoomType>(),
                    ResortCategories = new List<ResortCategory>(),
                    Bookings = new List<Booking>(),

                }; // Возвращаем пустые списки, если файл не найден
            }
        }

        // Сохранение данных в JSON файл
        public void SaveData()
        {
            var data = new DatabaseData 
            { 
                Users = Users,
                Resorts = Resorts,
                RoomTypes = RoomTypes,
                ResortCategories = ResortCategories,
                Bookings = Bookings,
            };
            var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented); // Сериализация в формат JSON
            File.WriteAllText(DbFilePath, json); // Запись в файл
        }

        public void AddUser(User user)
        {
            Users.Add(user);
            SaveData(); // Сохраняем изменения в базе
        }

        public void UpdateUser(User userUpdate)
        {
            User userStored = GetUserByEmail(UserSession.GetCurrentUser().Email);

            int userIndex = Users.IndexOf(userStored);
            
            if (userIndex >= 0)
            {             
                Users[userIndex].UserName = userUpdate.UserName;
                Users[userIndex].Email = userUpdate.Email;
                Users[userIndex].PasswordHash = userUpdate.PasswordHash;

                SaveData();
                UserSession.SetCurrentUser(userUpdate);
            }
            else
            {
                throw new InvalidOperationException("Не удалось найти пользователя в списке.");
            }
        }

        public User GetUserByEmail(String email)
        {
            return Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public List<Resort> GetAllResorts()
        {
            return Resorts;
        }

        public List<Room> GetAllRooms()
        {
            return Rooms;
        }

        public Resort GetResortByName(string name)
        {
            return Resorts.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }


        public void AddServiceToBooking(Guid bookingId, Service service)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                throw new InvalidOperationException("Бронирование не найдено.");
            }

            booking.SelectedServices.Add(service);
            SaveData(); // Сохраняем данные после изменения
        }

        // Получить все услуги для санатория
        public List<Service> GetServicesByResort(Guid resortId)
        {
            var resort = Resorts.FirstOrDefault(r => r.Id == resortId);
            if (resort != null)
            {
                return resort.Rooms.SelectMany(r => r.Services).ToList();
            }
            throw new InvalidOperationException("Санаторий не найден.");
        }

        // Получить все услуги для комнаты
        public List<Service> GetServicesByRoom(Guid roomId)
        {
            var room = Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room != null)
            {
                return room.Services;
            }
            throw new InvalidOperationException("Комната не найдена.");
        }

        // Получить все категории санаториев
        public List<ResortCategory> GetAllCategories()
        {
            return ResortCategories;
        }

        public double CalculateBookingTotalPrice(Guid bookingId)
        {
            var booking = Bookings.FirstOrDefault(b => b.Id == bookingId);
            if (booking == null)
            {
                throw new InvalidOperationException("Бронирование не найдено.");
            }

            var room = Rooms.FirstOrDefault(r => r.Id == booking.RoomId);
            if (room == null)
            {
                throw new InvalidOperationException("Комната не найдена.");
            }

            double totalPrice = room.Price; // Начальная цена за комнату

            // Добавляем стоимость услуг
            foreach (var service in booking.SelectedServices)
            {
                totalPrice += service.Price;
            }

            return totalPrice;
        }

    }
}
