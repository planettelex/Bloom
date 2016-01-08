using Bloom.Domain.Models;
using Bloom.State.Domain.Models;

namespace Bloom.LibraryModule.Services
{
    public interface ISharedLibraryService
    {
        /// <summary>
        /// Creates a new library.
        /// </summary>
        /// <param name="library">The library.</param>
        void CreateNewLibrary(Library library);

        /// <summary>
        /// Loads a new library from an existing file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        LibraryConnection ConnectNewLibrary(string filePath);

        /// <summary>
        /// Shows the connected libraries modal window.
        /// </summary>
        void ShowConnectedLibrariesModal();

        /// <summary>
        /// Shows the library properties modal window.
        /// </summary>
        /// <param name="library">The library.</param>
        void ShowLibraryPropertiesModal(Library library);
    }
}
