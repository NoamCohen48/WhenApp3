﻿#nullable disable
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
        private readonly IContactsService contactService;
        private readonly IUsersService userService;

        public ContactsController(IContactsService ContactService, IUsersService UserService )
        {
            contactService = ContactService;
            userService = UserService;
        }

        [HttpGet]
        [NonAction]
        public async Task<User> GetCurrentUser()
        {

            var user = HttpContext.User.FindFirst("UserId")?.Value;
            return await userService.Get(user);
        }


        // GET: Contacts - action number 1
        [HttpGet]
        [ActionName("Index")]
        public async Task<ICollection<Contact>> GetAllContacts()
        {
            User currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                return await contactService.GetAllContacts(currentUser);
            }
            return null;
        }

        //POST: Contacts - action number 1
        [HttpPost]
        [ActionName("Index")]
        public async Task AddContact(string id, string name, string server)
        {
            User currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                await contactService.AddContact(currentUser.Username, id, name, server);
            }
        }
        

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        //GET: Contacts/id - action number 2
        [Route("{id}")]
        [HttpGet]
        [ActionName("Index")]
        public async Task<Contact?> GetDetails(string id)
        {
            if (id == null)
            {
                return null;
            }

            Contact contact = await contactService.GetContact(id);

            return contact;
        }


        //PUT: Contacts/id - action number 2
        [HttpPut]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task<Contact?> UpdateContact(string id, string name = null, string server = null)
        {
            Contact oldContact = await contactService.GetContact(id);

            if (oldContact != null)
            {
                if (name != null)
                    oldContact.Name = name;

                if (server != null)
                    oldContact.Server = server;

                return await contactService.UpdateContact(oldContact, oldContact.Id);
            }
            return null;
        }

        //POST: Contacts/id - action number 2
        [HttpDelete]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task DeleteContact(string id)
        {
            await contactService.DeleteContact(id);
        }
    }
}
