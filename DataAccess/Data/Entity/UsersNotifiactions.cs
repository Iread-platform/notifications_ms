using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("UsersNotifications")]
    public class UsersNotification
    {
        [Required]
        public User Users { get; set; }
        public int UserId { get; set; }
        public int NotificationId { get; set; }
        [Required]
        public SingleNotification Notifications { get; set; }
    }
}