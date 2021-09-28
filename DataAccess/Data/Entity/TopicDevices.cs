using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace iread_notifications_ms.DataAccess.Data.Entity
{
    public class TopicUsers
    {
        [Required]
        public Device Devices { get; set; }
        public string Token { get; set; }
        public int TopicId { get; set; }
        [Required]
        public Topic Topics { get; set; }
    }
}