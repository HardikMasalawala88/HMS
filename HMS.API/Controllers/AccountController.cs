using HMS.API.Utilities;
using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using HMS.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HMS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly JWTTokenGenerator _jWTTokenGenerator;
        private readonly IUserService _loginUserService;
        private IConfiguration _config;
        private ApplicationContext _context;
        public AccountController(ILogger<AccountController> logger, IUserService loginUserService, ApplicationContext context, IConfiguration config)
        {
            _logger = logger;
            _jWTTokenGenerator = new JWTTokenGenerator(config);
            _loginUserService = loginUserService;
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("[action]")]
        public string Login(LoginFormModel loginUser)
        {
            if (loginUser.Username == null && loginUser.Password != null)
            {
                return "";
            }
            else
            {
                User userData = _loginUserService.GetUserDetails(loginUser);
                Role roleData = _context.Roles.Where(x => x.RoleId == userData.RoleId).FirstOrDefault();
                string token = _jWTTokenGenerator.GenerateLoginToken(userData.EmailId,roleData.Name);
                return token;
            }
        }
    }
}
