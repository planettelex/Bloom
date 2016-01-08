using System;
using Bloom.Data.Interfaces;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.Player.State.Services
{
    /// <summary>
    /// Service for managing the player application state.
    /// </summary>
    public class PlayerStateService : StateBaseService, IPlayerStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStateService" /> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="playerStateRepository">The player state repository.</param>
        /// <param name="libraryService">The library service.</param>
        public PlayerStateService(IDataSource stateDataSource, IPlayerStateRepository playerStateRepository, ILibraryService libraryService)
        {
            StateDataSource = stateDataSource;
            _playerStateRepository = playerStateRepository;
            _libraryService = libraryService;

            EventAggregator.GetEvent<SaveStateEvent>().Subscribe(SaveState);
        }
        private readonly ILibraryService _libraryService;
        private readonly IPlayerStateRepository _playerStateRepository;

        /// <summary>
        /// Initializes the player application state.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public PlayerState InitializeState(User user)
        {
            State = _playerStateRepository.GetPlayerState(user) ?? AddNewState(user);

            if (State.User == null)
                return (PlayerState) State;

            State.User.LastLogin = DateTime.Now;
            if (State.Connections == null || State.Connections.Count <= 0)
                return (PlayerState) State;

            _libraryService.ConnectLibraries(State.Connections, user, false, true);
            SaveState();

            return (PlayerState) State;
        }

        private PlayerState AddNewState(User user)
        {
            var playerState = new PlayerState();
            playerState.SetUser(user);
            _playerStateRepository.AddPlayerState(playerState);

            EventAggregator.GetEvent<UserChangedEvent>().Publish(null);
            return playerState;
        }

        private void SaveState(object nothing)
        {
            SaveState();
        }
    }
}
