using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Player.State.Services
{
    /// <summary>
    /// Service for managing the player application state.
    /// </summary>
    public interface IPlayerStateService
    {
        /// <summary>
        /// Connects the data source.
        /// </summary>
        void ConnectDataSource();

        /// <summary>
        /// Initializes the player application state for a given user.
        /// </summary>
        /// <param name="user">The user.</param>
        PlayerState InitializeState(User user);

        /// <summary>
        /// Gets the last process to access state.
        /// </summary>
        ProcessType LastProcessToAccessState();

        /// <summary>
        /// Changes the running process in state data.
        /// </summary>
        /// <param name="processType">The process type.</param>
        void ChangeStateProcess(ProcessType processType);

        /// <summary>
        /// Saves the application state.
        /// </summary>
        void SaveState();
    }
}
