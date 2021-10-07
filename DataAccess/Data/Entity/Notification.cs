using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;



namespace iread_notifications_ms.DataAccess.Data.Entity
{
    public abstract class Notification
    {

        [Required]
        public String Title { get; set; }
        public DateTime Created { get; set; }
        public TimeSpan SendAfter { get; set; }

        [Required]
        public String Body
        {
            get; set;
        }
        public string ExtraData
        {
            get; set;
        }

    }
}
