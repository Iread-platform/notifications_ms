using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface INotificationRepo
    {
        // public Task<Notification> GetById(string id);
        // public Task<Notification> Add(Notification notification);
        public Task<SingleNotification> SendSingle(SingleNotification notification, int user);
        public Task<TopicNotification> Broadcast(TopicNotification notification);
        public Task<List<SingleNotification>> GetUserNotifications(int user);
        // public List<Notification> GetAll();
        public Task<List<SingleNotification>> GetByDevice(string token);
        // Task<bool> _Exists(string id);
    }
}