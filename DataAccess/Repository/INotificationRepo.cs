using System.Threading.Tasks;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.DataAccess.Repository
{

    public interface INotificationRepo
    {
        public Task<Notification> GetById(string id);
        public void Add(Notification notification);
        public List<Notification> GetAll();
        public Task<List<Notification>> GetByDevice(string token);
        public Task<bool> IsSent(string id);
        Task<bool> _Exists(string id);
    }
}