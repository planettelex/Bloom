using System.Data.Linq;
using System.Linq;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the player state.
    /// </summary>
    public class PlayerStateRepository : IPlayerStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStateRepository"/> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        public PlayerStateRepository(IStateDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IStateDataSource _dataSource;
        private Table<PlayerState> PlayerStateTable { get { return _dataSource.Context.GetTable<PlayerState>(); } }

        /// <summary>
        /// Gets the state of the player.
        /// </summary>
        /// <returns></returns>
        public PlayerState GetPlayerState()
        {
            if (!_dataSource.IsConnected())
                return null;

            var query =
                from analyticsState in PlayerStateTable
                select analyticsState;

            return query.ToList().SingleOrDefault();
        }
    }
}
