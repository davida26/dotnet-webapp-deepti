using Dot.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace Dot.Data
{
    /// <summary>
    /// Data Store
    /// </summary>
    public class DotContext : DbContext
    {
        public DotContext(DbContextOptions<DotContext> options): base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Follower> Followers { get; set; }

    }
}

