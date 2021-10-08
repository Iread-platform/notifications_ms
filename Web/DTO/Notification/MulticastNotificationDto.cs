using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO
{
    public class MulticastNotificationDTo
    {
        public string Title { get; set; }

        public string Body
        {
            get; set;
        }
        public List<string> Users;
    }
}