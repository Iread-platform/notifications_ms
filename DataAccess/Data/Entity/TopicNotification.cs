using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("TopicNotifications")]
    public class TopicNotification : Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TopicNotificationId { get; set; }
        public Topic Topic { get; set; }
        public int TopicId { get; set; }
    }

}