using EMS.DataAccess.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EMS.DataAccess.Data.Configuration
{
    public partial class BaseEntityConfiguration
    {
        public class UserConfiguration : IEntityTypeConfiguration<User>
        {
            public void Configure(EntityTypeBuilder<User> builder)
            {
                builder.Property(user => user.PhoneNumber)
                    .HasMaxLength(11)
                    .IsRequired(false);
            }
        }
    }
}
