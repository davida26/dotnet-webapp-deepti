using System;
using System.Collections.Generic;
using System.Text;

namespace Dot.Services.Models
{
    public class UserVm
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string AvatarUrl { get; set; }

        public string Url { get; set; }

        public string Type { get; set; }

        public List<FollowerVm> Followers { get; set; }

        public bool IsFavorite { get; set; }
    }
}
