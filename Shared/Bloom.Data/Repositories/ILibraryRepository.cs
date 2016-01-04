using Bloom.Data.Interfaces;
using Bloom.Domain.Models;

namespace Bloom.Data.Repositories
{
    public interface ILibraryRepository
    {
        Library GetLibrary(IDataSource dataSource);

        void AddLibrary(IDataSource dataSource, Library library);
    }
}
