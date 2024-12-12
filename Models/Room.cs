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
        public Guid ResortId { get; set; }
        public RoomType Type { get; set; }
        public Double Price {  get; set; }
        public int Capacity { get; set; }

        public bool IsBusy { get; set; }
        public List<Service> Services { get; set; }

        public Room(Guid resortId, RoomType type, Double price, int capacity, bool isBusy)
        {
            Id = Guid.NewGuid();
            ResortId = resortId;
            Type = type;
            Price = price;
            Capacity = capacity;
            IsBusy = isBusy;
            Services = new List<Service>();
        }
    }
}
