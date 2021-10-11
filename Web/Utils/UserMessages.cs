using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iread_notifications_ms.Web.Utils
{

    public class UserMessages
    {
        // Topic messages.
        public const string TOPIC_TITLE_EXISTS = "A topic with the same title already exists.";
        public const string TOPIC_IVALID_NAME = "Invalid topic name, Allowed characters: a-z/A-Z, numbers: 0-9, and . - % .";
        public const string TOPIC_NO_SUBSCRIBERS = "This topic has no subscribers.";
        public const string TOPIC_NO_NOTIFICATIONS = "This topic has no notifications.";
        public const string TOPIC_NO_FOUND = "There is no topic with such info.";

        // Notification messages.
        public const string NOTIFICATION_NOT_SENT = "Could  not deliver this notification.";
        public const string NOTIFICATION_SENT = "Notification was sent successfully.";

        //User messages
        public const string USER_NO_NOTIFICATIONS = "This user has no notifications.";
        public const string USER_NO_FOUND = "There is no user with such info.";
        public const string FILE_EXTENSION_NOT_ALLOWED = "File Extension is not allowed";
        public const string TOKEN_EXISTS = "This token is used by another user.";
        public static List<String> ModelStateParser(ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList();
        }
    }
}