using System;
using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Modules.UserModule.Services
{
    /// <summary>
    /// Service for shared user operations.
    /// </summary>
    public interface ISharedUserService
    {
        /// <summary>
        /// Initializes the user to the last one to use the suite.
        /// </summary>
        User InitializeUser();

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="personId">The user's person identifier.</param>
        User GetUser(Guid personId);

        /// <summary>
        /// Lists all users.
        /// </summary>
        List<User> ListUsers();

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">The user.</param>
        void AddUser(User user);

        /// <summary>
        /// Checks to see if the suite user has changed.
        /// </summary>
        void CheckUser();
    }
}
