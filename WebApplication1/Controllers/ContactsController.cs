#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhenUp;
using whenAppModel.Models;
using whenAppModel.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WhenUp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService service;
        public IConfiguration _configuration;
        User current_user = new User("noam", "admin", "admin", "ss");

        public ContactsController(IContactsService _service, IConfiguration config)
        {
            service = _service;
            _configuration = config;
        }

        //login
        [HttpPost]
        public async Task<IActionResult> Post(string username,string password)
        {
            if(await service.Validation(username, password))
            {
                var claims = new []
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JwtParms:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
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
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    
            }


        }

        // GET: Contacts - action number 1
        [HttpGet]
        [ActionName("Index")]
        public async Task<ICollection<User>> GetAllContacts()
        {
            return await service.GetAllContacts(current_user.Username);
        }


        //action number 2
        // POST: Contacts/Create 
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        [ActionName("Index")]
        //([fromBody] User user)
        public async Task AddContact(string id, string name, string server)
        {
            //return await service.AddContact(current_user.Username, name);
            await service.AddContact(current_user.Username, id);
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        //action number 3
        [Route("{id}")]
        [HttpGet]
        [ActionName("Index")]
        public async Task<User?> GetDetails(string id)
        {
            if (id == null)
            {
                return null;
            }

            User user = await service.Get(id);

            return user;
        }


        //action number 4
        [HttpPut]
        //[ValidateAntiForgeryToken]
        [Route("{id1}")]
        [ActionName("Index")]
        public async Task<User?> UpdateUser(string id1, string name = null, string server = null)
        {
            User user_old = await service.Get(id1);

            if(name != null)
                user_old.Nickname = name;

            if(server != null)
                user_old.Server = server;
            
            return await service.Update(user_old, user_old.Username);
        }
        //action number 5
        // POST: Contacts/Delete/5
        [HttpDelete]
        [ValidateAntiForgeryToken]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task DeleteConfirmed(string id)
        {
            await service.Delete(id);
        }



    }
}
