using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using HMS.Repository.Repositories;
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
        private readonly ILoginTokenService _loginService;
        private readonly IUserService _loginUserService;
        private IConfiguration _config;
        private HospitalContext _context;
        public AccountController(ILogger<AccountController> logger, ILoginTokenService loginService, IUserService loginUserService, HospitalContext context, IConfiguration config)
        {
            _logger = logger;
            _loginService = loginService;
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
                string token = _loginService.GenerateLoginToken();
                var loggedInUser = _loginUserService.GetUserDetails(loginUser);
                return token;
            }
        }
    }
}
