using FirebaseAdmin.Messaging;
using System.Threading.Tasks;
namespace iread_notifications_ms.Web.Service
{

    public interface IFirebaseMessagingService
    {
        void InitFirebaseApp();
        Message SetupNotificationMessage(string title, string notificationBody, string token);

        Task<string> sendMessage(string title, string notificationBody, string token);
    }
}