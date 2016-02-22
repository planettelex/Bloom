using System;
using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for user data.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="personId">The user's person identifier.</param>
        User GetUser(Guid personId);

        /// <summary>
        /// Gets the last user to login.
        /// </summary>
        User GetLastUser();

        /// <summary>
        /// Lists the users.
        /// </summary>
        List<User> ListUsers();

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">The user.</param>
        void AddUser(User user);

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The user.</param>
        void DeleteUser(User user);
    }
}
