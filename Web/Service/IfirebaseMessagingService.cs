using FirebaseAdmin.Messaging;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace iread_notifications_ms.Web.Service
{

    public interface IFirebaseMessagingService
    {
        Message SetupNotificationMessage(string title, string notificationBody, string token, Dictionary<string, string> data);

        Task<string> sendMessage(string title, string notificationBody, string token, Dictionary<string, string> data);

        void init();

    }
}