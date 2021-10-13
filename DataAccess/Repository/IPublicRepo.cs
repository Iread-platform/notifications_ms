namespace iread_notifications_ms.DataAccess.Repository
{
    public interface IPublicRepo
    {

        INotificationRepo NotificationRepo { get; }
        IUserRepo UserRepo { get; }
        ITopicRepo TopicRepo { get; }

    }
}