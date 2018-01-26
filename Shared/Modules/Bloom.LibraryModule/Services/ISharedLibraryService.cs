using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.Modules.LibraryModule.Services
{
    /// <summary>
    /// Service for shared library operations.
    /// </summary>
    public interface ISharedLibraryService
    {
        /// <summary>
        /// Loads a new library from an existing file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        LibraryConnection ConnectNewLibrary(string filePath);

        /// <summary>
        /// Connects the existing library.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        /// <param name="user">The user.</param>
        /// <param name="timestamp">If set to <c>true</c> set the last connected time of library and owner.</param>
        /// <param name="setLibrary">If set to <c>true</c> set the library property of the connection.</param>
        bool ConnectLibrary(LibraryConnection libraryConnection, User user, bool timestamp, bool setLibrary);

        /// <summary>
        /// Connects the existing libraries.
        /// </summary>
        /// <param name="libraryConnections">The library connections.</param>
        /// <param name="user">The user.</param>
        /// <param name="timestamp">If set to <c>true</c> set the last connected time of library and owner.</param>
        /// <param name="setLibrary">if set to <c>true</c> set the library property of the connection.</param>
        void ConnectLibraries(List<LibraryConnection> libraryConnections, User user, bool timestamp, bool setLibrary);

        /// <summary>
        /// Lists the library connections.
        /// </summary>
        List<LibraryConnection> ListLibraryConnections();

        /// <summary>
        /// Removes a library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        void RemoveLibraryConnection(LibraryConnection libraryConnection);

        /// <summary>
        /// Adds or removes the library connections based on the state data source.
        /// </summary>
        void CheckLibraryConnections();

        /// <summary>
        /// Synchronizes the data between the library connection owner and the provided user.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        /// <param name="user">The user.</param>
        void SyncLibraryOwnerAndUser(LibraryConnection libraryConnection, User user);

        /// <summary>
        /// Shows the connected libraries modal window.
        /// </summary>
        void ShowConnectedLibrariesModal();
    }
}
