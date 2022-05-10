using whenAppModel.Models;

namespace WebApplication1.Services
{
    public interface IOtherService
    {
        public Task<User?> Add(User user);
        public Task<User?> Get(string Username, string Password);
    }
}
