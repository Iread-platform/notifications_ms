using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;

using iread_notifications_ms.Web.DTO;
using iread_notifications_ms.Web.Utils;
using iread_notifications_ms.Web.Service;
using iread_notifications_ms.DataAccess.Data.Entity;

namespace iread_notifications_ms.Controllers
{
    [ApiController]
    [Route("api/Notification/[controller]")]
    public class NotificationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly NotificationService _notificationService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;
        public NotificationController(NotificationService service, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _notificationService = service;
            _mapper = mapper;
            _firebaseMessagingService = firebaseMessagingService;
        }

        [HttpPost("Send")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendSingleNotification([FromBody] SingletNotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }
            SingleNotification Addednotification = await _notificationService.Sendnotification(_mapper.Map<SingleNotification>(notificationDto)) as SingleNotification;
            if (Addednotification != null)
            {
                try
                {
                    string result = await _firebaseMessagingService.sendMessage(Addednotification, null);
                }
                catch (System.Exception)
                {

                }


            }
            return null;

        }

        [HttpPost("BroadCast")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Broadcast([FromBody] BroadcastNotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }
            BroadcastNotification Addednotification = await _notificationService.Sendnotification(_mapper.Map<BroadcastNotification>(notificationDto)) as BroadcastNotification;
            if (Addednotification != null)
            {
                try
                {

                    string result = await _firebaseMessagingService.SendBoradcast(Addednotification, null);
                }
                catch (System.Exception)
                {

                }
            }
            return null;

        }

    }

}
