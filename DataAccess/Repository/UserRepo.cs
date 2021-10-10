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

        public async Task<List<User>> GetUsers(List<string> users)
        {
            return await _context.Users.Where(u => users.Contains(u.UserId)).ToListAsync();
        }

        public async Task<List<User>> GetUsersByTopic(int topicId)
        {
            Topic topic = await _context.Topics.Include(t => t.Users).Where(t => t.TopicId == topicId).FirstOrDefaultAsync();
            if (topic == null)
            {
                return null;
            }
            return topic.Users.ToList();
        }

        public bool UserExists(User User)
        {
            return _context.Users.Where(d => d.UserId.Equals(User.UserId)).Count() > 0;
        }
        public async Task<User> GetUser(string id)
        {
            return await _context.Users.Where(d => d.UserId.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<List<Topic>> GetUserTopics(string userId)
        {
            User user = await _context.Users.Include(u => u.Topics).Where(u => u.UserId.Equals(userId)).FirstOrDefaultAsync();
            return user.Topics.ToList();
        }
        public async Task<User> GetUserByToken(string token)
        {
            return await _context.Users.Where(d => d.Token.Equals(token)).SingleOrDefaultAsync();

        }

    }
}