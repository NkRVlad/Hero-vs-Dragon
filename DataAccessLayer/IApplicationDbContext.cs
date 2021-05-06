using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer
{
    public interface IApplicationDbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Dragon> Dragons { get; set; }
        public DbSet<Hit> Hits { get; set; }
        public int SaveChanges();
    }
}
