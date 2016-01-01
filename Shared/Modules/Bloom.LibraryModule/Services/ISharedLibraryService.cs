using Bloom.Domain.Models;

namespace Bloom.LibraryModule.Services
{
    public interface ISharedLibraryService
    {
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
