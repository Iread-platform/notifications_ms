using System;
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
        [Required]
        public String Title { get; set; }
        public DateTime Created { get; set; }
        public TimeSpan SendAfter { get; set; }

        [Required]
        public String Body
        {
            get; set;
        }
    }

}
