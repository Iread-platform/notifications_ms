using System;
using System.Collections.Generic;

namespace iread_notifications_ms.DataAccess.Data.Entity
{
    public class Notification
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Token { get; set; }

        public String Body
        {
            get; set;
        }
        public bool IsSent { get; set; }

        public Dictionary<String, String> data { get; set; }
    }

}
