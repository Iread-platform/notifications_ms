using System;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;

namespace iread_notifications_ms.Web.Service
{

    public class FirebaseMessagingService : IFirebaseMessagingService
    {
        private FirebaseApp m_app;
        private FirebaseMessaging m_messaging;

        private static string DeviceToken = "fl-IvSmKRmGfX6O9a5hlJ2:APA91bFQyrAP2N0P9glf7zyouZcEZ51Okuy43RTiWndWkiMQLgkZjJ-REIxpA4Cwb0jQWqv_8fZfBU5uI9E9IAVIRxzFsR2H_Y0AY5eO8MfVIDS9HE1oCZd2rfP70H6dihyb6vH60ZCl";

        public void init()
        {
            m_app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("firebase_auth_provider.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            m_messaging = FirebaseMessaging.GetMessaging(m_app);
        }

        public FirebaseMessagingService()
        {
            init();
        }
        public async Task<string> sendMessage(string title, string notificationBody, string token)
        {
            Message message = SetupNotificationMessage(title, notificationBody, token);
            string response = await m_messaging.SendAsync(message);
            return response;
        }

        public Message SetupNotificationMessage(string title, string notificationBody, string token)
        {
            return new Message()
            {
                Token = DeviceToken,
                Notification = new Notification()
                {
                    Body = notificationBody,
                    Title = title
                }
            };
        }
    }
}