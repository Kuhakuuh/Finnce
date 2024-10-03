using Finnce_Api.core.Notifications;
using Finnce_Api.Models.NotificationDto;

namespace Finnce_Api.Services
{
    public class NotificationService
    {
        private readonly RepositoryContext context;
        public NotificationService(RepositoryContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Get all notifications form DB
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Notification> GetAllNotifications()
        {
            if (this.context.Entities != null)
            {
                var notifications = this.context.Notifications.Select(notification => new Notification
                {
                    Id = notification.Id,
                    Message = notification.Message,
                    IdUser = notification.IdUser,
                    Name = notification.Name
                }).ToList();
                return notifications;
            }
            return null;
        }

        /// <summary>
        /// Return the notification list of the specific id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Notification>> GetAllNotificationsForUser(string userId)
        {
            var notificationList = context.Notifications
                .Where(t => t.IdUser == userId)
                .ToList();
            if (notificationList != null)
            {
                return notificationList;
            }

            return null;
        }

        /// <summary>
        /// Create a new Notification in DB
        /// </summary>
        /// <param name="notificationModel"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Notification CreateNotification(NotificationModel notificationModel, String idUser)
        {
            if (notificationModel == null)
            {
                throw new ArgumentNullException(nameof(notificationModel));
            }
            var userById = context.Users.FirstOrDefault(user => user.Id == notificationModel.IdUser);
            var newNotification = new Notification
            {
                Name = notificationModel.Name,
                Message = notificationModel.Message,
                IdUser = notificationModel.IdUser,
            };

            context.Notifications.Add(newNotification);
            context.SaveChanges();
            return newNotification;
        }

        /// <summary>
        /// Delete the notification form DB with the provider ID
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteNotification(String notificationId)
        {
            try
            {
                var notificationToDelete = context.Notifications.FirstOrDefault(e => e.Id.ToString() == notificationId);

                if (notificationToDelete == null)
                {
                    return false;
                }
                context.Notifications.Remove(notificationToDelete);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir a notification", ex);
            }
        }




    }
}