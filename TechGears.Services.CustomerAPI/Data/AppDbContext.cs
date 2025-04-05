using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TechGears.Services.CustomerAPI.Models;

namespace TechGears.Services.CustomerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        

        // To get your configuration picked up by migration.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(
               Assembly.GetExecutingAssembly()
               );

           base.OnModelCreating(modelBuilder);
        }
    }
}
