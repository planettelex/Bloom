using Bloom.State.Domain.Models;

namespace Bloom.Analytics.State.Services
{
    /// <summary>
    /// Service for managing the analytics application state.
    /// </summary>
    public interface IAnalyticsStateService
    {
        /// <summary>
        /// Initializes the analytics application state.
        /// </summary>
        /// <returns>The analytics application state.</returns>
        AnalyticsState InitializeState();

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
