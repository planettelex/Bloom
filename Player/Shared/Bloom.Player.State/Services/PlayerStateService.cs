using System;
using System.IO;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.Player.State.Services
{
    /// <summary>
    /// Service for managing the player application state.
    /// </summary>
    public class PlayerStateService : IPlayerStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerStateService"/> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="playerStateRepository">The player state repository.</param>
        public PlayerStateService(IDataSource stateDataSource, IPlayerStateRepository playerStateRepository)
        {
            _stateDataSource = stateDataSource;
            _playerStateRepository = playerStateRepository;
        }
        private readonly IDataSource _stateDataSource;
        private readonly IPlayerStateRepository _playerStateRepository;

        /// <summary>
        /// Initializes the player application state.
        /// </summary>
        /// <returns>
        /// The player application state.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public PlayerState InitializeState()
        {
            if (string.IsNullOrEmpty(_stateDataSource.FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            if (File.Exists(_stateDataSource.FilePath))
            {
                _stateDataSource.Connect();
                return _playerStateRepository.GetPlayerState() ?? AddNewPlayerState();
            }
            else
            {
                _stateDataSource.Create();
                return AddNewPlayerState();
            }
        }

        /// <summary>
        /// Saves the player application state.
        /// </summary>
        public void SaveState()
        {
            _stateDataSource.Save();
        }

        private PlayerState AddNewPlayerState()
        {
            var playerState = new PlayerState();
            _playerStateRepository.AddPlayerState(playerState);
            _stateDataSource.Save();
            return playerState;
        }
    }
}
