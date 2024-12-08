using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using Sanatorium.Models;

namespace Sanatorium.Services
{
    public class UserSession
    {
        public static void SetCurrentUser(User user)
        {
            // Сериализуем объект пользователя в строку JSON
            string userJson = JsonConvert.SerializeObject(user);

            // Сохраняем строку в Application.Current.Properties
            Application.Current.Properties["CurrentUser"] = userJson;
        }
        public static User GetCurrentUser()
        {
            // Проверяем, есть ли сохраненный пользователь в Properties
            if (Application.Current.Properties.Contains("CurrentUser"))
            {
                string userJson = Application.Current.Properties["CurrentUser"] as string;

                // Десериализуем строку JSON обратно в объект User
                return JsonConvert.DeserializeObject<User>(userJson);
            }

            // Если нет пользователя, возвращаем null или создаем нового
            return null;
        }


    }
}
