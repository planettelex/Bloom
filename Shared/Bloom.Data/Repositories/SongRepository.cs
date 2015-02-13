using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class SongRepository : ISongRepository
    {
        public SongRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
