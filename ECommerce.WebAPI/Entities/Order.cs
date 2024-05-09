using ECommerce.WebAPI.Entities.Abstract;

namespace ECommerce.WebAPI.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser AppUser { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderDetail> Details { get; set; }
    }
}
