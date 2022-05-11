using whenAppModel.Models;

namespace whenAppModel.Services
	{
	public interface IContactsService
    {


		public Task AddContact(string currentUser, string contactUserName, string contactNickName, string contactServer);

		public Task<Contact?> GetContact(string id);

		public Task<ICollection<Contact>?> GetAllContacts(User user);

		public Task UpdateContact(string id, string name, string server);

		public Task DeleteContact(string UserName);

	}
}
