using Bloom.State.Domain.Models;

namespace Bloom.Player.State.Services
{
    /// <summary>
    /// Service for managing the player application state.
    /// </summary>
    public interface IPlayerStateService
    {
        /// <summary>
        /// Initializes the player application state.
        /// </summary>
        /// <returns>The player application state.</returns>
        PlayerState InitializeState();

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
