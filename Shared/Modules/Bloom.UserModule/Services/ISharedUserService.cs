using System;
using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.UserModule.Services
{
    public interface ISharedUserService
    {
        User InitializeUser();

        User GetUser(Guid personId); 

        List<User> ListUsers();

        void AddUser(User user);
    }
}
