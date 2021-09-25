using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace iread_notifications_ms.DataAccess.Data.Entity
{
    [Table("Notifications")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        public String Title { get; set; }
        public DateTime Created { get; set; }

        public String Body
        {
            get; set;
        }
        public bool IsSent { get; set; }
    }

}
