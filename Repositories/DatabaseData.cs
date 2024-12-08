using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanatorium.Models;

namespace Sanatorium.Repositories
{
    public class DatabaseData
    {
        public List<User> Users { get; set; }
        public List<Resort> Resorts { get; set; }
        public List<ResortCategory> ResortCategories { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Room> Rooms { get; set; }
        public List<RoomType> RoomTypes { get; set; }
        public List<Service> Services { get; set; }
    }
}
