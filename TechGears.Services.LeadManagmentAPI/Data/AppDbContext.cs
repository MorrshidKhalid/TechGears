using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TechGears.Services.LeadManagmentAPI.Models;

namespace TechGears.Services.LeadManagmentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Lead> Leads { get; set; }


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
