using ECommerce.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.WebAPI.EntityConfigurations
{
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(new AppUser { Email = "admin@admin.com", Password = "1234", FirstName = "Admin", LastName = "Admin", CreatedDate = DateTime.Now });
        }
    }
}
