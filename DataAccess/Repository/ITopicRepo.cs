using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface ITopicRepo
    {
        public Task<Topic> AddTopic(Topic topic);
        public Task<Topic> GetTopic(int id);
        public Task<List<Topic>> GetAllTopics();
        public Task<Topic> SubscribeUsers(List<User> users, int topicId);
        public bool TopicExists(Topic topic);
        public Task<bool> TopicExists(int id);

    }
}