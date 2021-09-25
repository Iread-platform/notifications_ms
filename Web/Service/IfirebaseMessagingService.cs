using FirebaseAdmin.Messaging;
using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public interface IFirebaseMessagingService
    {
        Message SetupNotificationMessage(SingleNotification notification, Dictionary<string, string> data);

        Task<string> sendMessage(SingleNotification notification, Dictionary<string, string> data);
        Task<string> SendBoradcast(BroadcastNotification notification, Dictionary<string, string> data);

        Task<TopicManagementResponse> SubscribeToTopic(List<string> tokens, string topic);
        void init();

    }
}