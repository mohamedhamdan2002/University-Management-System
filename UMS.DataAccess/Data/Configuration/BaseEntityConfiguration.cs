using UMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace UMS.DataAccess.Data.Configuration
{
    public partial class BaseEntityConfiguration : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            builder.HasKey(entity =>  entity.Id);

            // it means the values in this column must be unique
            builder.HasAlternateKey(entity => entity.NationalID);

            builder.Property(entity => entity.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(entity => entity.LastName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(entity => entity.Address) // this need to apply migration 
                .HasMaxLength(350)
                .IsRequired();

            builder.Property(entity => entity.NationalID)
                .HasMaxLength(14)
                .IsRequired();

            builder.Property(entity => entity.Gender)
                .HasConversion(
                    x => x.ToString(),
                    x => (Gender)Enum.Parse(typeof(Gender), x)
                );

            builder.UseTpcMappingStrategy<BaseEntity>();
        }
    }
}
