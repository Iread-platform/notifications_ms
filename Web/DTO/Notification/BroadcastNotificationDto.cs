using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO
{
    public class BroadcastNotificationDto
    {
        public string Title { get; set; }

        public string Body
        {
            get; set;
        }
        public string Topic { get; set; }
    }
}