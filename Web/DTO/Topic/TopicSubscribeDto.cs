using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO
{
    public class TopicSubscribeDto
    {
        public string TopicTitle { get; set; }
        public List<int> Users { get; set; }
    }
}