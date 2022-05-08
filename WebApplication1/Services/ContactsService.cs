using Microsoft.AspNetCore.Mvc;
using whenAppModel.Models;
using whenAppModel.Services;
using WhenUp;

namespace whenAppModel.Services
{
    public class ContactsService : IContactsService
    {

        private readonly WhenAppContext _context;

        public ContactsService(WhenAppContext context)
        {
            _context = context;
        }
        public async Task<User?> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //add contact to user.
        public async Task<User?> AddContact(string Username, string ContactName)
        {
            var user = await Get(Username);
            var contact = await Get(ContactName);

            if (user != null && contact != null)
            {
                if (!user.Contacts.Contains(contact))
                {
                    user.Contacts.Add(contact);
                    await _context.SaveChangesAsync();
                    return user;
                }
            }
            return null;

        }

        public async Task<User?> Delete(string UserName)
        {
            var user = Get(UserName);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return null;
        }

        //Get user by his username.
        public async Task<User?> Get(string Username)
        {
            return await _context.Users.FindAsync(Username);
        }

        //get user bu username and password.
        public async Task<User?> Get(string Username, string Password)
        {
            return await _context.Users.FindAsync(Username, Password);
        }

        //return all user contacts.
        public async Task<List<User>?> GetAllContacts(string Username)
        {
            var user = await Get(Username);
            return user?.Contacts.ToList();
        }

        public async Task<User?> Update(User NewUser, string OldUserUserName)
        {
            var OldUser = await Get(OldUserUserName);
            if (OldUser != null)
            {
                await Delete(OldUserUserName);
                await Add(NewUser);
                await _context.SaveChangesAsync();
                return NewUser;
            }
            return null;
        }
    }
}
