#nullable disable
using Microsoft.AspNetCore.Mvc;
using WhenUp;
using whenAppModel.Models;
using whenAppModel.Services;
using Microsoft.AspNetCore.Authorization;

namespace WhenUp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService service;
        public ContactsController(IContactsService _service)
        {
            service = _service;
        }

        [HttpGet]
        [NonAction]
        public async Task<User> GetCurrentUser()
        {

            var user = HttpContext.User.FindFirst("UserId")?.Value;
            return await service.Get(user);
        }


        // GET: Contacts - action number 1
        [HttpGet]
        [ActionName("Index")]
        public async Task<ICollection<User>> GetAllContacts()
        {
            User currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                return await service.GetAllContacts(currentUser.Username);
            }
            return null;
        }

        //POST: Contacts - action number 2
        [HttpPost]
        [ActionName("Index")]
        public async Task AddContact(string id, string name, string server)
        {
            User currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                await service.AddContact(currentUser, id, name, server);
            }
        }
        

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        //GET: Contacts/id - action number 2
        [Route("{id}")]
        [HttpGet]
        [ActionName("Index")]
        public async Task<User?> GetDetails(string id)
        {
            if (id == null)
            {
                return null;
            }

            User user = await service.GetContact(id);

            return user;
        }


        //PUT: Contacts/id - action number 2
        [HttpPut]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task<User?> UpdateUser(string id, string name = null, string server = null)
        {
            User oldUser = await service.GetContact(id);

            if (oldUser != null)
            {
                if (name != null)
                    oldUser.Nickname = name;

                if (server != null)
                    oldUser.Server = server;

                return await service.UpdateContact(oldUser, oldUser.Username);
            }
            return null;
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
