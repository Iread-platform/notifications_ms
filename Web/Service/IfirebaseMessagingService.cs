using FirebaseAdmin.Messaging;
using System.Threading.Tasks;
using System.Collections.Generic;
using NotificationEntity = iread_notifications_ms.DataAccess.Data.Entity.Notification;

namespace iread_notifications_ms.Web.Service
{

    public interface IFirebaseMessagingService
    {
        Message SetupNotificationMessage(NotificationEntity notification, Dictionary<string, string> data);

        Task<string> sendMessage(NotificationEntity notification, Dictionary<string, string> data);

        void init();

    }
}