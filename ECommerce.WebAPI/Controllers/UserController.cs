using AutoMapper;
using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Controllers.Abstract;
using ECommerce.WebAPI.Dtos;
using ECommerce.WebAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserController(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDto request, CancellationToken cancellationToken)
        {
            AppUser user = _mapper.Map<AppUser>(request);
            await _dbContext.AppUsers.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Created();
        }
    }
}
