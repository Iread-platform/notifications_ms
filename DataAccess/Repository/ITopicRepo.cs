using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface ITopicRepo
    {
        public Task<Topic> AddTopic(Topic topic);
        public Task<List<Topic>> GetAllTopics();
        public bool TopicExists(Topic topic);

    }
}