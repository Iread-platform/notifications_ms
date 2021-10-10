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
        public async Task<bool> TopicExists(int id)
        {
            return await _publicRepo.TopicRepo.TopicExists(id);
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            return await _publicRepo.TopicRepo.GetAllTopics();
        }

        public async Task<Topic> SubscribeToTopic(List<User> users, int topic)
        {

            Topic addedTopic = await _publicRepo.TopicRepo.SubscribeUsers(users, topic);
            return addedTopic;

        }

        public async Task<Topic> SubscribeToTopic(List<User> users, string topicTitle)
        {

            Topic addedTopic = await _publicRepo.TopicRepo.SubscribeUsers(users, topicTitle);
            return addedTopic;

        }
        public async Task<Topic> GetTopic(int id)
        {
            return await _publicRepo.TopicRepo.GetTopic(id);
        }
        public async Task<Topic> GetTopic(string title)
        {
            return await _publicRepo.TopicRepo.GetTopic(title);
        }
        public async Task UnScubscribeUserFromAllTopics(User user)
        {
            await _publicRepo.TopicRepo.UnSubscribeUserFromAllTopics(user);
        }
        public async Task UnScubscribeUserFromTopic(User user, string topicTitle)
        {
            await _publicRepo.TopicRepo.UnSubscribeUserFromTopic(user, topicTitle);
        }

        public Topic DeleteTopic(Topic topic)
        {
            return _publicRepo.TopicRepo.DeleteTopic(topic);
        }
    }
}