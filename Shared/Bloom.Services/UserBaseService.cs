using System.Collections.Generic;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public class UserBaseService : IUserBaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserBaseService" /> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public UserBaseService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private readonly IUserRepository _userRepository;

        public User InitializeUser()
        {
            return _userRepository.GetLastUser();
        }

        public List<User> ListUsers()
        {
            return _userRepository.ListUsers();
        }

        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public void DeleteUser(User user)
        {
            _userRepository.DeleteUser(user);
        }
    }
}
