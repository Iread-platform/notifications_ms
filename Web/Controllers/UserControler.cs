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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser(string id)
        {
            User user = await _UserService.GetUser(id);
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
            User userToAdd = _mapper.Map<User>(addUserDto);
            // If user has a token
            if (!string.IsNullOrWhiteSpace(userToAdd.Token))
            {
                // if the user with the same token already exists
                if (_UserService.ExactUserExists(userToAdd))
                {
                    //chaeck his supscriptions 
                    SubscripeUserToAllTopics(userToAdd);
                    return StatusCode(201);
                }
                User existedUser = await _UserService.GetUser(userToAdd.UserId);

                // if the same user with a different or null token exists
                if (existedUser != null)
                {
                    // If a different token exists.
                    if (!string.IsNullOrWhiteSpace(existedUser.Token))
                    {
                        try
                        {
                            await ChangeUserDevice(existedUser, addUserDto.Token);

                        }
                        catch (System.Exception)
                        {
                            return StatusCode(500);
                        }
                    }

                    // Add the new token and sunscribe to to
                    // Check his subscribtions 
                    existedUser.Token = userToAdd.Token;
                    _UserService.UpdateUser(existedUser);
                    SubscripeUserToAllTopics(existedUser);
                    return StatusCode(201);
                }

                // If a different user has the same token.
                User userWithThesameToken = await _UserService.GetUserByToken(userToAdd.Token);
                if (userWithThesameToken != null)
                {
                    return BadRequest(UserMessages.TOKEN_EXISTS);
                }

            }

            User user = await _UserService.AddUser(userToAdd);
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

        [HttpGet("{id}/ByTopic")]
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

        private async void SubscripeUserToAllTopics(User user)
        {
            List<Topic> topics = await _UserService.GetUserTopics(user.UserId);
            foreach (Topic topic in topics)
            {
                await _firebaseMessagingService.SubscribeToTopic(new List<string>() { user.Token }, topic.Title);
            }
        }

        private async Task UnSubscripeUserFromAllTopics(User user)
        {
            // unsubscribe from firebase topics
            try
            {
                await UnSubscripeUserFromAllFirebaseTopics(user);
                await _topicService.UnScubscribeUserFromAllTopics(user);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private async Task UnSubscripeUserFromAllFirebaseTopics(User user)
        {
            // unsubscribe from firebase topics
            try
            {
                List<Topic> topics = await _UserService.GetUserTopics(user.UserId);
                if (topics.Count > 0)
                {
                    foreach (Topic topic in topics)
                    {
                        await _firebaseMessagingService.UnSubscribeToTopic(new List<string>() { user.Token }, topic.Title);
                    }
                }

            }
            catch (System.Exception)
            {

                throw;
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

        private async Task ChangeUserDevice(User user, string newDevice)
        {
            // Unscubscribe the current user from his topics.
            await UnSubscripeUserFromAllFirebaseTopics(user);

            // Update user's token.
            user.Token = newDevice;
            _UserService.UpdateUser(user);

            // Resubscribe user to all his topics 
            SubscripeUserToAllTopics(user);
        }


    }

}