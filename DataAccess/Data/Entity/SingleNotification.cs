using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("SingleNotifications")]
    public class SingleNotification : Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int SingleNotificationId { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        // public List<UsersNotification> UsersNotifications { get; set; }

    }

}