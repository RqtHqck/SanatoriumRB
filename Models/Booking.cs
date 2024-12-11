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
        public Guid UserId { get; set; }
        public Guid ResortId { get; set; }
        public Guid RoomId { get; set; }
        public List<Service> SelectedServices { get; set; } // Услуги, выбранные для этой комнаты
        //public String Date { get; set; }

        public Booking(Guid userId, Guid resortId, Guid roomId, List<Service> selectedServices) 
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ResortId = resortId;
            RoomId = roomId;
            SelectedServices = selectedServices ?? new List<Service>();

        }
    }
}
