using System.Collections.Generic;

namespace iread_notifications_ms.Web.DTO
{
    public class AddDeviceDto
    {
        public int UserId { get; set; }

        public string Token
        {
            get; set;
        }
    }
}