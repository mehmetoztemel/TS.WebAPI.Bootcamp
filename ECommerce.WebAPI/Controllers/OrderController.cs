using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Controllers.Abstract;
using ECommerce.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ECommerce.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : BaseController
{
    private readonly ECommerceDbContext _dbContext;
    public OrderController(ECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [Route("createorder")]
    [HttpPost]
    public async Task<IActionResult> CreateOrder(CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
        List<Basket> basketItems = await _dbContext.Baskets.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

        var totalPrice = basketItems.Sum(x => x.Price);
        Order order = new Order()
        {
            UserId = userId,
            Total = totalPrice,
        };

        await _dbContext.Orders.AddAsync(order, cancellationToken);
        foreach (var item in basketItems)
        {
            var product = await _dbContext.Products.FindAsync(item.ProductId);
            OrderDetail detail = new OrderDetail()
            {
                OrderId = order.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = product.Price,
                Total = item.Price
            };
            await _dbContext.OrderDetails.AddAsync(detail, cancellationToken);
        }
        bool saveCheck = await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        if (saveCheck)
        {
            _dbContext.Baskets.RemoveRange(basketItems); 
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        return NoContent();
    }
}
