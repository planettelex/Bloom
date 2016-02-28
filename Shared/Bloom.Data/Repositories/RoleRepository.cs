using System;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for role data.
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        /// <summary>
        /// Determines whether a role exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleId">The role identifier.</param>
        public bool RoleExists(IDataSource dataSource, Guid roleId)
        {
            if (!dataSource.IsConnected())
                return false;

            var roleTable = RoleTable(dataSource);
            if (roleTable == null)
                return false;

            var roleQuery =
                from r in roleTable
                where r.Id == roleId
                select r;

            return roleQuery.Any();
        }

        /// <summary>
        /// Determines whether a role exists.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleName">The name of the role.</param>
        public bool RoleExists(IDataSource dataSource, string roleName)
        {
            if (!dataSource.IsConnected())
                return false;

            var roleTable = RoleTable(dataSource);
            if (roleTable == null)
                return false;

            var roleQuery =
                from r in roleTable
                where r.Name.ToLower() == roleName.ToLower()
                select r;

            return roleQuery.Any();
        }

        /// <summary>
        /// Gets a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleId">The role identifier.</param>
        public Role GetRole(IDataSource dataSource, Guid roleId)
        {
            if (!dataSource.IsConnected())
                return null;

            var roleTable = RoleTable(dataSource);
            if (roleTable == null)
                return null;

            var roleQuery =
                from r in roleTable
                where r.Id == roleId
                select r;

            return roleQuery.SingleOrDefault();
        }

        /// <summary>
        /// Gets a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="roleName">The name of the role.</param>
        public Role GetRole(IDataSource dataSource, string roleName)
        {
            if (!dataSource.IsConnected())
                return null;

            var roleTable = RoleTable(dataSource);
            if (roleTable == null)
                return null;

            var roleQuery =
                from r in roleTable
                where r.Name.ToLower() == roleName.ToLower()
                select r;

            return roleQuery.SingleOrDefault();
        }

        /// <summary>
        /// Adds a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="role">The role.</param>
        public void AddRole(IDataSource dataSource, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            var roleTable = RoleTable(dataSource);
            if (roleTable == null)
                return;

            roleTable.InsertOnSubmit(role);
            dataSource.Save();
        }

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="role">The role.</param>
        public void DeleteRole(IDataSource dataSource, Role role)
        {
            if (!dataSource.IsConnected())
                return;

            var roleTable = RoleTable(dataSource);
            if (roleTable == null)
                return;

            roleTable.DeleteOnSubmit(role);
            dataSource.Save();
        }

        #region Tables

        private static Table<Role> RoleTable(IDataSource dataSource)
        {
            return dataSource != null ? dataSource.Context.GetTable<Role>() : null;
        }

        #endregion
    }
}
