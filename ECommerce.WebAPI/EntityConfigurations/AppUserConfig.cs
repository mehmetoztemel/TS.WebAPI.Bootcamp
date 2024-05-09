using ECommerce.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.WebAPI.EntityConfigurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(x => x.Orders).WithOne(x => x.AppUser).HasForeignKey(x => x.UserId);
            builder.HasMany(x => x.Baskets).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}
