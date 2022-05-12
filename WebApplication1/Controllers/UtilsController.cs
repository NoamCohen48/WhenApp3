using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whenAppModel.Services;

namespace WhenUp.Controllers
{
    [ApiController]
    [Route("api")]
    public class UtilsController : Controller
    {
        
        private readonly IMessageService _messagesService;
        private readonly IContactsService _contactService;
        private readonly IUsersService _userService;

        public UtilsController(IContactsService ContactService, IUsersService UserService, IMessageService MessageService)
        {
            _contactService = ContactService;
            _userService = UserService;
            _messagesService = MessageService;
        }

        [HttpGet]
        [Route("invitations")]
        public async Task<IActionResult> Invitations(string from, string to, string server)
        {
            await _contactService.AddContact(to, from, from, server);
            return Ok();
        }

        [HttpGet]
        [Route("transfer")]
        public async Task<IActionResult> Transfer(string from, string to, string content)
        {
            // if from is in my server
            if(await _userService.Get(from) != null)
            {
                await _messagesService.AddMessage(from, to, content);
            }
            return Ok();
        }
        
    }
}
