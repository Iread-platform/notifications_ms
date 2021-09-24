using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO.Notification
{
    public class NotificationDto
    {
        public string Title { get; set; }

        public string Body
        {
            get; set;
        }
        public Dictionary<string, string> data { get; set; }
    }
}