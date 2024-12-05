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
        public List<Booking> Bookings { get; set; }
    }
}
