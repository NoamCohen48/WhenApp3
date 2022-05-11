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
            return await _context.Messages.ToListAsync();
        }
        //action number 1
        public async Task<List<Message>?> GetMessages(string current_user, string contact_user)
        {
            var q1 = _context.Messages
                .Where(message => message.From == current_user && message.To == contact_user);
            //.Select(message => new {id = message.Id, content = message.Content, created = message.Created,
            //sent = true}
            //);

            var q2 = _context.Messages
                .Where(message => message.From == contact_user && message.To == current_user);
                /*
                .Select(message => new {
                    id = message.Id,
                    content = message.Content,
                    created = message.Created,
                sent = false
                });*/
            return await q1.Union(q2).ToListAsync();

        }
        //action number 2
        public async Task AddMessage(string from, string to, string contect)
        {
            int id = _context.Messages.Max(message => message.Id) + 1;  
            _context.Messages.Add(new Message(id, contect, DateTime.Now, from, to));
        }
        //action number 3
        public async Task<Message?> GetMessage(int Id)
        {
            var q = from message in _context.Messages
                    where message.Id == Id
                    select message;

            return q.First();
        }
        //action number 4
        public async Task Update(int id, string content)
        {
            var m = await _context.Messages.FindAsync(id);
            if (m != null)
            {
                m.Created = DateTime.Now;
                m.Content = content;
            }
        }
        //action number 5
        public async Task<bool> RemoveMessage(int id)
        {
            var m = await _context.Messages.FindAsync(id);

            if (m == null)
                return false;

            _context.Messages.Remove(m);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
