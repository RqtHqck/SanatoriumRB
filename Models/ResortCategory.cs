using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatorium.Models
{
    public class ResortCategory
    {
        public Guid Id { get; set; }
        public String Name { get; set; }

        public ResortCategory(String name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
