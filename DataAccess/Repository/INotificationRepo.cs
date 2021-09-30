using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface INotificationRepo
    {
        public Task<SingleNotification> SendSingle(SingleNotification notification, int user);
        public Task<TopicNotification> Broadcast(TopicNotification notification);
        public Task<List<SingleNotification>> GetUserNotifications(int user);
        public Task<List<SingleNotification>> GetByDevice(string token);
    }
}