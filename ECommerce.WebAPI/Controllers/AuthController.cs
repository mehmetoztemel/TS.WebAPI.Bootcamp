using ECommerce.WebAPI.Context;
using ECommerce.WebAPI.Dtos;
using ECommerce.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthController(ECommerceDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto request)
        {
            AppUser user = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password);
            if (user is not null)
            {
                var secretKey = _configuration.GetSection("TokenSettings:SecretKey").Value;

                DateTime expires = DateTime.Now.AddDays(1);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email,user.Email)
                };
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    expires: expires,
                    claims: claims,
                    notBefore: DateTime.Now,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                    );
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                return Ok(new { AccessToken = tokenHandler.WriteToken(jwtSecurityToken) });

            }
            else return NotFound("User cannot find");
        }
    }
}
