using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.State.Services
{
    /// <summary>
    /// Service for managing state.
    /// </summary>
    public interface IStateService
    {
        /// <summary>
        /// Initializes the state of a provided process.
        /// </summary>
        /// <param name="processType">The type of process.</param>
        IApplicationState InitializeState(ProcessType processType);

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
