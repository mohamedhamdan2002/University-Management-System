using UMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UMS.DataAccess.Data.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(g => g.Faculty)
                .WithMany(f => f.Groups)
                .HasForeignKey(g => g.FacultyId)
                .IsRequired();

            builder.Property(g => g.Scientific)
                .HasColumnType("VARCHAR")
                .HasMaxLength(8)
                .HasConversion(
                    x => x.ToString(),
                    x => (ScientificType)Enum.Parse(typeof(ScientificType), x)
                );
            builder.ToTable("Groups");
        }
    }
}
