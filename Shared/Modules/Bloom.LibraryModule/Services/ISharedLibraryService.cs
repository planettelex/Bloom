using System.Collections.Generic;
using Bloom.State.Domain.Models;

namespace Bloom.LibraryModule.Services
{
    public interface ISharedLibraryService
    {
        /// <summary>
        /// Loads a new library from an existing file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        LibraryConnection ConnectNewLibrary(string filePath);

        /// <summary>
        /// Connects the library.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        /// <param name="user">The user.</param>
        /// <param name="timestamp">if set to <c>true</c> [timestamp].</param>
        /// <param name="setLibrary">if set to <c>true</c> [set library].</param>
        bool ConnectLibrary(LibraryConnection libraryConnection, User user, bool timestamp, bool setLibrary);

        /// <summary>
        /// Connects the libraries.
        /// </summary>
        /// <param name="libraryConnections">The library connections.</param>
        /// <param name="user">The user.</param>
        /// <param name="timestamp">if set to <c>true</c> [timestamp].</param>
        /// <param name="setLibrary">if set to <c>true</c> [set library].</param>
        void ConnectLibraries(List<LibraryConnection> libraryConnections, User user, bool timestamp, bool setLibrary);

        /// <summary>
        /// Lists the library connections.
        /// </summary>
        List<LibraryConnection> ListLibraryConnections();

        /// <summary>
        /// Removes the library connection.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        void RemoveLibraryConnection(LibraryConnection libraryConnection);

        /// <summary>
        /// Adds or removes the library connections based on the state data source.
        /// </summary>
        void CheckLibraryConnections();

        /// <summary>
        /// Synchronizes the library owner and user.
        /// </summary>
        /// <param name="libraryConnection">The library connection.</param>
        /// <param name="user">The user.</param>
        void SyncLibraryOwnerAndUser(LibraryConnection libraryConnection, User user);

        /// <summary>
        /// Shows the connected libraries modal.
        /// </summary>
        void ShowConnectedLibrariesModal();
    }
}
