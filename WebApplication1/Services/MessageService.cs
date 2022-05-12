using Microsoft.EntityFrameworkCore;
using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{
    public class MessageService : IMessageService
    {
        private readonly WhenAppContext _context;

        //constructor
        public MessageService(WhenAppContext context)
        {
            _context = context;
        }

        //not a action
        public async Task<ICollection<Message>> GetAllMessages()
        {
            using var context = _context;

            return await context.Messages.ToListAsync();
        }

        public async Task<ICollection<Message>> GetAllMessages(string username)
        {
            using var context = _context;

            var result = context.Messages
                .Where(message => message.From == username || message.To == username);

            return await result.ToListAsync();
        }

        //action number 1
        public async Task<ICollection<Message>?> GetMessagesBetween(string user1, string user2)
        {
            using var context = _context;

            var user1sent = context.Messages
                .Where(message => message.From == user1 && message.To == user2);

            var user2sent = context.Messages
                .Where(message => message.From == user2 && message.To == user1);

            return await user1sent.Union(user2sent).ToListAsync();

        }

        //action number 3
        public async Task<Message?> GetMessage(int Id)
        {
            using var context = _context;

            var result = await context.Messages.FindAsync(Id);

            return result;
        }

        //action number 2
        public async Task AddMessage(string from, string to, string content)
        {
            using var context = _context;

            context.Messages.Add(new Message(from, to, content));

            await context.SaveChangesAsync();
        }

        //action number 4
        public async Task Update(int id, string content)
        {
            using var context = _context;
            var m = await context.Messages.FindAsync(id);
            if (m != null)
            {
                m.Created = DateTime.Now;
                m.Content = content;
            }
            await context.SaveChangesAsync();
        }
        //action number 5
        public async Task<bool> RemoveMessage(int id)
        {
            using var context = _context;
            var m = await context.Messages.FindAsync(id);

            if (m == null)
                return false;

            context.Messages.Remove(m);
            await context.SaveChangesAsync();
            return true;
        }

    }
}
