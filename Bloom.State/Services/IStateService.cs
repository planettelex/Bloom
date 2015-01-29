using Bloom.State.Domain.Models;

namespace Bloom.State.Services
{
    /// <summary>
    /// Service for managing state.
    /// </summary>
    public interface IStateService
    {
        /// <summary>
        /// Initializes the analytics state.
        /// </summary>
        AnalyticsState InitializeAnalyticsState();

        /// <summary>
        /// Initializes the browser state.
        /// </summary>
        BrowserState InitializeBrowserState();

        /// <summary>
        /// Initializes the player state.
        /// </summary>
        PlayerState InitializePlayerState();

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
