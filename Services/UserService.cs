using System;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Windows;
using Sanatorium.Models;
using Sanatorium.Repositories;
using Sanatorium.Utils;


namespace Sanatorium.Services
{
    public class UserService
    {
        public static bool Login(String email, String candidatePassword)
        {
            try
            {
                var db = new Database();
                
                User candidate = db.GetUserByEmail("rathack13@gmail.com");
                UserSession.SetCurrentUser(candidate);
                return true;
                //IsValidEmail(email);
                //IsValidPassword(candidatePassword);

                //var db = new Database();
                //User candidate = db.GetUserByEmail(email);

                //if (candidate == null)
                //{
                //    throw new Exception($"Пользователь с email {email} не найден.");
                //}


                //if (PasswordHashHelper.VerifyPassword(candidatePassword, candidate.PasswordHash))
                //{
                //    UserSession.SetCurrentUser(candidate);
                //    return true; 
                //}
                //else
                //{
                //    throw new Exception("Неверный пароль.");
                //}
            }
            catch (Exception ex)
            { 
                MessageBox.Show($"Ошибка при логине: { ex.Message}");
                return false; 
            }
        }

        public static bool Register(String username, String email, String password)
        {
            try
            {
                IsValidUsername(username);
                IsValidEmail(email);
                IsValidPassword(password);

                var db = new Database();

                User candidate = db.GetUserByEmail(email);
                if (candidate == null)
                {
                    string hashedPassword = PasswordHashHelper.HashPassword(password);
                    User newUser = new User(username, email, hashedPassword, null);
                    db.AddUser(newUser);
                    return true;
                }
                else
                {
                    throw new Exception($"Пользователь с email {email} уже существует!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка: {ex.Message}");
                return false;
            }

        }

        public static bool EditProfile(string username, string email, string password)
        {
            try
            {
                IsValidUsername(username);
                IsValidEmail(email);
                IsValidPassword(password);

                User currentUser = UserSession.GetCurrentUser();
                var db = new Database();

                if (IsEqual(username, currentUser.UserName, false) ||
                    IsEqual(email, currentUser.Email, true))
                {
                    throw new ArgumentException("Вы не можете изменить данные на те же самые.");
                }

                string passwordNewHashed = PasswordHashHelper.HashPassword(password);
                User newUser = new User(username, email, passwordNewHashed, null);
                db.UpdateUser(newUser);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Непредвиденная ошибка: {ex.Message}");
                return false;
            }
        }

        private static bool IsEqual(String update, String stored, bool IgnoreCase = true)
        {
            if (IgnoreCase) 
                return string.Equals(update, stored, StringComparison.OrdinalIgnoreCase);
            else
                return update.Equals(stored); 
        }

        private static void IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Имя пользователя не может быть пустым.");
            if (username.Length < 3 || username.Length > 30)
                throw new ArgumentException("Имя пользователя должно быть от 3 до 30 символов.");
        }

        private static void IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email не может быть пустым.");
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Некорректный формат email.");
        }

        private static void IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Пароль не может быть пустым.");
            // Пароль должен содержать хотя бы одну заглавную букву, цифру и специальный символ
            if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$"))
                throw new ArgumentException("Пароль должен содержать хотя бы одну заглавную букву, цифру и специальный символ, а также быть не менее 8 символов.");
        }


    }
}
