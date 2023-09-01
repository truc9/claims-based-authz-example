using ClaimsBasedAuthz.Data;
using ClaimsBasedAuthz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClaimsBasedAuthz.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AccountsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel model)
        {
            // Keep it simple
            var user = FakeDataSource.FakeUsers.FirstOrDefault(x => x.UserName == model.UserName && x.Password == model.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            string token = GenerateToken(user);
            return Ok(token);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim("tenant", user.Tenant),
            };
            var token = new JwtSecurityToken("api",
                "web",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
