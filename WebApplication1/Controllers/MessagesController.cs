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
    [Route("contacts/{id}/messages")]
    public class MessagesController : Controller
    {

        private readonly IMessageService MessagesService;
        private readonly IContactsService ContactsService;
        private readonly IUsersService userService;

        public MessagesController(IMessageService _service1, IContactsService _service2, IUsersService _service3)
        {
            MessagesService = _service1;
            ContactsService = _service2;
            userService = _service3;
        }

        [HttpGet]
        [NonAction]
        public async Task<User> GetCurrentUser()
        {

            var user = HttpContext.User.FindFirst("UserId")?.Value;
            return await userService.Get(user);
        }

        //action number 1
        [HttpGet]
        [ActionName("Index")]
        public async Task<IActionResult> GetMessagesByUser(string id)
        {
            User user = await GetCurrentUser();
            var messages = await MessagesService.GetMessagesBetween(user.Username, id);
            var r = messages.Select(message => new
            {
                id = message.Id,
                content = message.Content,
                created = message.Created,
                sent = message.From == id
            }).ToList();
            return Ok(r);
        }

        //action number 2
        [HttpPost]
        [ActionName("Index")]
        public async Task SendMessageToUser(string id, string content)
        {
            var user = await GetCurrentUser();
            await MessagesService.AddMessage(user.Username, id, content);
        }

        //action number 3
        [HttpGet]
        [Route("{id2}")]
        [ActionName("Index")]
        public async Task<IActionResult?> GetMessageById(int id)
        {
            return Ok(await MessagesService.GetMessage(id));
        }

        [HttpPut]
        [Route("{id2}")]
        [ActionName("Index")]
        //action number 4
        public async Task UpdateMessage(int id, string contect)
        {
            await MessagesService.UpdateMessage(id, contect);
        }

        [HttpDelete]
        [Route("{id2}")]
        [ActionName("Index")]
        //action number 5
        public async Task DeleteMessage(int id)
        {
            await MessagesService.RemoveMessage(id);
        }

    }
}
