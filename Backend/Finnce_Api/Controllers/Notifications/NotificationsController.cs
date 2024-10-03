using Finnce_Api.core.Notifications;
using Finnce_Api.Models.NotificationDto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Finnce_Api.Controllers.Notifications
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService NotificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.NotificationService = notificationService;
        }

        /// <summary>
        /// Recieve an notification id than delete it
        /// </summary>
        /// <param name="notificationId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteNotification")]
        public IActionResult DeleteNotification(String notificationId)
        {
            try
            {
                var deleteNotification = NotificationService.DeleteNotification(notificationId);
                return Ok(deleteNotification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao excluir Notification");
            }
        }

        /// <summary>
        /// Get all notifications of the user
        /// </summary>
        /// <returns></returns>
        [HttpGet("User/GetAllNotificationsForUser")]
        [Authorize]
        public ActionResult<IEnumerable<Notification>> GetAllNotificationsForUser()
        {
            var notifications = this.NotificationService.GetAllNotificationsForUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(notifications);
        }

        /// <summary>
        /// Create new notifications 
        /// </summary>
        /// <param name="notificationModel"></param>
        /// <returns></returns>
        [HttpPost("CreateNotification")]
        [Authorize]
        public ActionResult<Notification> CreateNotification([FromBody] NotificationModel notificationModel)
        {
            try
            {
                var createNotification = NotificationService.CreateNotification(notificationModel, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(createNotification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro Interno do servidor");
            }
        }

    }
}