using Bloom.Data.Interfaces;

namespace Bloom.Data.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        public PlaylistRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
    }
}
