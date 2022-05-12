using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{
    public class ContactsService : IContactsService
    {
        private readonly WhenAppContext _context;


        public ContactsService(WhenAppContext Context)
        {
            _context = Context;
        }

        //Function that return all the user contacts.
        public async Task<ICollection<Contact>?> GetAllContacts(string username)
        {
            using var context = _context;

            return await context.Contacts.Where(contact => contact.ContactOfUsername == username).ToListAsync();

        }

        public async Task<ICollection<Contact>?> GetAllContacts(User user)
        {
            using var context = _context;
            if (user == null)
            {
                return null;
            }
            return await context.Contacts.Where(contact => contact.ContactOfUsername == user.Username).ToListAsync();

        }

        //Function that return the user in contact format(id, name, server, last, lastdate)
        public async Task<Contact?> GetContact(string contactOf, string contactUsername)
        {
            using var context = _context;

            var contacts = await context.Contacts.FindAsync(contactUsername, contactOf);

            return contacts;
        }

        //TO-DO: Function to add new contact
        public async Task<bool> AddContact(string contactOf, string contactUsername, string contactNick, string contactServer)
        {
            if (contactOf == null || contactNick == null || contactServer == null || contactUsername == null)
            {
                return false;
            }

            using var context = _context;

            var isExists = await context.Contacts.FindAsync(contactUsername, contactOf) != null;
            if (isExists)
            {
                return false;
            }

            Contact contact = new Contact(contactUsername, contactNick, contactServer, contactOf);
            await context.Contacts.AddAsync(contact);
            await context.SaveChangesAsync();
            return true;
        }

        //Function that update contact
        public async Task<bool> UpdateContact(string contactOf, string contactUsername, string newNick, string newServer)
        {
            using var context = _context;
            var m = await context.Contacts.FindAsync(contactUsername, contactOf);

            if (m == null)
            {
                return false;
            }

            m.ContactNickname = newNick;
            m.Server = newServer;
            await context.SaveChangesAsync();

            return true;
        }

        //Function that delete contact.
        public async Task<bool> DeleteContact(string contactOf, string contactUsername)
        {
            using var context = _context;
            var m = await context.Contacts.FindAsync(contactUsername, contactOf);

            if (m == null)
            {
                return false;
            }

            context.Contacts.Remove(m);
            await context.SaveChangesAsync();

            return true;

        }
    }
}
