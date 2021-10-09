using Dot.Data.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace Dot.Data
{
    /// <summary>
    /// Data Store
    /// </summary>
    public class DotContext : DbContext
    {
        public DotContext(DbContextOptions<DotContext> options)
        : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Favorite> Favorite { get; set; }

    }
}
