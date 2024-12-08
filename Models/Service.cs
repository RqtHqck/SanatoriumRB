using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatorium.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Double Price { get; set; }

        public Service(String name, Double price) { 
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
        }
    }
}
