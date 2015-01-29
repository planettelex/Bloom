using Bloom.State.Data;
using Bloom.State.Domain.Models;

namespace Bloom.State.Services
{
    /// <summary>
    /// Service for managing state.
    /// </summary>
    public class StateService : IStateService
    {
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
