using ECommerce.WebAPI.Entities.Abstract;

namespace ECommerce.WebAPI.Entities
{
    public class AppUser : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}
