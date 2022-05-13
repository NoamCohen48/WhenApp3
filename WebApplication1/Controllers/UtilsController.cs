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

        public class UtilsPayload
        {
            public string? from { get; set; }
            public string? to { get; set; }
            public string? content { get; set; }
            public string? server { get; set; }
        }

        [HttpPost]
        [Route("invitations")]
        public async Task<IActionResult> Invitations([FromBody] UtilsPayload payload)
        {
            string? from = payload.from;
            string? to = payload.to;
            string? server = payload.server;

            await _contactService.AddContact(to, from, from, server);
            return Ok();
        }

        [HttpPost]
        [Route("transfer")]
        public async Task<IActionResult> Transfer([FromBody] UtilsPayload payload)
        {
            string? from = payload.from;
            string? to = payload.to;
            string? content = payload.content;

            // if from is in my server
            if (await _userService.Get(from) == null)
            {
                await _messagesService.AddMessage(from, to, content);
            }
            return Ok();
        }
        
    }
}
