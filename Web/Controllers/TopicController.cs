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
    public class TopicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DeviceService _deviceService;
        private readonly TopicService _topicService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;


        public TopicController(DeviceService deviceService, TopicService topicService, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _deviceService = deviceService;
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
            try
            {
                List<TopicUsers> topicUsers = await _topicService.SubscribeToTopic(topicSubscribeDto);
                if (topicUsers == null)
                {
                    return BadRequest("Devices already subscribed to theis topic");
                }
                List<string> devicesTokens = new List<string>();
                foreach (var user in topicUsers)
                {
                    devicesTokens.Add(user.Token);
                }
                Topic topic = await _topicService.GetTopic(topicSubscribeDto.TopicId);
                var topicManagementResponse = await _firebaseMessagingService.SubscribeToTopic(devicesTokens, topic.Title);

                return Ok(topicUsers);
            }
            catch (System.Exception e)
            {

                return StatusCode(500, e.ToString());
            }


        }
    }

}