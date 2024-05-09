using ECommerce.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.WebAPI.EntityConfigurations
{
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Property(x => x.Total).HasColumnType("decimal(18,2)");
        }
    }
}
