using EMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EMS.DataAccess.Data.Configuration
{
    public class DepartmentDivisionConfiguration : IEntityTypeConfiguration<DepartmentDivision>
    {
        public void Configure(EntityTypeBuilder<DepartmentDivision> builder)
        {
            builder.HasKey(dd => new { dd.DepartmentId, dd.DivisionId });

            builder.HasOne(dd => dd.Department)
                .WithMany(d => d.DepartmentDivisions)
                .HasForeignKey(dd => dd.DepartmentId)
                .IsRequired();

            builder.HasOne(dd => dd.Division)
                .WithMany(div => div.DepartmentDivisions )
                .HasForeignKey(dd => dd.DivisionId)
                .IsRequired();
        }
    }
}
