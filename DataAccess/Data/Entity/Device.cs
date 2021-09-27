using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace iread_notifications_ms.DataAccess.Data.Entity
{

    [Table("Devices")]
    public class Device

    {
        [Key]
        [Required]
        public string Token { get; set; }
        [Required]
        public int UserId { get; set; }

        public List<DeviceNotification> DeviceNotifications { get; set; }
    }
}