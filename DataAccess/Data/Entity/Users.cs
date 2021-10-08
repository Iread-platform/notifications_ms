using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace iread_notifications_ms.DataAccess.Data.Entity
{

    [Table("Users")]
    public class User

    {
        [Required]
        public string Token { get; set; }
        [Key]
        [Required]
        public string UserId { get; set; }
        public ICollection<Topic> Topics { get; set; }
        public ICollection<SingleNotification> Notifications { get; set; }
    }
}