using System.Collections.Generic;
using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.FirebaseCloudMessaging;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;
using System;
using System.Text.Json;

using Entities = iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public class FirebaseMessagingService : IFirebaseMessagingService
    {
        private FirebaseApp m_app;
        private FirebaseMessaging m_messaging;

        public void init()
        {
            m_app ??= FirebaseApp.Create(new AppOptions() { Credential = GoogleCredential.FromFile("firebase_auth_provider.json").CreateScoped("https://www.googleapis.com/auth/firebase.messaging") });
            m_messaging ??= FirebaseMessaging.GetMessaging(m_app);
        }

        public FirebaseMessagingService()
        {
            init();
        }
        public async Task<bool> sendMessage(Entities.SingleNotification notification, Dictionary<string, string> data = null)
        {
            Message message = SetupNotificationMessage(notification, data);
            try
            {
                string response = await m_messaging.SendAsync(message);
                return response != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendTopicNotification(Entities.TopicNotification notification, Dictionary<string, string> data)
        {
            try
            {
                string response = await m_messaging.SendAsync(new Message()
                {
                    Notification = new Notification()
                    {
                        Body = notification.Body,
                        Title = notification.Title,


                    },
                    Data = JsonSerializer.Deserialize<Dictionary<string, string>>(notification.ExtraData),
                    Topic = notification.Topic.Title
                });
                return response != null;

            }
            catch (Exception)
            {

            }
            return false;
        }

        public async Task<TopicManagementResponse> SubscribeToTopic(List<string> tokens, string topic)
        {
            return await m_messaging.SubscribeToTopicAsync(tokens, topic);
        }

        public async Task<TopicManagementResponse> UnSubscribeToTopic(List<string> tokens, string topic)
        {
            return await m_messaging.UnsubscribeFromTopicAsync(tokens, topic);
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

        private Message SetupNotificationMessage(Entities.SingleNotification notification, Dictionary<string, string> data)
        {
            return new Message()
            {
                Token = notification.Token,
                Notification = new Notification()
                {
                    Body = notification.Body,
                    Title = notification.Title,
                },
                Data = JsonSerializer.Deserialize<Dictionary<string, string>>(notification.ExtraData)
            };
        }
        private string exceptionHandler(FirebaseException e)
        {
            if (e is FirebaseMessagingException)
            {
                switch ((e as FirebaseMessagingException).MessagingErrorCode)
                {
                    case MessagingErrorCode.Internal:
                        {
                            return "Internal server error";
                        }
                    case MessagingErrorCode.Unavailable:
                        {
                            return "Messaging serves is not available";
                        }
                    case MessagingErrorCode.SenderIdMismatch:
                        {
                            return "Sender id and registered id do not match.";
                        }
                    case MessagingErrorCode.Unregistered:
                        {
                            return "Unregistered device token.";
                        }
                    default:
                        return "Firebase internal error";

                }
            }
            switch (e.ErrorCode)
            {
                case ErrorCode.InvalidArgument:
                    {
                        return "Invalid request arguments.";
                    }
                case ErrorCode.NotFound:
                    {
                        return "Requested records are not found.";
                    }

                default: return "Unknown error.";
            }
        }
    }
}