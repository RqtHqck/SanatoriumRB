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
        public Double Price {  get; set; }
        public int RoomNumber { get; set; }
        public bool IsBusy {  get; set; }

        public Room(Guid sanatoriumId, Guid typeId, Double price, int roomNumber, bool isBusy = false)
        {
            Id = Guid.NewGuid();
            SanatoriumId = sanatoriumId;
            TypeId = typeId;
            Price = price;
            RoomNumber = roomNumber;
            IsBusy = isBusy;
        }
    }
}
