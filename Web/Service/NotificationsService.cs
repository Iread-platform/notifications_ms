using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

using iread_notifications_ms.DataAccess.Repository;
using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public class NotificationService
    {

        private readonly IPublicRepo _publicRepo;

        NotificationService(IPublicRepo publicRepo)
        {
            _publicRepo = publicRepo;
        }
        public async Task<Notification> Sendnotification(Notification notification)
        {
            try
            {
                Notification addedNotification =
            await _publicRepo.NotificationRepo.Add(notification);
                return addedNotification;

            }
            catch (DbUpdateException)
            {

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

