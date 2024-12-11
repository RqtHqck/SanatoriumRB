﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatorium.Models
{
    public class RoomType
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        
        public RoomType(String name) 
        { 
            Id = Guid.NewGuid();
            Name = name;
        }
    }

}