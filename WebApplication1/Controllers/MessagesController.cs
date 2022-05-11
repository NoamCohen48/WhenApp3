#nullable disable
using Microsoft.AspNetCore.Mvc;
using WhenUp;
using whenAppModel.Models;
using whenAppModel.Services;

namespace WhenUp.Controllers
{
    [ApiController]
    [Route("contacts/{id}/messages")]
    public class MessagesController : Controller
    {
        
        private readonly IMessageService MessagesService;
        private readonly IContactsService ContactsService;
        private readonly IUsersService userService;

        public MessagesController(IMessageService _service1, IContactsService _service2, IUsersService _service3 )
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
            List<Message> messages = await MessagesService.GetMessages(user.Username, id);
            List<Message> sentTrue = new List<Message>();
            List<Message> sentFalse = new List<Message>();
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].From == user.Username)
                {
                    sentTrue.Add(messages[i]);
                } else
                {
                    sentFalse.Add(messages[i]);
                }
            }
            var target1 = sentTrue.ConvertAll(message => new
            {
                id = message.Id,
                content = message.Content,
                created = message.Created,
                sent = true
            });
            var target2 = sentFalse.ConvertAll(message => new
            {
                id = message.Id,
                content = message.Content,
                created = message.Created,
                sent = false
            });
            var target3 = sentTrue.Concat(sentFalse);
            return (IActionResult)target3;
        }
        //action number 2
        [HttpPost]
        [ActionName("Index")]
        public async Task SendMessageToUser(string contact, string contect)
        {
            var user = await GetCurrentUser(); 
            await MessagesService.AddMessage(user.Username, contact, contect);
        }
        //action number 3
        [HttpGet]
        [Route("{id2}")]
        [ActionName("Index")]
        public async Task<Message?> GetMessageById(int id)
        {
           return await MessagesService.GetMessage(id);
        }
        [HttpPut]
        [Route("{id2}")]
        [ActionName("Index")]
        //action number 4
        public async Task UpdateMessage(int id, string contect)
        {
           await MessagesService.Update(id, contect);
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
