using HMS.API.Utilities;
using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using HMS.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace HMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountController> _logger;
        private readonly JWTTokenGenerator _jWTTokenGenerator;
        private readonly IUserService _loginUserService;
        private readonly IConfiguration _config;
        private ApplicationContext _context;
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AccountController> logger, IUserService loginUserService, ApplicationContext context, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _jWTTokenGenerator = new JWTTokenGenerator(config);
            _loginUserService = loginUserService;
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Register(RegisterFormModel registerModel)
        {
            var userExists = await _userManager.FindByNameAsync(registerModel.Username);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User Already Exists..!"
                });
            }

            IdentityUser user = new IdentityUser()
            {
                Email = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerModel.Username
            };

            // Create User
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if(!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User Creation Failed..!"
                });
            }

            //Checking roles in db and creating if not exists
            if(!await _roleManager.RoleExistsAsync(ApplicationUserRoles.SuperAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.SuperAdmin));
            }
            if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.Admin));
            }
            if (!await _roleManager.RoleExistsAsync(ApplicationUserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(ApplicationUserRoles.User));
            }

            //Add role to user
            if(!string.IsNullOrEmpty(registerModel.Role) && registerModel.Role == ApplicationUserRoles.Admin)
            {
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.Admin);
            }
            else if (!string.IsNullOrEmpty(registerModel.Role) && registerModel.Role == ApplicationUserRoles.SuperAdmin)
            {
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.SuperAdmin);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, ApplicationUserRoles.User);
            }

            return Ok(new Response { Status = "Success", Message = "User Created Successfully...!"});
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginFormModel loginUser)
        {
            var user = await _userManager.FindByNameAsync(loginUser.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);
                string token = _jWTTokenGenerator.GenerateLoginToken(user.UserName, userRole);

                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
