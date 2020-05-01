using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Employer.Infrastructure.Data
{
    public class EmployerContext : DbContext
    {
        public EmployerContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Domain.Models.Employer> Employers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
