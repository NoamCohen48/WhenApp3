using whenAppModel.Models;
using WhenUp;

namespace whenAppModel.Services
{

    public class UsersService : IUsersService
    {
        private readonly WhenAppContext _context;

        public UsersService(WhenAppContext context)
        {
            _context = context;
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
        //delete user - action number 5.
        public async Task Delete(string UserName)
        {
            //var user = await Get(UserName);
            //_context.Users.Remove(user);
            //await _context.SaveChangesAsync();
        }
        
    }
}
