using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
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

        //return all user contacts - action number 1.
        public async Task<ICollection<User>?> GetAllContacts(string Username)
        {
            var q1 = _context.Chats.Where(chat => chat.Person1 == Username)
                .Select(chat => chat.Person1);
            var q2 = _context.Chats.Where(chat => chat.Person2 == Username)
                .Select(chat => chat.Person2);
            var users = await q1.Union(q2).ToListAsync();
            return await _context.Users.Where(User => users.Contains(User.Username)).ToListAsync();
        }

        //add contact to user - action number 2.
        public async Task AddContact(User currentUser, string contactUserName, string contactNickName, string contactServer)
        {
            var contactUser = Get(contactUserName);
            if (contactUser != null)
            {
                //await _context.Chats.Add(currentUser, contactUser, "txt", "time");
            }
        }

        //TO-DO: Function that return the user in contact format(id, name, server, last, lastdate)
        public async Task<User?> GetContact(string Username)
        {
            return null;
        }

        //Get user by his username - action number 3.
        public async Task<User?> Get(string Username)
        {

            var q = from user in _context.Users
                    where user.Username == Username
                    select user;

            if (q.Count() == 0)
            {
                return null;
            }

            return q.First();
        }
        public async Task<User?> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //TO-DO: Function to add new contact
        public async Task<User?> AddContact(User user)
        {
            //_context.Chats.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> Validation(string Username, string Password)
        {
            var user = await Get(Username);
            if (user != null && user.Password == Password)
            {
                return true;
            }
            return false;
        }

        //update user - action number 4.
        public async Task<User?> UpdateContact(User NewUser, string OldUserUserName)
        {
            var OldUser = await Get(OldUserUserName);
            if (OldUser != null)
            {
                await DeleteContact(OldUserUserName);
                await AddContact(NewUser);
                await _context.SaveChangesAsync();
                return NewUser;
            }
            return null;
        }


        //TO-DO: Function that delete contact.
        public async Task DeleteContact(string UserName)
        {
            var q1 = _context.Chats.Where(chat => chat.Person1 == UserName)
               .Select(chat => chat.Person1);
            var q2 = _context.Chats.Where(chat => chat.Person2 == UserName)
                .Select(chat => chat.Person2);
            await _context.SaveChangesAsync();

        }


        //delete user - action number 5.
        public async Task Delete(string UserName)
        {
            var q1 = _context.Chats.Where(chat => chat.Person1 == UserName)
               .Select(chat => chat.Person1); 
            var q2 = _context.Chats.Where(chat => chat.Person2 == UserName)
                .Select(chat => chat.Person2);

            //var user = await Get(UserName);
            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
        }
        
    }
}
