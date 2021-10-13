using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using iread_notifications_ms.DataAccess.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iread_notifications_ms.Web.Controller
{
    [Route("[Controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet("")]
        [HttpHead("")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
