using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Controllers.Abstract;
using ECommerce.WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly ECommerceDbContext _dbContext;
        public BasketController(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("getbasket")]
        [HttpGet]
        public async Task<IActionResult> GetBasket(Guid userId, CancellationToken cancellationToken)
        {
            bool basketCheck = await _dbContext.Baskets.AnyAsync(x => x.UserId == userId, cancellationToken);
            if (basketCheck)
            {
                var baskets = await _dbContext.Baskets.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(BasketProductAddDto request, CancellationToken cancellationToken)
        {
            // ürün ekleme ve güncelle burada olacak
            Guid userId = Guid.Parse(HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        //https://github.com/gabo71096/ReStore/blob/b333d5d1893ac3f821839de1b13824e25d574cef/API/Controllers/BasketController.cs
            bool basketCheck = await _dbContext.Baskets.AnyAsync(x => x.UserId == userId, cancellationToken);

            return NoContent();
        }


        // sepetten ürün silme olacak




    }
}
