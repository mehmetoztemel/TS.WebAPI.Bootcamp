using ECommerce.WebAPI.Entities;
using ECommerce.WebAPI.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ECommerce.WebAPI.Context
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var source in ChangeTracker.Entries<BaseEntity>())
            {
                switch (source.State)
                {
                    case EntityState.Added:
                        source.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        source.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


    }
}
