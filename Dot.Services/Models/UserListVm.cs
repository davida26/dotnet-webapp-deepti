using System;
using System.Collections.Generic;
using System.Text;

namespace Dot.Services.Models
{
    public class UserListVm
    {
        public List<UserVm> Users { get; set; } = new List<UserVm>();

        public bool IsSearchResult { get; set; }
    }
}
