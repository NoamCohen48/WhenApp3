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
        public async Task<User?> Get(string username)
        {
            using var context = _context;
            var user = await context.Users.FindAsync(username);

            /*
            var q = from user in _context.Users
                    where user.Username == Username
                    select user;

            if (q.Count() == 0)
            {
                return null;
            }
            */

            return user;
        }


        public async Task<User?> Add(string username, string password)
        {
            using var context = _context;

            var user = new User(username, password);

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> Add(User user)
        {
            using var context = _context;

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        //delete user - action number 5.
        public async Task<User?> Delete(string username)
        {
            using var context = _context;

            var user = await Get(username);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<bool> Validation(string username, string password)
        {
            var user = await Get(username);

            return user != null && user.Password == password;
        }

    }
}
