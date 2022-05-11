using System;
using whenAppModel.Models;

namespace whenAppModel.Services
{
    public interface IMessageService
    {
        public Task AddMessage(string from, string to, string content);

        public Task AddMessage(Message message);

        public Task<bool> RemoveMessage(int id);

        public Task<ICollection<Message>?> GetMessages(string current_user, string contact_user);

        public Task<Message?> GetMessage(int Id);

        public Task<Message?> Update(int id, Message Message);

        public Task<List<Message>> GetAllMessages();
    }
}
