﻿using Microsoft.EntityFrameworkCore;
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

        public async Task AddMessage(string from, string to, string content)
        {
            var chat = _context.Chats.Where(chat => chat.Compare(from, to)).First();
            await AddMessage(new Message
            {
                Chat = chat,
                Data = content,
                Date = DateTime.Now,
                Type = Message.Types.Text
            });
        }

        public async Task AddMessage(Message message)
        {
            _context.Messages.Add(message);
            message.Chat.Last = message.Data;
            message.Chat.LastDate = message.Date.ToString();
            await _context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessages(string p1, string p2)
        {
            var messages = _context.Messages.Where(message =>
            (message.Chat.Person1 == p1 && message.Chat.Person2 == p2) ||
            message.Chat.Person2 == p1 && message.Chat.Person1 == p2)
                .OrderByDescending(message => message.Date);

            return await messages.ToListAsync();

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

        public async Task<List<Message>?> GetMessages(User user)
        {
            var messages = _context.Messages
                .Where(message => (message.Chat.Person1 == user.Username || message.Chat.Person2 == user.Username))
                .OrderByDescending(message => message.Date);

            return await messages.ToListAsync();
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

        public async Task<List<Message>> GetAllMessages()
        {
            return await _context.Messages.ToListAsync();
        }
    }
}
