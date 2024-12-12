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
        public RoomType Type { get; set; }
        public Double Price {  get; set; }
        public int Capacity { get; set; }

        public int IsBusy { get; set; }
        public List<Service> Services { get; set; }

        public Room(RoomType type, Double price, int capacity)
        {
            Id = Guid.NewGuid();
            Type = type;
            Price = price;
            Capacity = capacity;
            Services = new List<Service>();
        }
    }
}
