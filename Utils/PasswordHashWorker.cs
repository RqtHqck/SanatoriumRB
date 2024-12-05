using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace Sanatorium.Utils
{
    public class PasswordHashWorker
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] salt = Encoding.UTF8.GetBytes("YourRandomSalt"); // Используйте уникальную соль для каждого пользователя
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltedPassword = passwordBytes.Concat(salt).ToArray();

                return Convert.ToBase64String(sha256.ComputeHash(saltedPassword));
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string enteredHash = HashPassword(enteredPassword);
            return storedHash == enteredHash;
        }
    }
}


public class PasswordHelper
{
    
}