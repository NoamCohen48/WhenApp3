using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{
    public class ContactsService : IContactsService
    {
        private readonly WhenAppContext _context;
        private readonly IUsersService _usersService;


        public ContactsService(WhenAppContext context, IUsersService usersService)
        {
            _context = context;
            _usersService = usersService;
        }

        //Function that return all the user contacts.
        public async Task<ICollection<Contact>?> GetAllContacts(User user)
        {
            if (user != null)
            {
                return _context.Contacts.Where(contact => contact.User == user.Username).ToList();
            }
            return null;
        }

        //Function that return the user in contact format(id, name, server, last, lastdate)
        public async Task<Contact?> GetContact(string id)
        {
            if (id != null)
            {
                var contacts = _context.Contacts.Where(contact => contact.Id == id).ToList();
                return contacts.FirstOrDefault();
            }
            return null;
        }

        //TO-DO: Function to add new contact
        public async Task AddContact(string currentUser, string contactId, string contactName, string contactServer)
        {
            if (currentUser != null && contactName != null && contactServer != null && contactId != null)
            {
                var isExists = _context.Contacts.Where(contact => contact.Id == contactId).ToList();
                if (isExists.Count == 0)
                {
                    Contact contact = new Contact(contactId, contactName, contactServer, currentUser);
                    await _context.Contacts.AddAsync(contact);
                    await _context.SaveChangesAsync();
                }
            }
        }

        //TO-DO: Function that update contact.
        public async Task<Contact?> UpdateContact(Contact NewUser, string OldUserUserName)
        {
            return null;
        }

        //TO-DO: Function that delete contact.
        public async Task DeleteContact(string UserName)
        {
        }

        


    }
}
