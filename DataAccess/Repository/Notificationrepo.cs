using System.Collections.Generic;
using System.Threading.Tasks;
using iread_notifications_ms.DataAccess.Data.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace iread_notifications_ms.DataAccess.Repository
{

    class NotificationRepo : INotificationRepo
    {

        private readonly AppDbContext _context;
        public NotificationRepo(AppDbContext context)
        {
            AppDbContext _context = context;

        }


        public List<Notification> GetAll()
        {
            return _context.Notifications.ToList();

        }

        public async void Add(Notification notification)
        {
            await _context.Notifications.AddAsync(notification);
        }

        public async Task<List<Notification>> GetByDevice(string token)
        {

            return await _context.Notifications.Where((notification) => notification.Token.Equals(token)).ToListAsync();
        }

        public async Task<Notification> GetById(string id)
        {
            return await _context.Notifications.Where((notification) => notification.Id.Equals(id)).FirstAsync();

        }

        public async Task<bool> IsSent(string id)
        {
            return (await _context.Notifications.Where((notification) => notification.Id.Equals(id)).FirstAsync()).IsSent;

        }

        public async Task<bool> _Exists(string id)
        {
            return await _context.Notifications.AnyAsync(notification => notification.Id.Equals(id));
        }
    }
}