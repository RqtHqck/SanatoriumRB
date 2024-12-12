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
        public Double TotalPrice { get; set; }
        public List<Service> SelectedServices { get; set; }

        public Booking(Guid userId, Guid resortId, Guid roomId, Double totalPrice, List<Service> selectedServices = null) 
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ResortId = resortId;
            RoomId = roomId;
            TotalPrice = totalPrice;
            SelectedServices = selectedServices ?? new List<Service>();
        }
    }
}
