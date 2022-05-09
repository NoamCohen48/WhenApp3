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

        User current_user = new User("admin", "admin", "admin", "ss");

        public MessagesController(IMessageService _service1, IContactsService _service2)
        {
            MessagesService = _service1;
            ContactsService = _service2;
        }

        [HttpGet]
        [ActionName("Index")]
        public async Task<ICollection<Message>> GetMessagesByUser(string id)
        {
            return await MessagesService.GetMessages(current_user.Username, id);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task SendMessageToUser(string id, string content)
        {
            int nextId;
            var To = await ContactsService.Get(id);
            var LastMessage = await MessagesService.GetLastMessage();
            if (LastMessage == null)
            {
                nextId = 0;
            }
            else
            {
                nextId = LastMessage.Id + 1;
            }
            Message message = new Message(nextId, current_user, To, DateTime.Now, Message.Types.Text, content);
            Message message = new Message();
            await MessagesService.AddMessage(new Message
            {

            });
        }

        [HttpGet]
        [Route("{id2}")]
        [ActionName("Index")]
        public async Task<Message?> GetMessageById(string id, int id2)
        {
            var message = await MessagesService.GetMessage(id2);
            var user = await ContactsService.Get(id);
            if (message != null && user != null)
            {
                //if (message.From == user || message.To == user)
                {
                    return message;
                }
            }
            return null;
        }
    }
}
