using System;
using System.Data.Linq;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for player state data.
    /// </summary>
    /// <seealso cref="Bloom.State.Data.Respositories.IPlayerStateRepository" />
    public class PlayerStateRepository : IPlayerStateRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStateRepository" /> class.
        /// </summary>
        /// <param name="dataSource">The data source.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        public PlayerStateRepository(IDataSource dataSource, ILibraryConnectionRepository libraryConnectionRepository)
        {
            _dataSource = dataSource;
            _libraryConnectionRepository = libraryConnectionRepository;
        }
        private readonly IDataSource _dataSource;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;
        private Table<PlayerState> PlayerStateTable { get { return _dataSource.Context.GetTable<PlayerState>(); } }

        /// <summary>
        /// Determines whether the player state exists.
        /// </summary>
        /// <param name="user">The user.</param>
        public bool PlayerStateExists(User user)
        {
            if (!_dataSource.IsConnected() || user == null)
                return false;

            return PlayerStateTable.Any(u => u.UserId == user.PersonId);
        }

        /// <summary>
        /// Gets player state.
        /// </summary>
        /// <param name="user">The user.</param>
        public PlayerState GetPlayerState(User user)
        {
            if (!_dataSource.IsConnected() || user == null)
                return null;

            var stateQuery =
                from state in PlayerStateTable
                where state.UserId == user.PersonId
                select state;

            var playerState = stateQuery.SingleOrDefault();

            if (playerState != null)
            {
                playerState.User = user;
                playerState.Connections = _libraryConnectionRepository.ListLibraryConnections(true);
            }
                
            return playerState;
        }

        /// <summary>
        /// Adds the player state.
        /// </summary>
        /// <param name="playerState">The player state.</param>
        public void AddPlayerState(PlayerState playerState)
        {
            if (!_dataSource.IsConnected() || playerState == null || playerState.User == null || playerState.UserId == Guid.Empty || PlayerStateExists(playerState.User))
                return;

            PlayerStateTable.InsertOnSubmit(playerState);
        }
    }
}
