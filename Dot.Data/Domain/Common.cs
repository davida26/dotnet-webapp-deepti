using System;
using System.Collections.Generic;
using System.Text;

namespace Dot.Data.Domain
{

    public abstract class BaseEntity
    {
        public int Id { get; set; } // all tables need an id column
    }
}
