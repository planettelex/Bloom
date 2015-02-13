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
        /// Gets the analytics state.
        /// </summary>
        AnalyticsState GetAnalyticsState();

        /// <summary>
        /// Initializes the browser state.
        /// </summary>
        BrowserState InitializeBrowserState();

        /// <summary>
        /// Gets the browser state.
        /// </summary>
        BrowserState GetBrowserState();

        /// <summary>
        /// Initializes the player state.
        /// </summary>
        PlayerState InitializePlayerState();

        /// <summary>
        /// Gets the player state.
        /// </summary>
        PlayerState GetPlayerState();

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
