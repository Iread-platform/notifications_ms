using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("BroadcastNotifications")]
    public class BroadcastNotification : Notification
    {
        public string Topic { get; set; }
    }

}