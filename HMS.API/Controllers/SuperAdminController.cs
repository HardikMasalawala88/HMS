using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using HMS.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS.API.Controllers
{
    [Authorize(ApplicationUserRoles.SuperAdmin)]
    [ApiController]
    [Route("[controller]")]
    public class SuperAdminController : Controller
    {
        private readonly ILogger<SuperAdminController> _logger;
        private readonly IUserService _userService;
        private IConfiguration _config;
        private ApplicationContext _context;
        public SuperAdminController(ILogger<SuperAdminController> logger, IUserService userService, ApplicationContext context, IConfiguration config)
        {
            _logger = logger;
            _userService = userService;
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult AddAdmin(UserFM userForm)
        {
            var data = _userService.Create(userForm);

            return Ok();
        }
    }
}
