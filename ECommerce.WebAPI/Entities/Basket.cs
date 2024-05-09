using ECommerce.WebAPI.Entities.Abstract;

namespace ECommerce.WebAPI.Entities
{
    public class Basket : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
    }
}
