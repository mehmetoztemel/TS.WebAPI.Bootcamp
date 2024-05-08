using ECommerce.WebAPI.Entities.Abstract;

namespace ECommerce.WebAPI.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
