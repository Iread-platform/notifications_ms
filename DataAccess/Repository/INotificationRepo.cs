using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface INotificationRepo
    {
        public Task<SingleNotification> SendSingle(SingleNotification notification, string user);
        public Task<TopicNotification> Broadcast(TopicNotification notification);
        public Task<List<SingleNotification>> GetUserNotifications(string user);
        public Task<List<TopicNotification>> GetTopicNotifications(int topic);
        public Task<List<SingleNotification>> GetByDevice(string token);
    }
}