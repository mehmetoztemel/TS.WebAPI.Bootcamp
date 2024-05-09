using AutoMapper;
using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Controllers.Abstract;
using ECommerce.WebAPI.Dtos;
using ECommerce.WebAPI.Entities;
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
        private readonly IMapper _mapper;

        public BasketController(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Route("getbasket")]
        [HttpGet]
        public async Task<IActionResult> GetBasket(CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            bool basketCheck = await _dbContext.Baskets.AnyAsync(x => x.UserId == userId, cancellationToken);
            if (basketCheck)
            {
                List<Basket> basketItems = await _dbContext.Baskets.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
                List<BasketDto> basketDtoList = _mapper.Map<List<BasketDto>>(basketItems);
                decimal totalPrice = 0;
                foreach (var item in basketItems)
                {
                    totalPrice += item.Price;
                }

                var result = new BasketResult(basketDtoList, totalPrice);
                return Ok(result);
            }
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(BasketProductAddDto request, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            bool basketCheck = await _dbContext.Baskets.AnyAsync(x => x.UserId == userId, cancellationToken);
            if (basketCheck)
            {
                var basketItems = await _dbContext.Baskets.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
                Product product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

                foreach (var item in basketItems)
                {
                    if (request.ProductId == item.ProductId)
                    {
                        if (request.Quantity != item.Quantity)
                        {
                            item.Quantity = request.Quantity;
                            item.Price = product.Price * request.Quantity;
                            _dbContext.Update(item);
                        }
                    }
                    else
                    {
                        Basket basket = _mapper.Map<Basket>(request);
                        basket.Price = product.Price * request.Quantity;
                        basket.UserId = userId;
                        await _dbContext.AddAsync(basket, cancellationToken);
                    }
                }
            }
            else
            {
                Basket basket = _mapper.Map<Basket>(request);
                Product product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);
                basket.UserId = userId;
                basket.Price = product.Price * request.Quantity;
                await _dbContext.AddAsync(basket, cancellationToken);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveBasketItem(Guid productId, CancellationToken cancellationToken)
        {
            Guid userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            Basket basket = await _dbContext.Baskets.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
            if (basket is not null)
                _dbContext.Remove(basket);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Ok(new { Message = "Product removed in ur basket" });
        }

    }
}
