using Microsoft.EntityFrameworkCore;
using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{
    public class MessageService : IMessageService
    {
        private readonly WhenAppContext _context;

        public MessageService(WhenAppContext context)
        {
            _context = context;
        }

        public async Task AddMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessages(User from, User to)
        {
            var messages = _context.Messages.Where(message => (message.From.Username == from.Username && message.To.Username == to.Username) ||
            message.From.Username == to.Username && message.To.Username == from.Username).OrderByDescending(message => message.Date);

            return await messages.ToListAsync();

        }

        public async Task RemoveMessage(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>?> GetMessages(User user)
        {
            var messages = _context.Messages.Where(message => (message.From.Username == user.Username || message.To.Username == user.Username)).OrderByDescending(message => message.Date);

            return await messages.ToListAsync();
        }

        public async Task<Message?> GetMessage(int Id)
        {
            var messages = _context.Messages.Where(message => (message.Id == Id));

            return (Message?)messages;
        }

        public async Task<Message?> Update(Message Message)
        {
            if (Message != null)
            {
                await RemoveMessage((Message?)_context.Messages.Where(message => Message.Id == Message.Id));
                await AddMessage(Message);
                await _context.SaveChangesAsync();
            }
            return Message;

        }

        public async Task<Message?> GetLastMessage(User user)
        {
            if (user != null)
            {
                List<Message>? messages = await GetMessages(user);
                if (messages != null)
                {
                    return messages.FirstOrDefault();
                }

            }
            return null;
        }

        public async Task<List<Message>> GetAllMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<Message?> GetLastMessage()
        {
            if (_context.Messages != null)
            {
                List<Message>? messages = await GetAllMessages();
                if (messages != null)
                {
                    return messages.FirstOrDefault();
                }

            }
            return null;

        }
    }

}
