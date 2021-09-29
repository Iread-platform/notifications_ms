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
        public async Task<List<User>> GetUsers(List<int> users)
        {
            return await _publicRepo.UserRepo.GetUsers(users);
        }


        public async Task<List<User>> GetAllUsers()
        {
            return await _publicRepo.UserRepo.GetAllUsers();
        }
        public async Task<User> GetUser(int id)
        {
            return await _publicRepo.UserRepo.GetUser(id);
        }
    }
}