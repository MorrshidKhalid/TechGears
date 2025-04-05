using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechGears.Services.CustomerAPI.Models;

namespace TechGears.Services.CustomerAPI.Configuration
{
    public class CustomerConfig
        : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer", "tech");

            builder.Property(c => c.FirstName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(c => c.CompanyName)
                .HasMaxLength(55)
                .IsRequired(false);

                
            builder.Property(c => c.Address)
                .HasMaxLength(155)
                .IsRequired(false);

            builder.Property(c => c.Indestry)
                .HasMaxLength(55)
                .IsRequired(false);

                builder.Property(c => c.Email)
                .HasMaxLength(155)
                .IsRequired();

            builder.Property(c => c.Phone)
                .HasMaxLength(13)
                .IsRequired(false);

            builder.Property(c => c.Type)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(c => c.LeadId)
                .IsRequired(false);

            builder.Property(c => c.AssignedTo)
                .IsRequired(false);
            
        }
    }
}
