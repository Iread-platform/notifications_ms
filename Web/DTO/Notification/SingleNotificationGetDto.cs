using System;

namespace iread_notifications_ms.Web.DTO
{
    public class SingletNotificationGetDto
    {
        public string Title { get; set; }

        public string Body
        {
            get; set;
        }
        public string UserId { get; set; }
        public DateTime created { get; set; }
        public TimeSpan SendAfter { get; set; }

    }
}