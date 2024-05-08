using ECommerce.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.WebAPI.EntityConfigurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasData(new Product { Name = "Black Ballpoint Pen", Description = "Black Ballpoint Pen", Price = 5m, CreatedDate = DateTime.Now });
            builder.HasData(new Product { Name = "Blue Ballpoint Pen", Description = "Blue Ballpoint Pen", Price = 6m, CreatedDate = DateTime.Now });
            builder.HasData(new Product { Name = "Red Ballpoint Pen", Description = "Red Ballpoint Pen", Price = 7m, CreatedDate = DateTime.Now });
        }
    }
}
