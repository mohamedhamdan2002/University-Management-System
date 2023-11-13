using EMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.DataAccess.Data.Configuration
{
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(f => f.University)
                .WithMany(u => u.Faculties)
                .HasForeignKey(u => u.UniversityId)
                .IsRequired();

            builder.ToTable("Faculties");
        }
    }
}
