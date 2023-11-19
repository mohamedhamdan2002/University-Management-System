using UMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace UMS.DataAccess.Data.Configuration
{
    public class UniversityConfiguration : IEntityTypeConfiguration<University>
    {
        public void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Location)
                .HasMaxLength(250)
                .IsRequired();
            builder.ToTable("Universities");

            builder.HasData(LoadUniversities());
        }
        private static List<University> LoadUniversities()
        {
            return new()
            {
                new University
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "Tanta University",
                    Location = "in Tanta"
                },
                new University
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "A7a University",
                    Location = "In a7a"
                }
            };
        }
    }
}
