using AutoMapper;
using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Controllers.Abstract;
using ECommerce.WebAPI.Dtos;
using ECommerce.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductController(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Route("getproducts")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            List<Product> products = await _dbContext.Products.ToListAsync();
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        [Route("createproduct")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Created();
        }

        [Route("updateproduct")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto request, CancellationToken cancellationToken)
        {
            bool productCheck = await _dbContext.Products.AnyAsync(x => x.Id == request.Id, cancellationToken);
            if (productCheck)
            {
                Product product = _mapper.Map<Product>(request);
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return Ok(new { Message = "Product Updated" });
            }
            else return BadRequest("Product cannot find");
        }

        [Route("getbyname")]
        [HttpGet]
        public async Task<IActionResult> GetProductByName(string name, CancellationToken cancellationToken)
        {
            bool productCheck = await _dbContext.Products.AnyAsync(x => x.Name == name, cancellationToken);
            if (productCheck)
            {
                Product product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
                return Ok(product);
            }
            else return BadRequest("Product cannot find");
        }

        [Route("getbyid/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProductById(string id, CancellationToken cancellationToken)
        {
            bool productCheck = await _dbContext.Products.AnyAsync(x => x.Id == Guid.Parse(id), cancellationToken);
            if (productCheck)
            {
                Product product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id), cancellationToken);
                return Ok(product);
            }
            else return BadRequest("Product cannot find");
        }

    }
}
