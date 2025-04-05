using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechGears.Services.LeadManagmentAPI.Models;

namespace TechGears.Services.CustomerAPI.Configuration
{
    public class LeadConfig
        : IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.ToTable("Lead", "tech");

            builder.Property(c => c.FirstName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(c => c.LastName)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(c => c.CompanyName)
                .HasMaxLength(55);

            builder.Property(c => c.Indestry)
                .HasMaxLength(55);

            builder.Property(c => c.Email)
                .HasMaxLength(155);

            builder.Property(c => c.Phone)
                .IsRequired(false)
                .HasMaxLength(13);

            builder.Property(c => c.Source)
                .IsRequired();

            builder.Property(c => c.Status)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(c => c.AssignedTo)
                .IsRequired(false);
        }
    }
}
