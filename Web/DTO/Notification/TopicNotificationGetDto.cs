using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO
{
    public class TopicNotificationGetDto
    {
        public string Title { get; set; }

        public string Body
        {
            get; set;
        }
        public int TopicNotificationId
        {
            get; set;
        }
    }
}