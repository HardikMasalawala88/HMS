using HMS.Data.ContextModels;
using HMS.Repository.Interface;
using HMS.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private IRepository<User> _userRepository;
        public UserRepository(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public void DeleteUser(long id)
        {
            User user = GetUser(id);
            _userRepository.Remove(user);
            _userRepository.SaveChanges();
        }

        public User GetUser(long id)
        {
            return _userRepository.GetById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public void InsertUser(User user)
        {
            _userRepository.Insert(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
