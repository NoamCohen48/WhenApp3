using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using WebApplication1.Services;
using whenAppModel.Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api")]
    public class AutenticationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUsersService userService;
        JWTService JWTService;

        public AutenticationController(IConfiguration _config, IUsersService _service, JWTService _JWTService)
        {
            userService = _service;
            configuration = _config;
            JWTService = _JWTService;
        }

        public class AutenticationPayload
        {
            public string? username { get; set; }
            public string? password { get; set; }
        }

        //login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AutenticationPayload payload)
        {
            string username = payload.username;
            string password = payload.password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Username or password are missing" });
            }

            if (await userService.Validation(username, password))
            {
                var token = JWTService.CreateToken(username);
                return Ok(token);
                //return Ok(await service.Get(username));
            }
            return BadRequest(new { message = "Username or password is incorrect" });
        }

        //Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AutenticationPayload payload)
        {
            string username = payload.username;
            string password = payload.password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest(new { message = "Username or password are missing" });
            }

            if (await userService.Get(username) != null)
            {
                return BadRequest(new { message = "User already exists" });
            }

            await userService.Add(username, password);
            var token = JWTService.CreateToken(username);
            return Ok(token);
        }
    }

}
