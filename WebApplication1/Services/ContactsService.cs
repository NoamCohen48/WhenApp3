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
        public async Task AddContact(string Username, string ContactName)
        {
            _context.Chats.Add(new Chat(Username, ContactName));
            await _context.SaveChangesAsync();
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

        //get user by username and password
        public async Task<User?> Get(string Username, string Password)
        {

            return await _context.Users.FindAsync(Username, Password);
        }

        public async Task<bool> Validation(string Username, string Password)
        {
            var user = await Get(Username, Password);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        //update user - action number 4.
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
