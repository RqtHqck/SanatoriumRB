﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sanatorium.Models
{
    public class Resort
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Address { get; set; }
        public String Description { get; set; }
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
        public String Contacts { get; set; }
        public int TotalRooms { get; set; }
        public Guid CategoryId { get; set; }
        public Resort(String name, String address, String description, int rating, String contacts, int totalRoom, Guid categoryId)
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
