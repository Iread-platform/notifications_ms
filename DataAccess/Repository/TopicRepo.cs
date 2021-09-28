using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{
    public class TopicRepo : ITopicRepo
    {
        private readonly AppDbContext _context;
        public TopicRepo(AppDbContext context)
        {
            _context = context;

        }

        public async Task<Topic> AddTopic(Topic topic)
        {
            if (TopicExists(topic))
            {
                return null;
            }
            Topic addedTopic = (await _context.Topics.AddAsync(topic)).Entity;
            await _context.SaveChangesAsync();
            return addedTopic;
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            return await _context.Topics.ToListAsync();
        }

        public bool TopicExists(Topic topic)
        {
            return _context.Topics.Where(t => t.Title.Equals(topic.Title)).Count() > 0;
        }
    }
}