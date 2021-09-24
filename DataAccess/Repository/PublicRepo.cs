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
        public INotificationRepo NotificationRepo => _notificationRepo ??= new NotificationRepo(_context);
    }
}