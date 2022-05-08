using whenAppModel.Models;

namespace whenAppModel.Services
	{
	public interface IContactsService
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
