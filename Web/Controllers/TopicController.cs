using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
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
    public class TopicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly TopicService _topicService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;


        public TopicController(UserService UserService, TopicService topicService, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _userService = UserService;
            _topicService = topicService;
            _mapper = mapper;
            _firebaseMessagingService = firebaseMessagingService;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddTopic([FromBody] AddTopicDto addTopicDto)
        {
            if (addTopicDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }

            Topic topic = await _topicService.AddTopic(_mapper.Map<Topic>(addTopicDto));

            if (topic != null)
            {
                return Ok(topic);
            }
            return Ok();

        }


        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AllTopics()
        {

            List<Topic> topics = await _topicService.GetAllTopics();
            if (topics == null)
            {
                return NotFound();
            }
            if (topics.Count == 0)
            {
                return NotFound();

            }

            return Ok(topics);

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopic(int id)
        {

            Topic topic = await _topicService.GetTopic(id);
            if (topic == null)
            {
                return NotFound();
            }

            return Ok(topic);

        }

        [HttpPost("Subscribe")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Subscribe(TopicSubscribeDto topicSubscribeDto)
        {

            if (topicSubscribeDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }

            List<User> users = await _userService.GetUsers(topicSubscribeDto.Users);
            if (users == null || users.Count == 0)
            {
                return BadRequest(UserMessages.USER_NO_FOUND);
            }
            try
            {
                var topic = await _topicService.SubscribeToTopic(users, topicSubscribeDto.TopicTitle);
                if (topic == null)
                {
                    return BadRequest("Devices already subscribed to theis topic");
                }
                List<string> devicesTokens = new List<string>();
                foreach (var user in topic.Users)
                {
                    if (!String.IsNullOrWhiteSpace(user.Token))
                        devicesTokens.Add(user.Token);
                }
                // Topic topic = await _topicService.GetTopic(topicSubscribeDto.TopicId);
                if (devicesTokens.Count == 0 || devicesTokens == null)
                {
                    return Ok();
                }
                var topicManagementResponse = await _firebaseMessagingService.SubscribeToTopic(devicesTokens, topic.Title);
                return Ok();
            }
            catch (System.Exception e)
            {

                return StatusCode(500, e.ToString());
            }


        }

        [HttpPost("UnSubscribe")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UnSubscribe(UnSubscribeUserFromTopicDto topicSubscribeDto)
        {

            if (topicSubscribeDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }

            User user = await _userService.GetUser(topicSubscribeDto.User);
            if (user == null)
            {
                return BadRequest(UserMessages.USER_NO_FOUND);
            }
            try
            {
                UnsubscribefromTopic(user, topicSubscribeDto.TopicTitle);
                return Ok();
            }
            catch (System.Exception e)
            {

                return StatusCode(500, e.ToString());
            }


        }

        private async void SubscripeUserToAllTopics(User user)
        {
            List<Topic> topics = await _userService.GetUserTopics(user.UserId);
            foreach (Topic topic in topics)
            {
                await _firebaseMessagingService.SubscribeToTopic(new List<string>() { user.Token }, topic.Title);
            }
        }

        private async void UnsubscribefromTopic(User user, string topicTitle)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(user.Token))
                {
                    await _firebaseMessagingService.UnSubscribeToTopic(new List<string>() { user.Token }, topicTitle);

                }
                await _topicService.UnScubscribeUserFromTopic(user, topicTitle);
            }
            catch (System.Exception)
            {

                throw;
            }



        }
        [HttpDelete("{id}/delete")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser(int id)
        {

            Topic topic = await _topicService.GetTopic(id);
            if (topic == null)
            {
                return NotFound();
            }

            Topic deletedUser = _topicService.DeleteTopic(topic);
            if (deletedUser == null)
            {
                return StatusCode(500);
            }
            return Ok(deletedUser);

        }

    }

}