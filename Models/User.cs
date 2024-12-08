using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Xml;

namespace Sanatorium.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String PasswordHash { get; set; }
        public String Role { get; set; }
        public String Bookings { get; set; }

        public User(String userName, String email, String passwordHash, String role = "user")
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Bookings = string.Empty;
        }
    } 
}