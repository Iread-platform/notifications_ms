using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface IUserRepo
    {
        public Task<User> AddUser(User User);
        public Task<User> GetUser(int id);
        public Task<List<User>> GetAllUsers();
        public Task<List<User>> GetUsers(List<int> users);
        public Task<List<User>> GetUsersByTopic(int topicId);
        public bool UserExists(User User);

    }
}