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

namespace WhenUp.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactsController : Controller
    {
        private readonly IContactsService service;
        User current_user = new User("admin", "admin", "admin", "ss");

        public ContactsController(IContactsService _service)
        {
            service = _service;
        }

        // GET: Contacts
        [HttpGet]
        [ActionName("Index")]
        public async Task<ICollection<User>> GetAllContacts()
        {
            return await service.GetAllContacts(current_user.Username);
        }

        

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName ("Index")]
        public async Task<User> AddContact([Bind("Username,Nickname,Password,Avatar")] User user)
        {
            if (ModelState.IsValid)
            {
                return await service.AddContact(current_user.Username, user.Username);
            }
            return null;
        }

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        
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

        

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task<User?> UpdateUser(string id, [Bind("Username,Nickname,Password,Avatar")] User user)
        {
            if (id != user.Username)
            {
                return null;
            }

            if (ModelState.IsValid)
            {
                User user_old = await service.Get(id);
                user_old.Avatar = user.Avatar;
                user_old.Password = user.Password;
                user_old.Nickname = user.Nickname;
                return await service.Update(user_old, user_old.Username);
            }

            return null;
        }

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
