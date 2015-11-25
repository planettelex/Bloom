using Bloom.State.Domain.Models;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    public interface IBrowserStateService
    {
        /// <summary>
        /// Initializes the browser application state.
        /// </summary>
        /// <returns>The browser application state.</returns>
        BrowserState InitializeState();

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
