using whenAppModel.Models;

namespace whenAppModel.Services
	{
	public interface IContactsService
    {


		public Task AddContact(User currentUser, string contactUserName, string contactNickName, string contactServer);

		public Task<User?> GetContact(string Username);

		public Task<ICollection<User>?> GetAllContacts(string Username);

		public Task<User?> UpdateContact(User NewUser, string OldUserUserName);

		public Task DeleteContact(string UserName);



    }
}
