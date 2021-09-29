using System.ComponentModel.DataAnnotations;

using iread_notifications_ms.Web.Utils;

namespace iread_notifications_ms.Web.DTO
{
    public class AddTopicDto
    {

        [RegularExpression(@"[a-zA-Z0-9-_.~%]+",
                ErrorMessage = UserMessages.TOPIC_IVALID_NAME)]
        public string Title { get; set; }
    }
}