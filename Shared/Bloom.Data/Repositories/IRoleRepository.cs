using System;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for role data.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Determines whether a role exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleId">The role identifier.</param>
        bool RoleExists(IDataSource dataSource, Guid roleId);

        /// <summary>
        /// Determines whether a role exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleName">The name of the role.</param>
        bool RoleExists(IDataSource dataSource, string roleName);

        /// <summary>
        /// Gets a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleId">The role identifier.</param>
        Role GetRole(IDataSource dataSource, Guid roleId);

        /// <summary>
        /// Gets a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleName">The name of the role.</param>
        Role GetRole(IDataSource dataSource, string roleName);

        /// <summary>
        /// Adds a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="role">The role.</param>
        void AddRole(IDataSource dataSource, Role role);

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="role">The role.</param>
        void DeleteRole(IDataSource dataSource, Role role);
    }
}
