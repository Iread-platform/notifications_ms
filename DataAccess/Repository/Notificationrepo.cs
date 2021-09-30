using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{
    class NotificationRepo : INotificationRepo
    {
        private readonly AppDbContext _context;
        public NotificationRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SingleNotification>> GetByDevice(string token)
        {
            return await _context.SingleNotifications.Where((notification) => notification.Token.Equals(token)).ToListAsync();
        }

        public async Task<SingleNotification> SendSingle(SingleNotification notification, int user)
        {
            SingleNotification addedNotification = (await _context.SingleNotifications.AddAsync(notification)).Entity;
            await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return addedNotification;
        }
        public async Task<TopicNotification> Broadcast(TopicNotification notification)
        {
            TopicNotification addedNotification = (await _context.TopicNotifications.AddAsync(notification)).Entity;
            await _context.SaveChangesAsync();
            return addedNotification;

        }
        public async Task<List<SingleNotification>> GetUserNotifications(int user)
        {
            return await _context.SingleNotifications.Where(sn => sn.UserId == user).ToListAsync();
        }

        public async Task<List<TopicNotification>> GetTopicNotifications(int topic)
        {
            return await _context.TopicNotifications.Where(sn => sn.TopicId == topic).ToListAsync();
        }

    }
}