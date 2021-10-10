using Dot.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Services.Models
{
    public class SearchResults : User
    {
        public int Total_Count { get; set; }
        public bool Incomplete_Results { get; set; }
        public List<User> Items { get; set; }
    }
}
