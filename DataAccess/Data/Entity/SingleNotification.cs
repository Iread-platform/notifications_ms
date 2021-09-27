using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("SingleNotifications")]
    public class SingleNotification : Notification
    {
        public string Token { get; set; }
        public List<DeviceNotification> DeviceNotifications { get; set; }
    }

}