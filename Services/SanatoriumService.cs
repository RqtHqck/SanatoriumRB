using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Sanatorium.Models;

namespace Sanatorium.Services
{
    public class SanatoriumService
    {
        public static List<Resort> SortByName(List<Resort> resorts)
        {
            return resorts.OrderBy(r => r.Name).ToList();
        }

        public static List<Resort> SortByRating(List<Resort> resorts)
        {
            return resorts.OrderByDescending(r => r.Rating).ToList();
        }
    }
}
