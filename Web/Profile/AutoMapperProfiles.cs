using iread_notifications_ms.DataAccess.Data.Entity;
using iread_notifications_ms.Web.DTO;

namespace iread_notifications_ms.Web.Profile
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SingleNotification, SingletNotificationDto>().ReverseMap();
            CreateMap<BroadcastNotification, BroadcastNotificationDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();

        }
    }
}