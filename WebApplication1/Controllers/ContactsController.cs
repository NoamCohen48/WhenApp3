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
        User current_user = new User("noam", "admin", "admin", "ss");

        public ContactsController(IContactsService _service)
        {
            service = _service;
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
