using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Data;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.State.Domain.Models
{
    /// <summary>
    /// Application state.
    /// </summary>
    public abstract class ApplicationState : BindableBase
    {
        /// <summary>
        /// Gets or sets the user of this application state.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the library connections.
        /// </summary>
        public List<LibraryConnection> Connections { get; set; }

        public virtual void SetUser(User user)
        {
            if (user == null)
                return;

            User = user;
            User.LastLogin = DateTime.Now;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public LibraryConnection GetConnection(Guid libraryId)
        {
            if (Connections == null || Connections.Count == 0)
                return null;

            return Connections.SingleOrDefault(c => c.LibraryId == libraryId);
        }

        /// <summary>
        /// Gets the library connection data source.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public LibraryDataSource GetConnectionData(Guid libraryId)
        {
            var connection = GetConnection(libraryId);
            return connection == null ? null : connection.DataSource;
        }

        /// <summary>
        /// Adds the connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        public void AddConnection(LibraryConnection libraryConnection)
        {
            if (Connections == null)
                Connections = new List<LibraryConnection>();

            if (!Connections.Contains(libraryConnection))
                Connections.Insert(0, libraryConnection);
        }

        /// <summary>
        /// Removes the connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        public void RemoveConnection(LibraryConnection libraryConnection)
        {
            if (Connections == null || Connections.Count == 0)
                return;

            Connections.Remove(libraryConnection);
        }

        /// <summary>
        /// Determines whether the specified library is connected.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public bool IsConnected(Guid libraryId)
        {
            var dataSource = GetConnectionData(libraryId);

            if (dataSource == null)
                return false;

            return dataSource.IsConnected();
        }
    }
}
