using ECommerce.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.WebAPI.EntityConfigurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.Details).WithOne(x => x.Order).HasForeignKey(x => x.OrderId);
            builder.Property(x => x.Total).HasColumnType("decimal(18,2)");
        }
    }
}
