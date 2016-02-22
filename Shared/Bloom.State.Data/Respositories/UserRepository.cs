using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for user data.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public UserRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<User> UserTable { get { return _dataSource.Context.GetTable<User>(); } }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="personId">The user's person identifier.</param>
        public User GetUser(Guid personId)
        {
            if (!_dataSource.IsConnected())
                return null;

            var userQuery =
                from users in UserTable
                where users.PersonId == personId
                select users;

            return userQuery.SingleOrDefault();
        }

        /// <summary>
        /// Gets the last user to login.
        /// </summary>
        public User GetLastUser()
        {
            if (!_dataSource.IsConnected())
                return null;

            var allUsers = ListUsers();

            return allUsers.OrderBy(user => user.LastLogin).Reverse().FirstOrDefault();
        }

        /// <summary>
        /// Lists the users.
        /// </summary>
        public List<User> ListUsers()
        {
            if (!_dataSource.IsConnected())
                return null;

            var lastUserQuery =
                from users in UserTable
                orderby users.LastLogin descending
                select users;

            var allUsers = lastUserQuery.ToList();
            _dataSource.Refresh(allUsers);

            return allUsers;
        }

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void AddUser(User user)
        {
            var existingUser = GetUser(user.PersonId);
            if (existingUser == null)
                UserTable.InsertOnSubmit(user);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void DeleteUser(User user)
        {
            UserTable.DeleteOnSubmit(user);
        }
    }
}
