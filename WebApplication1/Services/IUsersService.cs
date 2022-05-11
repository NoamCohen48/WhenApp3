using whenAppModel.Models;

namespace whenAppModel.Services
	{
	public interface IUsersService
    {

		public Task<User?> Get(string Username);

		public Task Delete(string UserName);

		public Task<User?> Add(User user);
		public Task<bool> Validation(string Username, string Password);

	}
}
