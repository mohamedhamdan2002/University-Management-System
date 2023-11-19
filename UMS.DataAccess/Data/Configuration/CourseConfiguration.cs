using UMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace UMS.DataAccess.Data.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(350)
                .IsRequired(false);

            builder.Property(c => c.Code)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(c => c.Credits)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(c => c.Semester)
                .HasColumnType("VARCHAR")
                .HasMaxLength(7)
                .HasConversion(
                    x => x.ToString(),
                    x => (SemesterType)Enum.Parse(typeof(SemesterType), x)
                );

            builder.HasOne(c => c.Division)
                .WithMany(div => div.Courses)
                .HasForeignKey(c => c.DivisionId)
                .IsRequired();

            builder.HasOne(c => c.Doctor)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DoctorId)
                .IsRequired(false);

            builder.HasOne(c => c.Staff)
                .WithMany(st => st.Courses)
                .HasForeignKey(c => c.staffId)
                .IsRequired(false);

            builder.ToTable("Courses");
        }
    }
}
