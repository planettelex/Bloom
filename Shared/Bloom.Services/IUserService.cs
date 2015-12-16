using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public interface IUserService
    {
        User InitializeUser();

        List<User> ListUsers();

        void AddUser(User user);

        void DeleteUser(User user);
    }
}
