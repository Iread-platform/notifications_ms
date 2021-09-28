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
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly TopicService _topicService;
        private readonly DeviceService _deviceService;
        private readonly NotificationService _notificationService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;
        public NotificationController(TopicService topicService, DeviceService deviceService, NotificationService service, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _notificationService = service;
            _mapper = mapper;
            _topicService = topicService;
            _deviceService = deviceService;
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
            Addednotification.Token = (await _deviceService.GetDevice(notificationDto.user)).Token;
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
        public async Task<IActionResult> Broadcast([FromBody] TopicNotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }
            TopicNotification Addednotification = await _notificationService.Sendnotification(_mapper.Map<TopicNotification>(notificationDto)) as TopicNotification;
            if (Addednotification != null)
            {
                try
                {
                    Addednotification.Topic = await _topicService.GetTopic(Addednotification.TopicId);
                    string result = await _firebaseMessagingService.SendTopicNotification(Addednotification, null);
                    // string result = await _firebaseMessagingService.
                    return Ok();
                }
                catch (System.Exception)
                {

                }
            }
            return StatusCode(500, "Notifications Where not sent");
        }

        [HttpPost("Multicast")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Multicast([FromBody] TopicNotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }
            TopicNotification Addednotification = await _notificationService.Sendnotification(_mapper.Map<TopicNotification>(notificationDto)) as TopicNotification;
            if (Addednotification != null)
            {
                try
                {
                    Addednotification.Topic = await _topicService.GetTopic(Addednotification.TopicId);
                    string result = await _firebaseMessagingService.SendTopicNotification(Addednotification, null);
                    // string result = await _firebaseMessagingService.
                    return Ok();
                }
                catch (System.Exception)
                {

                }
            }
            return StatusCode(500, "Notifications Where not sent");
        }


    }

}
