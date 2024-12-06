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

namespace Sanatorium.Repositories
{
    public class Database
    {
        private static string DbFilePath = Path.Combine(PathHelper.ProjectRootPath, "Data", "Database.json"); // Путь к файлу
        public List<User> Users { get; set; }
        public List<Booking> Bookings { get; set; }

        // Конструктор
        public Database()
        {
            var data = LoadData(DbFilePath);
            Users = data?.Users ?? new List<User>(); // Если данных нет, создаем пустой список пользователей
            Bookings = data?.Bookings ?? new List<Booking>();
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
                    Bookings = new List<Booking>()
                }; // Возвращаем пустые списки, если файл не найден
            }
        }

        // Сохранение данных в JSON файл
        public void SaveData()
        {
            var data = new DatabaseData 
            { 
                Users = Users,
                Bookings = Bookings
            };
            var json = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented); // Сериализация в формат JSON
            File.WriteAllText(DbFilePath, json); // Запись в файл
        }

        // Метод для добавления пользователя
        public void AddUser(User user)
        {
            Users.Add(user);
            Bookings = new List<Booking>();
            Booking usBookings = new Booking(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
            Bookings.Add(usBookings);
            SaveData(); // Сохраняем изменения в базе
        }

        // Пример метода для поиска пользователя по Email
        public User GetUserByEmail(string email)
        {
            return Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
