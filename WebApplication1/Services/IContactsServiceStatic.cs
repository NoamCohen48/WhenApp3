using whenAppModel.Models;

namespace WebApplication1.Services
{
    public class IContactsServiceStatic
    {
		public Task<User?> Add(User user);

		public Task<User?> AddContact(string Username, string ContactName);

		public Task<User?> Get(string Username);

		public Task<User?> Get(string Username, string Password);

		public Task<List<User>?> GetAllContacts(string Username);

		public Task<User?> Update(User NewUser, string OldUserUserName);

		public Task<User?> Delete(string UserName);
	}
}
