using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

using iread_notifications_ms.DataAccess.Repository;
using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Web.Service
{

    public class NotificationService
    {

        private readonly IPublicRepo _publicRepo;

        public NotificationService(IPublicRepo publicRepo)
        {
            _publicRepo = publicRepo;
        }

        public async Task<List<SingleNotification>> GetUserNotifications(int userId)
        {
            return await _publicRepo.NotificationRepo.GetUserNotifications(userId);
        }

        public async Task<Notification> SendNotification(Notification notification, int user)
        {
            try
            {
                Notification addedNotification;
                if (notification is TopicNotification)
                {

                    addedNotification = await _publicRepo.NotificationRepo.Broadcast(notification as TopicNotification);
                }
                else
                {
                    addedNotification = await _publicRepo.NotificationRepo.SendSingle(notification as SingleNotification, user);

                }

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

