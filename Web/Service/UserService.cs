using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Repository;
using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public class UserService
    {
        private readonly IPublicRepo _publicRepo;

        public UserService(IPublicRepo publicRepo)
        {
            _publicRepo = publicRepo;
        }

        public async Task<User> AddUser(User User)
        {
            return await _publicRepo.UserRepo.AddUser(User);
        }
        public async Task<List<User>> GetUsers(List<string> users)
        {
            return await _publicRepo.UserRepo.GetUsers(users);
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _publicRepo.UserRepo.GetAllUsers();
        }
        public async Task<User> GetUser(string id)
        {
            return await _publicRepo.UserRepo.GetUser(id);
        }
        public async Task<List<User>> GetUserByTopic(int id)
        {
            return await _publicRepo.UserRepo.GetUsersByTopic(id);
        }

        public async Task<List<Topic>> GetUserTopics(string userId)
        {
            return await _publicRepo.UserRepo.GetUserTopics(userId);
        }

        public bool ExactUserExists(User user)
        {
            return _publicRepo.UserRepo.UserExists(user);
        }
        public async Task<bool> UserExists(string id)
        {
            return await _publicRepo.UserRepo.GetUser(id) != null;
        }
        public async Task<User> GetUserByToken(string token)
        {
            return await _publicRepo.UserRepo.GetUserByToken(token);
        }

    }
}