using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using whenAppModel.Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api")]
    public class AutenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersService service;

        public AutenticationController(IConfiguration config, IUsersService _service)
        {
            service = _service;
            _configuration = config;
        }

        public class AutenticationPayload
        {
            public string? username { get; set; }
            public string? password { get; set; }
        }

        //login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> SaveToken([FromBody] AutenticationPayload payload)
        {
            string username = payload.username;
            string password = payload.password;

            if (await service.Validation(username, password))
            {
                var claims = new[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                        new Claim("UserId", username)
                    };

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTParams:SecretKey"]));
                var mac = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["JWTParams:Issuer"],
                    _configuration["JWTParams:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: mac);
                var options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddMinutes(60);
                options.HttpOnly = true;

                Response.Cookies.Append("token", new JwtSecurityTokenHandler().WriteToken(token), new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                //return Ok(await service.Get(username));
            }
            return BadRequest(new { message = "Username or password is incorrect" });
        }

    }

}
