using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Data.Domain
{
    public class User : BaseEntity
    {
        public string Login { get; set; }

        public string Avatar_Url { get; set; }

        public string Url { get; set; }

        public string Followers_Url { get; set; }

        public string Type { get; set; }

        public bool IsFavorite { get; set; }
        public List<Follower> Followers { get; set; } = new List<Follower>();
    }
}
