using System;
using System.IO;
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
        /// Initializes the analytics state.
        /// </summary>
        public AnalyticsState InitializeAnalyticsState()
        {
            AnalyticsState analyticsState;
            var stateDatabasePath = GetStateDatabasePath();

            if (File.Exists(stateDatabasePath))
            {
                _stateDataSource.Connect(stateDatabasePath);
                analyticsState = _analyticsStateRepository.GetAnalyticsState() ?? AddNewAnalyticsState(stateDatabasePath);
            }
            else
            {
                _stateDataSource.Create(stateDatabasePath);
                analyticsState = AddNewAnalyticsState(stateDatabasePath);
            }

            return analyticsState;
        }

        /// <summary>
        /// Gets the analytics state.
        /// </summary>
        public AnalyticsState GetAnalyticsState()
        {
            if (!_stateDataSource.IsConnected())
                return null;

            return _analyticsStateRepository.GetAnalyticsState();
        }

        /// <summary>
        /// Initializes the browser state.
        /// </summary>
        public BrowserState InitializeBrowserState()
        {
            BrowserState browserState;
            var stateDatabasePath = GetStateDatabasePath();

            if (File.Exists(stateDatabasePath))
            {
                _stateDataSource.Connect(stateDatabasePath);
                browserState = _browserStateRepository.GetBrowserState() ?? AddNewBrowserState(stateDatabasePath);
            }
            else
            {
                _stateDataSource.Create(stateDatabasePath);
                browserState = AddNewBrowserState(stateDatabasePath);
            }

            browserState.ConnectLibraryDataSources();
            return browserState;
        }

        /// <summary>
        /// Gets the browser state.
        /// </summary>
        public BrowserState GetBrowserState()
        {
            if (!_stateDataSource.IsConnected())
                return null;

            return _browserStateRepository.GetBrowserState();
        }

        /// <summary>
        /// Initializes the player state.
        /// </summary>
        /// <returns></returns>
        public PlayerState InitializePlayerState()
        {
            PlayerState playerState;
            var stateDatabasePath = GetStateDatabasePath();

            if (File.Exists(stateDatabasePath))
            {
                _stateDataSource.Connect(stateDatabasePath);
                playerState = _playerStateRepository.GetPlayerState() ?? AddNewPlayerState(stateDatabasePath);
            }
            else
            {
                _stateDataSource.Create(stateDatabasePath);
                playerState = AddNewPlayerState(stateDatabasePath);
            }

            return playerState;
        }

        /// <summary>
        /// Gets the player state.
        /// </summary>
        public PlayerState GetPlayerState()
        {
            if (!_stateDataSource.IsConnected())
                return null;

            return _playerStateRepository.GetPlayerState();
        }

        /// <summary>
        /// Saves the state.
        /// </summary>
        public void SaveState()
        {
            _stateDataSource.Save();
        }

        private string GetStateDatabasePath()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(appDataFolder, Properties.Settings.Default.Database_File);
        }

        private AnalyticsState AddNewAnalyticsState(string stateDatabasePath)
        {
            var analyticsState = new AnalyticsState();
            analyticsState.Connections.StateConnection.FilePath = stateDatabasePath;
            _analyticsStateRepository.AddAnalyticsState(analyticsState);
            _stateDataSource.Save();
            return analyticsState;
        }

        private BrowserState AddNewBrowserState(string stateDatabasePath)
        {
            var browserState = new BrowserState();
            browserState.Connections.StateConnection.FilePath = stateDatabasePath;
            _browserStateRepository.AddBrowserState(browserState);
            _stateDataSource.Save();
            return browserState;
        }

        private PlayerState AddNewPlayerState(string stateDatabasePath)
        {
            var playerState = new PlayerState();
            playerState.Connections.StateConnection.FilePath = stateDatabasePath;
            _playerStateRepository.AddPlayerState(playerState);
            _stateDataSource.Save();
            return playerState;
        }
    }
}
