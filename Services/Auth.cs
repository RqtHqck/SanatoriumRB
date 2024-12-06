using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Sanatorium.Models;
using Sanatorium.Repositories;
using Sanatorium.Utils;


namespace Sanatorium.Services
{
    public class Auth
    {
        public static bool Login(String email, String candidatePassword)
        {
            var db = new Database();
            //PathHelper.logRootPath();
            User candidate = db.GetUserByEmail(email);
            if (candidate == null)
            {
                MessageBox.Show($"Пользователь с таким email {email} не найден!");
                return false;
            }
            else
            {
                string hashedPassword = candidate.PasswordHash;
                bool passwordResult = PasswordHashHelper.VerifyPassword(candidatePassword, hashedPassword);
                if (passwordResult)
                {
                    return true;
                }
                else 
                {
                    MessageBox.Show($"Пароль введён неверно!");
                    return false;
                }
            }
        }

        public static bool Register(String username, String email, String password)
        {

            if (!IsValidUsername(username))
            {
                MessageBox.Show("Имя пользователя должно быть от 3 до 30 символов.");
                return false;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Неверный формат email.");
                return false;
            }

            if (!IsValidPassword(password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну заглавную букву, цифру и специальный символ.");
                return false;
            }


            var db = new Database();

            User candidate = db.GetUserByEmail(email);
            if (candidate == null)
            {
                string hashedPassword = PasswordHashHelper.HashPassword(password);
                User newUser = new User(username, email, hashedPassword);
                db.AddUser(newUser);
                return true;
            }
            else
            {
                MessageBox.Show($"Пользователь с email {email} уже существует!");
                return false;
            }            
        }

        public static bool IsValidUsername(String username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 30)
                return false;
            return true;
        }

        public static bool IsValidEmail(String email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailPattern);
        }

        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
            {
                return false;
            }

            // Регулярное выражение для проверки наличия хотя бы одной заглавной буквы, цифры и спецсимвола
            var passwordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$";
            return Regex.IsMatch(password, passwordPattern);
        }
    }
}
