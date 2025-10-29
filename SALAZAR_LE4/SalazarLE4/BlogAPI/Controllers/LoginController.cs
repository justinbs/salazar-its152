using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogAPI.Models;                 // LoginRequest
using BlogDataLibrary.Data;           // ISqlData
using BlogDataLibrary.Models;         // UserModel
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ISqlData _data;

        public LoginController(IConfiguration config, ISqlData data)
        {
            _config = config;
            _data = data;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var userId = await _data.ValidateUserAsync(model.Username, model.Password);
            if (userId is null) return Unauthorized();

            var token = GenerateToken(new UserModel { Id = userId.Value });
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserModel user)
        {
            var newId = await _data.RegisterUserAsync(user);
            if (newId is null or 0) return BadRequest("Registration failed.");
            var token = GenerateToken(new UserModel { Id = newId.Value });
            return Ok(token);
        }

        private string GenerateToken(UserModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
