using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanatorium.Models;

namespace Sanatorium.Models
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid SanatoriumId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoomId { get; set; }

        public Booking(Guid? sanatoriumId, Guid userId, Guid? roomId) 
        {
            Id = Guid.NewGuid();
            SanatoriumId = (Guid)sanatoriumId;
            UserId = userId;
            RoomId = (Guid)roomId;
        }
    }
}
