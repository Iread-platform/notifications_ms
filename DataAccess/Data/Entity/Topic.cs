using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace iread_notifications_ms.DataAccess.Data.Entity
{

    [Table("Topics")]
    public class Topic
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }


        [RegularExpression(@"[a-zA-Z0-9-_.~%]+")]
        public string Title { get; set; }
        public List<TopicNotification> Notifications { get; set; }
    }
}