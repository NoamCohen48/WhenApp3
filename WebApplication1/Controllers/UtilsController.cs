using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using whenAppModel.Services;

namespace WhenUp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class UtilsController : Controller
    {
        
        private readonly IMessageService MessagesService;

        private readonly IContactsService contactService;
        private readonly IUsersService userService;

        public UtilsController(IContactsService ContactService, IUsersService UserService, IMessageService _service1)
        {
            contactService = ContactService;
            userService = UserService;
            MessagesService = _service1;
        }

        
        [Route("invitations")]
        //[ActionName("Index")]
        [HttpGet]
        public async Task<IActionResult> Invitations(string from, string to, string server)
        {
            await contactService.AddContact(to, from, "defalut nick name", server);
            return Ok();
        }

        
        [HttpGet]
        [Route("transfer")]
        public async Task<IActionResult> Transfer(string from, string to, string content)
        {
            // if from is in my server
            if(await userService.Get(from) != null)
            {
                await MessagesService.AddMessage(from, to, content);
            }
            return Ok();
        }
        
    }
}
