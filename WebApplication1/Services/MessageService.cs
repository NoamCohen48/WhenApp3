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

        public async Task<ICollection<Message>> GetAllMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<ICollection<Message>?> GetMessages(string current_user, string contact_user)
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



        public async Task AddMessage(string from, string to, string content)
        {
            /*
            var chat = _context.C.Where(chat => chat.Compare(from, to)).First();
            await AddMessage(new Message
            {
                Chat = chat,
                Data = content,
                Date = DateTime.Now,
                Type = Message.Types.Text
            });
            */
        }

        public async Task AddMessage(Message message)
        {
            /*
            _context.Messages.Add(message);
            message.Chat.Last = message.Data;
            message.Chat.LastDate = message.Date.ToString();
            await _context.SaveChangesAsync();
            */
        }

        public async Task<List<Message>> GetMessages(string p1, string p2)
        {
            /*
            var messages = _context.Messages.Where(message =>
            (message.Chat.Person1 == p1 && message.Chat.Person2 == p2) ||
            message.Chat.Person2 == p1 && message.Chat.Person1 == p2)
                .OrderByDescending(message => message.Date);

            return await messages.ToListAsync();
            */
            return null;

        }

        public async Task<bool> RemoveMessage(int id)
        {
            var m = await _context.Messages.FindAsync(id);

            if (m == null)
                return false;

            _context.Messages.Remove(m);
            await _context.SaveChangesAsync();
            return true;
        }

       
        public async Task<Message?> GetMessage(int Id)
        {
            var q = from message in _context.Messages
                    where message.Id == Id
                    select message;

            return q.First();

            /*
            var r = await q.ToListAsync();

            if (r.Count == 0)
                return null;
            return r[0];
            */
        }

        public async Task<Message?> Update(int id, Message Message)
        {
            if (!await RemoveMessage(id))
                return null;

            await AddMessage(Message);

            await _context.SaveChangesAsync();

            return Message;
        }

      
    }
}
