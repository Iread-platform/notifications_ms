using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;

using iread_notifications_ms.Web.DTO;
using iread_notifications_ms.Web.Utils;
using iread_notifications_ms.Web.Service;
using iread_notifications_ms.DataAccess.Data.Entity;


namespace iread_notifications_ms.Controllers
{
    [ApiController]
    [Route("api/Reciver/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly NotificationService _notificationService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;


        public DeviceController(NotificationService service, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _notificationService = service;
            _mapper = mapper;
            _firebaseMessagingService = firebaseMessagingService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDevice([FromBody] AddDeviceDto addDeviceDto)
        {
            if (addDeviceDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(UserMessages.ModelStateParser(ModelState));
            }

            return Ok();

        }
    }

}