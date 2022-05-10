using whenAppModel.Models;

namespace whenAppModel.Services
	{
	public interface IContactsService
    {


		public Task AddContact(User currentUser, string contactUserName, string contactNickName, string contactServer);

		public Task<Contact?> GetContact(string id);

		public Task<ICollection<Contact>?> GetAllContacts(User user);

		public Task<Contact?> UpdateContact(Contact NewUser, string OldUserUserName);

		public Task DeleteContact(string UserName);

		public Task<bool> Validation(string Username, string Password);


	}
}
