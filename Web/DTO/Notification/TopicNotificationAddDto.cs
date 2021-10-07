using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO
{
    public class TopicNotificationAddDto
    {
        public string Title { get; set; }

        public string Body
        {
            get; set;
        }
        public string TopicName { get; set; }

        public ExtraDataDto ExtraData { get; set; }

    }
}