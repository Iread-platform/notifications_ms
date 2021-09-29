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

        public DbSet<User> Users
        {
            get; set;
        }

        public DbSet<UsersNotification> DeviceNotifications
        {
            get; set;
        }
        public DbSet<TopicUsers> TopicUsers
        {
            get; set;
        }

        public DbSet<Topic> Topics
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UsersNotification>().HasKey(dn => new { dn.UserId, dn.NotificationId });
            modelBuilder.Entity<UsersNotification>().HasOne(dn => dn.Users).WithMany(d => d.UsersNotifications).HasForeignKey(dn => dn.UserId);
            modelBuilder.Entity<UsersNotification>().HasOne(dn => dn.Notifications).WithMany(n => n.UsersNotifications).HasForeignKey(dn => dn.NotificationId);
            modelBuilder.Entity<TopicUsers>().HasKey(topicUser => new { topicUser.UserId, topicUser.TopicId });
            modelBuilder.Entity<TopicUsers>().HasOne(tu => tu.Users).WithMany(d => d.UserTopics).HasForeignKey(tu => tu.UserId);
            modelBuilder.Entity<TopicUsers>().HasOne(tu => tu.Topics).WithMany(t => t.TopicUsers).HasForeignKey(tu => tu.TopicId); ;
            modelBuilder.Entity<Topic>().HasMany(topic => topic.Notifications).WithOne(notification => notification.Topic);
            modelBuilder.Entity<User>().HasIndex(u => u.Token).IsUnique(true);
        }

    }


}