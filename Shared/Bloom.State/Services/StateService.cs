using System;
using System.IO;
using Bloom.Common;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.State.Services
{
    /// <summary>
    /// Service for managing state.
    /// </summary>
    public class StateService : IStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateService" /> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="analyticsStateRepository">The analytics state repository.</param>
        /// <param name="browserStateRepository">The browser state repository.</param>
        /// <param name="playerStateRepository">The player state repository.</param>
        public StateService(IDataSource stateDataSource, IAnalyticsStateRepository analyticsStateRepository, IBrowserStateRepository browserStateRepository, IPlayerStateRepository playerStateRepository)
        {
            _stateDataSource = stateDataSource;
            _analyticsStateRepository = analyticsStateRepository;
            _browserStateRepository = browserStateRepository;
            _playerStateRepository = playerStateRepository;
        }
        private readonly IDataSource _stateDataSource;
        private readonly IAnalyticsStateRepository _analyticsStateRepository;
        private readonly IBrowserStateRepository _browserStateRepository;
        private readonly IPlayerStateRepository _playerStateRepository;

        /// <summary>
        /// Initializes the state of a provided process.
        /// </summary>
        /// <param name="processType">The type of process.</param>
        public IApplicationState InitializeState(ProcessType processType)
        {
            var stateDatabasePath = GetStateDatabasePath();
            IApplicationState state = null;

            switch (processType)
            {
                case ProcessType.Analytics:
                    state = InitializeAnalyticsState(stateDatabasePath);
                    break;
                case ProcessType.Browser:
                    state = InitializeBrowserState(stateDatabasePath);
                    break;
                case ProcessType.Player:
                    state = InitializePlayerState(stateDatabasePath);
                    break;
            }
            return state;
        }

        /// <summary>
        /// Saves the state.
        /// </summary>
        public void SaveState()
        {
            _stateDataSource.Save();
        }

        private AnalyticsState InitializeAnalyticsState(string stateDatabasePath)
        {
            AnalyticsState analyticsState;

            if (File.Exists(stateDatabasePath))
            {
                _stateDataSource.Connect(stateDatabasePath);
                analyticsState = _analyticsStateRepository.GetAnalyticsState() ?? AddNewAnalyticsState();
            }
            else
            {
                _stateDataSource.Create(stateDatabasePath);
                analyticsState = AddNewAnalyticsState();
            }

            analyticsState.Connections.StateConnection.FilePath = stateDatabasePath;
            return analyticsState;
        }

        private BrowserState InitializeBrowserState(string stateDatabasePath)
        {
            BrowserState browserState;

            if (File.Exists(stateDatabasePath))
            {
                _stateDataSource.Connect(stateDatabasePath);
                browserState = _browserStateRepository.GetBrowserState() ?? AddNewBrowserState();
            }
            else
            {
                _stateDataSource.Create(stateDatabasePath);
                browserState = AddNewBrowserState();
            }

            browserState.Connections.StateConnection.FilePath = stateDatabasePath;
            return browserState;
        }

        private PlayerState InitializePlayerState(string stateDatabasePath)
        {
            PlayerState playerState;

            if (File.Exists(stateDatabasePath))
            {
                _stateDataSource.Connect(stateDatabasePath);
                playerState = _playerStateRepository.GetPlayerState() ?? AddNewPlayerState();
            }
            else
            {
                _stateDataSource.Create(stateDatabasePath);
                playerState = AddNewPlayerState();
            }

            playerState.Connections.StateConnection.FilePath = stateDatabasePath;
            return playerState;
        }

        private string GetStateDatabasePath()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appDataFolder, Properties.Settings.Default.Database_File);
        }

        private AnalyticsState AddNewAnalyticsState()
        {
            var analyticsState = new AnalyticsState();
            _analyticsStateRepository.AddAnalyticsState(analyticsState);
            _stateDataSource.Save();
            return analyticsState;
        }

        private BrowserState AddNewBrowserState()
        {
            var browserState = new BrowserState();
            _browserStateRepository.AddBrowserState(browserState);
            _stateDataSource.Save();
            return browserState;
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
