using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

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
        private readonly UserService _userService;
        private readonly NotificationService _notificationService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;
        public NotificationController(TopicService topicService, UserService UserService, NotificationService service, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _notificationService = service;
            _mapper = mapper;
            _topicService = topicService;
            _userService = UserService;
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
            User user = await _userService.GetUser(notificationDto.UserId);
            if (user == null)
            {
                return BadRequest("User has no registered devics.");

            }
            SingleNotification Addednotification = await _notificationService.SendNotification(_mapper.Map<SingleNotification>(notificationDto), notificationDto.UserId) as SingleNotification;
            if (Addednotification != null)
            {
                Addednotification.Token = (await _userService.GetUser(notificationDto.UserId)).Token;

                try
                {
                    string result = await _firebaseMessagingService.sendMessage(Addednotification, null);
                    return Ok(UserMessages.NOTIFICATION_SENT);
                }
                catch (System.Exception)
                {
                    return BadRequest(UserMessages.NOTIFICATION_NOT_SENT);

                }

            }
            return BadRequest();

        }

        [HttpGet("ByUserId/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserNotifications(int id)
        {
            List<SingleNotification> notifications = await _notificationService.GetUserNotifications(id);

            if (notifications == null)
            {
                return NotFound();
            }
            if (notifications.Count == 0)
            {
                return NotFound();

            }
            List<SingletNotificationGetDto> notificationsToShow = notifications.ConvertAll<SingletNotificationGetDto>(s => _mapper.Map<SingletNotificationGetDto>(s));
            return Ok(notificationsToShow);

        }

        [HttpGet("ByTopicId/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopicNotifications(int id)
        {
            if (!await _topicService.TopicExists(id))
            {
                return NotFound(UserMessages.TOPIC_NO_FOUND);
            }

            List<TopicNotification> notifications = await _notificationService.GetTopicNotifications(id);

            if (notifications == null)
            {
                return NotFound();
            }
            if (notifications.Count == 0)
            {
                return NotFound();

            }
            // List<SingletNotificationGetDto> notificationsToShow = notifications.ConvertAll<SingletNotificationGetDto>(s => _mapper.Map<SingletNotificationGetDto>(s));
            return Ok(notifications.ConvertAll<TopicNotificationGetDto>(s => _mapper.Map<TopicNotificationGetDto>(s)));

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
            var topic = await _topicService.GetTopic(notificationDto.TopicID);
            if (topic == null)
            {
                return NotFound(UserMessages.TOPIC_NO_FOUND);
            }
            TopicNotification Addednotification = await _notificationService.SendNotification(_mapper.Map<TopicNotification>(notificationDto), 0) as TopicNotification;
            if (Addednotification != null)
            {
                try
                {
                    Addednotification.Topic = topic;
                    string result = await _firebaseMessagingService.SendTopicNotification(Addednotification, null);
                    return Ok();
                }
                catch (System.Exception)
                {
                    return BadRequest(UserMessages.NOTIFICATION_NOT_SENT);
                }
            }
            return StatusCode(500, UserMessages.NOTIFICATION_NOT_SENT);
        }

        [HttpPost("Multicast")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Multicast([FromBody] MulticastNotificationDTo multicastDto)
        {
            if (multicastDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }
            List<User> Users = await _userService.GetUsers(multicastDto.Users);
            if (Users == null)
            {
                return BadRequest("No Users found for the given users.");

            }
            TopicNotification Addednotification = await _notificationService.SendNotification(_mapper.Map<TopicNotification>(multicastDto), 0) as TopicNotification;
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
