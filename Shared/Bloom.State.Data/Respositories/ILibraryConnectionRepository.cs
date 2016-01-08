using System;
using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for library connections.
    /// </summary>
    public interface ILibraryConnectionRepository
    {
        /// <summary>
        /// Gets the library connection.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        LibraryConnection GetLibraryConnection(Guid libraryId);

        /// <summary>
        /// Gets the library connection by it's file path.
        /// </summary>
        /// <param name="filePath">The library file path.</param>
        LibraryConnection GetLibraryConnection(string filePath);

        /// <summary>
        /// Lists the library connections.
        /// </summary>
        List<LibraryConnection> ListLibraryConnections(bool connected);

        /// <summary>
        /// Determines if a library connection exists.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        bool LibraryConnectionExists(Guid libraryId);

        /// <summary>
        /// Adds the library connections.
        /// </summary>
        /// <param name="libraryConnections">The library connections.</param>
        void AddLibraryConnections(List<LibraryConnection> libraryConnections);

        /// <summary>
        /// Adds the library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        void AddLibraryConnection(LibraryConnection libraryConnection);

        /// <summary>
        /// Deletes the library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        void DeleteLibraryConnection(LibraryConnection libraryConnection);
    }
}
