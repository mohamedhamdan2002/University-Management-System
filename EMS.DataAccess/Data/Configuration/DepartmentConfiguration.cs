using EMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.DataAccess.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(d => d.Group)
                .WithMany(g => g.Departments)
                .HasForeignKey(d => d.GroupId)
                .IsRequired();

            builder.ToTable("Departments");
        }
    }
}
