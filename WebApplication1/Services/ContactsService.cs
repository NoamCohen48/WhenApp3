using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{
    public class ContactsService : IContactsService
    {
        private readonly WhenAppContext _context;
        private readonly IUsersService userService;


        public ContactsService(WhenAppContext context)
        {
            _context = context;
        }

        //TO-DO: Function that return all the user contacts.
        public async Task<ICollection<User>?> GetAllContacts(string Username)
        {
            return null;
        }

        //TO-DO: Function that return the user in contact format(id, name, server, last, lastdate)
        public async Task<User?> GetContact(string Username)
        {
            return null;
        }

        //TO-DO: Function to add new contact
        public async Task AddContact(User currentUser, string contactUserName, string contactNickName, string contactServer)
        {
            await _context.SaveChangesAsync();
        }

        //TO-DO: Function that update contact.
        public async Task<User?> UpdateContact(User NewUser, string OldUserUserName)
        {
            return null;
        }

        //TO-DO: Function that delete contact.
        public async Task DeleteContact(string UserName)
        {
        }

    }
}
