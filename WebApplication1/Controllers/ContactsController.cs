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

        public ContactsController(IContactsService ContactService, IUsersService UserService)
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
        public async Task<IActionResult> GetAllContacts()
        {
            User currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                return Ok(await contactService.GetAllContacts(currentUser));
            }
            return NotFound();
        }

        //POST: Contacts - action number 2
        [HttpPost]
        [ActionName("Index")]
        public async Task AddContact(string id, string name, string server)
        {
            User currentUser = await GetCurrentUser();
            if (currentUser != null)
            {
                await contactService.AddContact(currentUser.Username, id, name, server);
            }
            else
            {
                NotFound();
            }
        }


        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        //GET: Contacts/id - action number 3
        [Route("{id}")]
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> GetDetails(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            User currentUser = await GetCurrentUser();
            Contact contact = await contactService.GetContact(currentUser.Username,id);

            return Ok(contact);
        }


        //PUT: Contacts/id - action number 2
        [HttpPut]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task<IActionResult> UpdateContact(string id, string name, string server)
        {
            User currentUser = await GetCurrentUser();
            Contact contact = await contactService.GetContact(currentUser.Username, id);

            if (contact == null)
                return NotFound();

            await contactService.UpdateContact(currentUser.Username,id, name, server);

            return Ok();
        }

        //POST: Contacts/id - action number 2
        [HttpDelete]
        [Route("{id}")]
        [ActionName("Index")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            User currentUser = await GetCurrentUser();

            await contactService.DeleteContact(currentUser.Username,id);
            return Ok();
        }
    }
}
