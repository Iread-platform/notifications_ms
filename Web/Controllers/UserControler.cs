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
        public async Task<IActionResult> GetUser(string userId)
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
            User user1 = _mapper.Map<User>(addUserDto);
            // If user has  a token
            if (!string.IsNullOrWhiteSpace(user1.Token))
            {
                // if the user with the same token already exists
                if (_UserService.ExactUserExists(user1))
                {
                    //chaeck his supscriptions 
                    SubscripeUserToAllTopics(user1);
                    return StatusCode(201);
                }
                User user2 = await _UserService.GetUser(user1.UserId);
                // if the user with a different or null token exists
                if (user2 != null)
                {
                    // If a different token exists.
                    if (!string.IsNullOrWhiteSpace(user2.Token))
                    {

                    }
                    //chaeck his supscriptions 
                    SubscripeUserToAllTopics(user1);
                    return StatusCode(201);
                }
            }
            User user = await _UserService.AddUser(user1);
            if (user != null) SubscripeUserToAllTopics(user);
            if (user != null)
            {
                return Ok(user);
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

        private async void SubscripeUserToAllTopics(User user)
        {
            List<Topic> topics = await _UserService.GetUserTopics(user.UserId);
            foreach (Topic topic in topics)
            {
                await _firebaseMessagingService.SubscribeToTopic(new List<string>() { user.Token }, topic.Title);
            }
        }

        private async void UnSubscripeUserToAllTopics(User user)
        {
            // unscubscribe from firebase topics

            try
            {
                List<Topic> topics = await _UserService.GetUserTopics(user.UserId);
                if (topics.Count > 0)
                {
                    foreach (Topic topic in topics)
                    {
                        await _firebaseMessagingService.UnSubscribeToTopic(new List<string>() { user.Token }, topic.Title);
                    }
                    await _topicService.UnScubscribeUserFromAllTopics(user);
                }



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
        public async Task<IActionResult> DeleteUser(string id)
        {

            User user = await _UserService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            User deletedUser = _UserService.DeleteUser(user);
            if (deletedUser == null)
            {
                return StatusCode(500);
            }
            return Ok(deletedUser);

        }
    }

}