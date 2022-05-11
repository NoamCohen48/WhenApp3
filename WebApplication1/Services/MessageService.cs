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

        //action number 1
        public async Task<ICollection<Message>?> GetMessages(string current_user, string contact_user)
        {
            using var context = _context;
            var sent = context.Messages
            .Where(message => message.From == current_user && message.To == contact_user);

            var recived = context.Messages
                .Where(message => message.From == contact_user && message.To == current_user);

            return await sent.Union(recived).ToListAsync();

        }
        //action number 2
        public async Task AddMessage(string from, string to, string content)
        {
            using var context = _context;
            context.Messages.Add(new Message(from, to, content));
            await context.SaveChangesAsync();
        }

        //action number 3
        public async Task<Message?> GetMessage(int Id)
        {
            using var context = _context;
            var q = from message in context.Messages
                    where message.Id == Id
                    select message;

            return q.First();
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
