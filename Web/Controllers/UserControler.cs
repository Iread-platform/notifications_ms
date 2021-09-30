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
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserService _UserService;
        private readonly TopicService _topicService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;


        public UserController(UserService service, TopicService topicService, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _UserService = service;
            _mapper = mapper;
            _topicService = topicService;
            _firebaseMessagingService = firebaseMessagingService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(int userId)
        {

            User user = await _UserService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);

        }
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
        {
            if (addUserDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }

            User User = await _UserService.AddUser(_mapper.Map<User>(addUserDto));

            if (User != null)
            {
                return Ok(User);
            }
            return Ok();

        }


        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AllUsers()
        {

            List<User> Users = await _UserService.GetAllUsers();
            if (Users == null)
            {
                return NotFound();
            }
            if (Users.Count == 0)
            {
                return NotFound();

            }

            return Ok(Users);

        }

        [HttpGet("ByTopic/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByTopicId(int id)
        {

            if (!await _topicService.TopicExists(id))
            {
                return BadRequest(UserMessages.TOPIC_NO_FOUND);
            }
            List<User> users = await _UserService.GetUserByTopic(id);
            if (users == null)
            {
                return NotFound();
            }
            if (users.Count == 0)
            {
                return NotFound();
            }


            return Ok(users.ConvertAll<UserGetDto>(u => _mapper.Map<UserGetDto>(u)));

        }
    }

}