using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("TopicNotification")]
    public class TopicNotification : Notification
    {
        public Topic Topic { get; set; }
    }

}