using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Repository;
using iread_notifications_ms.DataAccess.Data.Entity;
using iread_notifications_ms.Web.DTO;

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

        public async Task<List<TopicUsers>> SubscribeToTopic(TopicSubscribeDto topicSubscribeDto)
        {
            List<Device> devices = await _publicRepo.DeviceRepo.GetUsersDevices(topicSubscribeDto.Users);
            if (devices.Count > 0)
            {
                List<TopicUsers> topicUsers = await _publicRepo.TopicRepo.SubscribeDevices(devices, topicSubscribeDto.TopId);
                return topicUsers;
            }

            return null;
        }

    }
}