using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO
{
    public class TopicSubscribeDto
    {
        public int TopId { get; set; }
        public List<int> Users { get; set; }
    }
}