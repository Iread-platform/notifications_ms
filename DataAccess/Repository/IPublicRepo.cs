namespace iread_notifications_ms.DataAccess.Repository
{
    public interface IPublicRepo
    {

        INotificationRepo NotificationRepo { get; }
        IDeviceRepo DeviceRepo { get; }

    }
}