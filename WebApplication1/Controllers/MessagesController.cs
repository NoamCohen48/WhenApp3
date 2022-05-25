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
    [Route("api/contacts/{id}/messages")]
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

        public class MessagesPayload
        {
            public string? content { get; set; }
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
                sent = message.From == user.Username
            }).OrderBy(m => m.created).ToList();

            return Ok(r);
        }

        //action number 2
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> SendMessageToUser(string id, [FromBody] MessagesPayload payload)
        {
            var user = await GetCurrentUser();

            if (await MessagesService.AddMessage(user.Username, id, payload.content) == null)
            {
                return BadRequest(new { message = "the contcat is not exsist" });
            };
            return Created("SendMessageToUser", null);
        }

        //action number 3
        [HttpGet]
        [Route("{id2}")]
        [ActionName("Index")]
        public async Task<IActionResult?> GetMessageById(int id2)
        {
            var message = await MessagesService.GetMessage(id2);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPut]
        [Route("{id2}")]
        [ActionName("Index")]
        //action number 4
        public async Task<IActionResult> UpdateMessage(int id2, [FromBody] MessagesPayload payload)
        {
            if (!await MessagesService.UpdateMessage(id2, payload.content))
            {
                return BadRequest(new { message = "the message is not exsist" });
            };
            return NoContent();
        }

        [HttpDelete]
        [Route("{id2}")]
        [ActionName("Index")]
        //action number 5
        public async Task<IActionResult> DeleteMessage(int id2)
        {
            if (!await MessagesService.RemoveMessage(id2))
            {
                return BadRequest(new { message = "the message is not exsist" });
            }
            return NoContent();
        }

    }
}
