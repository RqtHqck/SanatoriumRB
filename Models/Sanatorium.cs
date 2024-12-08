using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sanatorium.Models
{
    public class Sanatorium
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        private int _rating;
        public int Rating 
        {
            get { return _rating; }
            set
            {
                if (value < 0 || value > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Рейтинг должен быть в пределах от 0 до 5.");
                }
                Rating = value;
            }

        }
        public string Contacts { get; set; }
        public int TotalRooms { get; set; }
        public Guid CategoryId { get; set; }
        public Sanatorium(String name, String address, String description, int rating, String contacts, int totalRoom, Guid categoryId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Address = address;
            Description = description;
            Rating = rating;
            Contacts = contacts;
            TotalRooms = totalRoom;
            CategoryId = categoryId;
        }
    }


}
