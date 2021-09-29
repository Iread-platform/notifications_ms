using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iread_notifications_ms.Web.Utils
{

    public class UserMessages
    {
        // Topic messages.
        public static string TOPIC_TITLE_EXISTS = "A topic with the same title already exists.";
        public static string TOPIC_NO_SUBSCRIBERS = "This topic has no subscribers.";
        public static string TOPIC_NO_NOTIFICATIONS = "This topic has no notifications.";
        public static string TOPIC_NO_FOUND = "There is no topic with such info.";

        // Notification messages.
        public static string NOTIFICATION_NOT_SENT = "Could  not deliver this notification.";

        //User messages
        public static string USER_NO_NOTIFICATIONS = "This user has no notifications.";
        public static string USER_NO_FOUND = "There is no user with such info.";
        public static List<String> ModelStateParser(ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList();
        }
    }
}