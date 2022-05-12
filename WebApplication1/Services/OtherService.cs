using whenAppModel.Models;
using WhenUp;

namespace WebApplication1.Services
{
    public class OtherService
    {
        private readonly WhenAppContext _context;

        public OtherService(WhenAppContext context)
        {
            _context = context;
        }

        //register
        public async Task<User?> Add(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }


    }
}
