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
        private readonly TopicService _TopicService;
        private readonly TopicService _topicService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;


        public TopicController(TopicService TopicService, TopicService topicService, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _TopicService = TopicService;
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

            Topic topic = await _TopicService.AddTopic(_mapper.Map<Topic>(addTopicDto));

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

            List<Topic> topics = await _TopicService.GetAllTopics();
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
    }

}