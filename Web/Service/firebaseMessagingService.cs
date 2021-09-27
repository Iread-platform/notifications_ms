using System.Collections.Generic;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.FirebaseCloudMessaging;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;

using Entities = iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public class FirebaseMessagingService : IFirebaseMessagingService
    {
        private FirebaseApp m_app;
        private FirebaseMessaging m_messaging;

        public void init()
        {
            m_app = FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("firebase_auth_provider.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            m_messaging = FirebaseMessaging.GetMessaging(m_app);
        }

        public FirebaseMessagingService()
        {
            init();
        }
        public async Task<string> sendMessage(Entities.SingleNotification notification, Dictionary<string, string> data = null)
        {
            Message message = SetupNotificationMessage(notification, data);
            string response = await m_messaging.SendAsync(message);
            return response;
        }

        public async Task<string> SendTopicNotification(Entities.TopicNotification notification, Dictionary<string, string> data)
        {
            string response = await m_messaging.SendAsync(new Message()
            {
                Notification = new Notification()
                {
                    Body = notification.Body,
                    Title = notification.Title,


                },
                // Data = data,
                Topic = notification.Topic
            });
            return response;
        }

        public async Task<TopicManagementResponse> SubscribeToTopic(List<string> tokens, string topic)
        {
            return await m_messaging.SubscribeToTopicAsync(tokens, topic);
        }

        public async Task<BatchResponse> sendToMnyDevices(List<string> tokens, string title, string body, Priority priority, Dictionary<string, string> data = null, string image = null)
        {
            BatchResponse batchResponse = await m_messaging.SendMulticastAsync(
                new MulticastMessage()
                {
                    Tokens = tokens,
                    Notification = new Notification()
                    { Body = body, Title = title, },

                    Android = new AndroidConfig() { Priority = Priority.High, },
                    // Data = data,
                });
            return batchResponse;
        }

        public Message SetupNotificationMessage(Entities.SingleNotification notification, Dictionary<string, string> data)
        {
            return new Message()
            {
                Token = "dotnet ef migrations add Inheritance",
                Notification = new Notification()
                {
                    Body = notification.Body,
                    Title = notification.Title,


                },
                // Data = data,
            };
        }
    }
}