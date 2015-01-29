using System;
using System.IO;
using Bloom.State.Data;
using Bloom.State.Domain.Models;

namespace Bloom.State.Services
{
    /// <summary>
    /// Service for managing state.
    /// </summary>
    public class StateService : IStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StateService"/> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        public StateService(IStateDataSource stateDataSource)
        {
            _stateDataSource = stateDataSource;
        }
        private readonly IStateDataSource _stateDataSource;

        public AnalyticsState InitializeAnalyticsState()
        {
            return new AnalyticsState();
        }

        public BrowserState InitializeBrowserState()
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var stateDatabasePath = Path.Combine(appDataFolder, Properties.Settings.Default.Database_File);

            if (!File.Exists(stateDatabasePath))
                _stateDataSource.Create(stateDatabasePath);

            return new BrowserState();
        }

        public PlayerState InitializePlayerState()
        {
            return new PlayerState();
        }

        public void SaveState()
        {
            _stateDataSource.Save();
        }
    }
}
