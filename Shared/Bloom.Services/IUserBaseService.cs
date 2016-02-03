using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    public interface IUserBaseService
    {
        User InitializeUser();

        List<User> ListUsers();

        void AddUser(User user);

        void DeleteUser(User user);
    }
}
