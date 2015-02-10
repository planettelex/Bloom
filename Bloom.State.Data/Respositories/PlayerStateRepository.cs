using System.Data.Linq;
using System.Linq;
using Bloom.Data;
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
        public PlayerStateRepository(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }
        private readonly IDataSource _dataSource;
        private Table<PlayerState> PlayerStateTable { get { return _dataSource.Context.GetTable<PlayerState>(); } }

        /// <summary>
        /// Determines whether the player state exists.
        /// </summary>
        /// <returns></returns>
        public bool PlayerStateExists()
        {
            if (!_dataSource.IsConnected())
                return false;

            return PlayerStateTable.Any();
        }

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

        /// <summary>
        /// Adds the state of the player.
        /// </summary>
        /// <param name="playerState">State of the player.</param>
        public void AddPlayerState(PlayerState playerState)
        {
            if (!_dataSource.IsConnected() || PlayerStateExists())
                return;

            PlayerStateTable.InsertOnSubmit(playerState);
        }
    }
}
