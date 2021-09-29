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
        private UserRepo _userRepo;
        private TopicRepo _TopicRepo;
        public INotificationRepo NotificationRepo => _notificationRepo ??= new NotificationRepo(_context);
        public IUserRepo UserRepo => _userRepo ??= new UserRepo(_context);
        public ITopicRepo TopicRepo => _TopicRepo ??= new TopicRepo(_context);
    }
}