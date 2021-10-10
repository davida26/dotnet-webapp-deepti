using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dot.Data.Domain
{
    public class Favorite : BaseEntity
    {
        public int UserId { get; set; }
    }
}
