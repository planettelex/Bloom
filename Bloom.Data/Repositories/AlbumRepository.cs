using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        public AlbumRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
