using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Management.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Management.Infrastructure.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
