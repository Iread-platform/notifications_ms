namespace iread_notifications_ms.DataAccess.Repository
{
    public class PublicRepo : IPublicRepo
    {
        private readonly AppDbContext _context;
        public PublicRepo(AppDbContext context)
        {
            _context = context;
        }
        private NotificationRepo _notificationRepo;
        private DeviceRepo _DeviceRepo;
        private TopicRepo _TopicRepo;
        public INotificationRepo NotificationRepo => _notificationRepo ??= new NotificationRepo(_context);
        public IDeviceRepo DeviceRepo => _DeviceRepo ??= new DeviceRepo(_context);
        public ITopicRepo TopicRepo => _TopicRepo ??= new TopicRepo(_context);
    }
}