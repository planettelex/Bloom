using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for library connections.
    /// </summary>
    public class LibraryConnectionRepository : ILibraryConnectionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryConnectionRepository"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public LibraryConnectionRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<LibraryConnection> LibraryConnectionTable { get { return _dataSource.Context.GetTable<LibraryConnection>(); } }

        /// <summary>
        /// Gets the library connection.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public LibraryConnection GetLibraryConnection(Guid libraryId)
        {
            if (!_dataSource.IsConnected())
                return null;

            var query =
                from libraryConnection in LibraryConnectionTable
                where libraryConnection.LibraryId == libraryId
                select libraryConnection;

            return query.ToList().FirstOrDefault();
        }

        /// <summary>
        /// Lists the library connections.
        /// </summary>  
        public List<LibraryConnection> ListLibraryConnections()
        {
            if (!_dataSource.IsConnected())
                return null;

            var query =
                from libraryConnection in LibraryConnectionTable
                select libraryConnection;

            return query.ToList();
        }

        /// <summary>
        /// Determines whether a library connection exists.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public bool LibraryConnectionExists(Guid libraryId)
        {
            if (!_dataSource.IsConnected())
                return false;

            return LibraryConnectionTable.Any(libraryConnection => libraryConnection.LibraryId == libraryId);
        }

        /// <summary>
        /// Adds the library connections.
        /// </summary>
        /// <param name="libraryConnections">The library connections.</param>
        public void AddLibraryConnections(List<LibraryConnection> libraryConnections)
        {
            foreach (var libraryConnection in libraryConnections)
                AddLibraryConnection(libraryConnection);
        }

        /// <summary>
        /// Adds the library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        public void AddLibraryConnection(LibraryConnection libraryConnection)
        {
            if (!_dataSource.IsConnected() || LibraryConnectionExists(libraryConnection.LibraryId))
                return;

            LibraryConnectionTable.InsertOnSubmit(libraryConnection);
        }

        /// <summary>
        /// Deletes the library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        public void DeleteLibraryConnection(LibraryConnection libraryConnection)
        {
            if (!_dataSource.IsConnected() || !LibraryConnectionExists(libraryConnection.LibraryId))
                return;

            LibraryConnectionTable.DeleteOnSubmit(libraryConnection);
        }
    }
}
