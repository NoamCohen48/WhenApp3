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
    }

}
