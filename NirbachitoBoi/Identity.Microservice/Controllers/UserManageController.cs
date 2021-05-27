using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Microservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserManageController : ControllerBase
    {
        private readonly ILogger<UserManageController> _logger;

        public UserManageController(ILogger<UserManageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("updateMyBooks")]
        public string updateMyBooks()
        {
            return "set successfully";
        }
    }
}
