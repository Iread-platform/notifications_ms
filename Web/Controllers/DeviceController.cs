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
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DeviceService _deviceService;
        private readonly IFirebaseMessagingService _firebaseMessagingService;


        public DeviceController(DeviceService service, IMapper mapper, IFirebaseMessagingService firebaseMessagingService)
        {
            _deviceService = service;
            _mapper = mapper;
            _firebaseMessagingService = firebaseMessagingService;
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

            Device device = await _deviceService.AddDevice(_mapper.Map<Device>(addDeviceDto));

            if (device != null)
            {
                return Ok(device);
            }
            return Ok();

        }
    }

}