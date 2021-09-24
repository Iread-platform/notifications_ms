using Microsoft.EntityFrameworkCore;

using iread_notifications_ms.DataAccess.Data.Entity;
namespace iread_notifications_ms.DataAccess
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Notification> Notifications
        {
            get; set;

        }
    }
}