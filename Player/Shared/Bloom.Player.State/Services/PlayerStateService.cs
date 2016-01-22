﻿using System;
using Bloom.Data.Interfaces;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

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
        /// <param name="suiteStateRepository">The suite state repository.</param>
        /// <param name="playerStateRepository">The player state repository.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="sharedLibraryService">The shared library service.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PlayerStateService(IDataSource stateDataSource, ISuiteStateRepository suiteStateRepository, IPlayerStateRepository playerStateRepository,
            ILibraryConnectionRepository libraryConnectionRepository, ISharedLibraryService sharedLibraryService, IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            StateDataSource = stateDataSource;
            SuiteStateRepository = suiteStateRepository;
            LibraryConnectionRepository = libraryConnectionRepository;
            _sharedLibraryService = sharedLibraryService;
            _playerStateRepository = playerStateRepository;
            
            EventAggregator.GetEvent<SaveStateEvent>().Subscribe(SaveState);
        }
        private readonly IPlayerStateRepository _playerStateRepository;
        private readonly ISharedLibraryService _sharedLibraryService;

        /// <summary>
        /// Initializes the player application state.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public PlayerState InitializeState(User user)
        {
            State = _playerStateRepository.GetPlayerState(user) ?? NewPlayerState(user);
            SuiteState = SuiteStateRepository.GetSuiteState() ?? NewSuiteState();
            SuiteState.LastProcessAccess = ((PlayerState) State).ProcessName;
            SuiteState.ProcessAccessedOn = DateTime.Now;

            if (State.User == null)
                return (PlayerState) State;

            State.User.LastLogin = DateTime.Now;
            if (State.Connections == null || State.Connections.Count <= 0)
                return (PlayerState) State;

            _sharedLibraryService.ConnectLibraries(State.Connections, user, false, true);
            SaveState();

            return (PlayerState) State;
        }

        private SuiteState NewSuiteState()
        {
            var suiteState = SuiteState.Create();
            SuiteStateRepository.AddSuiteState(suiteState);

            return suiteState;
        }

        private PlayerState NewPlayerState(User user)
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
