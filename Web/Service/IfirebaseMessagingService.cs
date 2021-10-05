using FirebaseAdmin.Messaging;
using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public interface IFirebaseMessagingService
    {
        Task<bool> sendMessage(SingleNotification notification, Dictionary<string, string> data = null);
        Task<bool> SendTopicNotification(TopicNotification notification, Dictionary<string, string> data);

        Task<TopicManagementResponse> UnSubscribeToTopic(List<string> tokens, string topic);

        Task<TopicManagementResponse> SubscribeToTopic(List<string> tokens, string topic);
        void init();

    }
}