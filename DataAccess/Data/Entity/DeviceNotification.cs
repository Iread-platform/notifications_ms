using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("DeviceNotifications")]
    public class DeviceNotification
    {
        [Required]
        public Device Devices { get; set; }
        public string DeviceToken { get; set; }
        public int NotificationId { get; set; }
        [Required]
        public SingleNotification Notifications { get; set; }
    }
}