using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class LibraryRepository : ILibraryRespository
    {
        public LibraryRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
