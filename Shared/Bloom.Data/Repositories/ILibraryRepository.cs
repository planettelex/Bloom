using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    /// <summary>
    /// Access methods for library data.
    /// </summary>
    public interface ILibraryRepository
    {
        /// <summary>
        /// Gets the library.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        Library GetLibrary(IDataSource dataSource);

        /// <summary>
        /// Adds the library.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="library">The library.</param>
        void AddLibrary(IDataSource dataSource, Library library);
    }
}
