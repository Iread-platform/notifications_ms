using FirebaseAdmin.Messaging;
using System.Threading.Tasks;
namespace iread_notifications_ms.Web.Service
{

    public interface IFirebaseMessagingService
    {
        Message SetupNotificationMessage(string title, string notificationBody, string token);

        Task<string> sendMessage(string title, string notificationBody, string token);

        void init();

    }
}