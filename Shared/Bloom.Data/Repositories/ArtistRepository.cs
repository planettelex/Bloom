using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        public ArtistRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
