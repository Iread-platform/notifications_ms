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

        public async Task<bool> TopicExists(int topic)
        {
            return (await _context.Topics.FindAsync(topic)) != null;
        }

        public async Task<Topic> SubscribeUsers(List<User> users, int topicId)
        {
            if (_context.Topics.Where(t => t.TopicId == topicId).Count() > 0)
            {

                Topic topic = await _context.Topics.FindAsync(topicId);
                topic.Users ??= new List<User>();
                users.AddRange(topic.Users);
                topic.Users = users;
                _context.Topics.Update(topic);
                await _context.SaveChangesAsync();
                return topic;
            }
            return null;
        }

        public async Task<Topic> SubscribeUsers(List<User> users, string topicTitle)
        {
            Topic topic = await _context.Topics.Include(t => t.Users).Where(t => t.Title == topicTitle).FirstOrDefaultAsync();
            if (topic != null)
            {
                if (topic.Users == null)
                {
                    topic.Users = new List<User>();
                }
                foreach (User user in users)
                {
                    topic.Users.Add(user);
                }
                _context.Topics.Update(topic);
                await _context.SaveChangesAsync();
                return topic;
            }
            return null;
        }

        public async Task<Topic> GetTopic(int id)
        {
            return await _context.Topics.Include(t => t.Users).FirstOrDefaultAsync(t => t.TopicId == id);
        }

        public async Task<Topic> GetTopic(string title)
        {
            return await _context.Topics.Include(t => t.Users).FirstOrDefaultAsync(t => t.Title == title);
        }
        public async Task UnSubscribeUserFromAllTopics(User user)
        {
            List<Topic> userTopics = await _context.Topics.Include(t => t.Users).Where(t => t.Users.Contains(user)).ToListAsync();
            if (userTopics.Count > 0)
            {
                foreach (Topic topic in userTopics)
                {
                    topic.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public async Task UnSubscribeUserFromTopic(User user, string topicTitle)
        {
            List<Topic> userTopics = await _context.Topics.Include(t => t.Users).Where(t => t.Title.Equals(topicTitle)).ToListAsync();

            if (userTopics.Count > 0)
            {
                foreach (Topic topic in userTopics)
                {
                    topic.Users?.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
        }
        public Topic DeleteTopic(Topic topic)
        {
            Topic deletedTopic = _context.Topics.Remove(topic).Entity;
            _context.SaveChanges();
            return deletedTopic;
        }


    }
}