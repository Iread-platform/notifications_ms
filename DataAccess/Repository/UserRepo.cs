using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context)
        {
            _context = context;

        }

        public async Task<User> AddUser(User User)
        {
            if (UserExists(User))
            {
                return null;
            }
            User addedUser = (await _context.Users.AddAsync(User)).Entity;
            await _context.SaveChangesAsync();
            return addedUser;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> GetUsers(List<int> users)
        {
            return await _context.Users.Where(u => users.Contains(u.UserId)).ToListAsync();
        }

        public bool UserExists(User User)
        {
            return _context.Users.Where(d => d.Token.Equals(User.Token)).Count() > 0;
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.Where(d => d.UserId == id).SingleOrDefaultAsync();
        }
    }
}