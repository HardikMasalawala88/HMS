using HMS.Data.ContextModels;
using HMS.Data.FormModels;
using HMS.Repository.Interface;
using HMS.Repository.Repository;
using HMS.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationContext _context;

        public UserService(IUserRepository userRepository, ApplicationContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public UserFM Create(UserFM userFM)
        {
            try
            {
                User user = new User();
                user.Name = userFM.Name;
                user.EmailId = userFM.EmailId;
                user.Gender = userFM.Gender;
                user.Address = userFM.Address;
                user.City = userFM.City;
                user.MobileNo = userFM.MobileNo;
                user.RoleId = userFM.RoleId;
                user.Username = userFM.Username;
                user.Password = userFM.Password;
                _userRepository.InsertUser(user);
            }
            catch (Exception ex)
            {
                
            }
            return userFM;
        }

        public User GetUserDetails(LoginFormModel loginUser)
        {
            User userInfo = _userRepository.GetUsers().Where(x => x.Username == loginUser.Username && 
                                                    x.Password == loginUser.Password).FirstOrDefault();
            return userInfo;
        }
    }
}
