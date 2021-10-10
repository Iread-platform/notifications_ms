using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface IUserRepo
    {
        public Task<User> AddUser(User User);
        public Task<User> GetUser(string id);
        public Task<User> GetUserByToken(string token);
        public Task<List<User>> GetAllUsers();
        public Task<List<User>> GetUsers(List<string> users);
        public Task<List<User>> GetUsersByTopic(int topicId);
        public bool UserExists(User User);
        public Task<List<Topic>> GetUserTopics(string userId);

    }
}