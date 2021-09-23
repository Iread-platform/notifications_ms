using System;
using System.Collections.Generic;

namespace iread_notifications_ms.DataAccess.Data.Entity
{
    public class Notification
    {
        public String Title { get; set; }

        public String Body
        {
            get; set;
        }
        public bool IsSent { get; set; }

        public Dictionary<String, string> data { get; set; }
    }

}
