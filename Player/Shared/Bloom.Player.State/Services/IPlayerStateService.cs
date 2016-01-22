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
        /// Initializes the player application state.
        /// </summary>
        /// <returns>The player application state.</returns>
        PlayerState InitializeState(User user);

        ProcessType LastProcessToAccessState();

        void ChangeStateProcess(ProcessType processType);

        void RefreshStateOf(object toRefresh);

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
