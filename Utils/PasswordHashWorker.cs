using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Sanatorium.Utils
{
    public class PasswordHashWorker
    {
        private const int SaltSize = 16;

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Генерация случайной соли
                byte[] salt = new byte[SaltSize];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(salt);
                }

                // Хеширование пароля с солью
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = passwordBytes.Concat(salt).ToArray();
                byte[] hash = sha256.ComputeHash(saltedPassword);

                // Сохранение соли и хеша в одной строке
                string saltString = Convert.ToBase64String(salt); // Преобразуем соль в строку
                string hashString = Convert.ToBase64String(hash); // Преобразуем хеш в строку
                return $"{saltString}:{hashString}"; // Конкатенируем соль и хеш
            }
        }


        public static bool VerifyPassword(string enteredPassword, string storedHashWithSalt)
        {
            try
            {
                // Проверяем, что строка корректна
                if (string.IsNullOrWhiteSpace(storedHashWithSalt))
                {
                    throw new ArgumentException("Stored password hash with salt is null or empty.");
                }

                // Разделяем строку на соль и хеш
                string[] parts = storedHashWithSalt.Split(':');
                if (parts.Length != 2)
                {
                    // Если формат некорректен, выбрасываем исключение
                    throw new ArgumentException("Stored hash with salt is not in the correct format.");
                }

                string saltString = parts[0];
                string storedHashString = parts[1];

                // Преобразуем соль и хеш обратно в байты
                byte[] salt = Convert.FromBase64String(saltString);
                byte[] storedHash = Convert.FromBase64String(storedHashString);

                // Хешируем введённый пароль с той же солью
                using (var sha256 = SHA256.Create())
                {
                    byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
                    byte[] saltedPassword = enteredPasswordBytes.Concat(salt).ToArray();
                    byte[] enteredHash = sha256.ComputeHash(saltedPassword);

                    // Сравниваем хеши
                    return storedHash.SequenceEqual(enteredHash);
                }
            }
            catch (Exception ex)
            {
                // Показываем сообщение об ошибке
                MessageBox.Show($"Error when verifying password:\n\t{ex.Message}");
                return false;  // Возвращаем false в случае ошибки
            }
        }
    }
}
