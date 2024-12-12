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
        public List<Booking> Bookings { get; set; }

        public User(String userName, String email, String passwordHash, List<Booking> bookings)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Bookings = bookings ?? new List<Booking>();
        }
    } 
}