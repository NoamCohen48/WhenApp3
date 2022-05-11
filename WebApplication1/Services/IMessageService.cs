using System;
using whenAppModel.Models;

namespace whenAppModel.Services
{
    public interface IMessageService
    {
        public Task<ICollection<Message>> GetAllMessages();
        public Task<List<Message>?> GetMessages(string current_user, string contact_user);
        public Task<Message?> GetMessage(int Id);
        public Task Update(int id, string contect);
        public Task AddMessage(string from, string to, string contect);
        public Task<bool> RemoveMessage(int id);
    }
}
