using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using HMS.Repository.Repository;
using HMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class LoginUserService : ILoginUserService
    {
        private IRepository<User> _userRepository;

        public LoginUserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUserDetails(LoginFormModel loginUser)
        {
            return _userRepository.GetAll().Where(x => x.UserName == loginUser.Username && x.Password == loginUser.Password);
        }
    }
}
