using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatorium.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public Guid SanatoriumId { get; set; }
        public Guid TypeId { get; set; }
        public double Price {  get; set; }

        public Room(Guid sanatoriumId, Guid typeId, double price) {
        
        }
    }
}
