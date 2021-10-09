using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Data.Domain
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string AvatarUrl { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }

        public List<Favorite> Favorites { get; set; }
    }
}
