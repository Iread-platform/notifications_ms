using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Repository;
using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public class TopicService
    {
        private readonly IPublicRepo _publicRepo;

        public TopicService(IPublicRepo publicRepo)
        {
            _publicRepo = publicRepo;
        }

        public async Task<Topic> AddTopic(Topic Topic)
        {
            return await _publicRepo.TopicRepo.AddTopic(Topic);
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            return await _publicRepo.TopicRepo.GetAllTopics();
        }

    }
}