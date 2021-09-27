using System.Collections.Generic;
using System.Threading.Tasks;
using iread_notifications_ms.DataAccess.Data.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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


        public async Task<SingleNotification> SendSingle(SingleNotification notification)
        {
            return (await _context.SingleNotifications.AddAsync(notification)).Entity;
        }
        public async Task<BroadcastNotification> Broadcast(BroadcastNotification notification)
        {
            return (await _context.TopicNotification.AddAsync(notification)).Entity;

        }
        public List<Notification> GetAll()
        {
            return _context.Notifications.ToList();

        }

        public async Task<Notification> Add(Notification notification)
        {
            return (await _context.Notifications.AddAsync(notification)).Entity;
        }

        public async Task<Notification> GetById(string id)
        {
            return await _context.Notifications.Where((notification) => notification.Id.Equals(id)).FirstAsync();
        }

        public async Task<bool> _Exists(string id)
        {
            return await _context.Notifications.AnyAsync(notification => notification.Id.Equals(id));
        }
    }
}