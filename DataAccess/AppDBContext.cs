using Microsoft.EntityFrameworkCore;

using iread_notifications_ms.DataAccess.Data.Entity;
namespace iread_notifications_ms.DataAccess
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<SingleNotification> SingleNotifications
        {
            get; set;
        }

        public DbSet<Notification> Notifications
        {
            get; set;
        }
        public DbSet<TopicNotification> TopicNotifications
        {
            get; set;
        }

        public DbSet<Device> Devices
        {
            get; set;
        }

        public DbSet<DeviceNotification> DeviceNotifications
        {
            get; set;
        }

        public DbSet<Topic> Topics
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<DeviceNotification>().HasKey(ES => new { ES.DeviceToken, ES.NotificationId });
            modelBuilder.Entity<Topic>().HasMany(topic => topic.Notifications).WithOne(notification => notification.Topic);
        }

    }


}