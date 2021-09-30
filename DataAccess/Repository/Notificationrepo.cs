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
            // await _context.DeviceNotifications.AddAsync(new UsersNotification()
            // {
            //     NotificationId = addedNotification.Id,
            //     UserId = user
            // });
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
            return (await _context.Users.FindAsync(user)).Notifications.ToList();
        }

        // public List<Notification> GetAll()
        // {
        //     return _context.Notifications.ToList();

        // }

        // public async Task<Notification> Add(Notification notification)
        // {
        //     Notification AddedNotification = (await _context.Notifications.AddAsync(notification)).Entity;
        //     await _context.SaveChangesAsync();
        //     return AddedNotification;
        // }

        // public async Task<Notification> GetById(string id)
        // {
        //     return await _context.Notifications.FindAsync(id);
        // }

        // public async Task<bool> _Exists(string id)
        // {
        //     return await _context.Notifications.AnyAsync(notification => notification.Id.Equals(id));
        // }
    }
}