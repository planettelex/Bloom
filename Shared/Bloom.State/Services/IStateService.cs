using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.State.Services
{
    /// <summary>
    /// Service for managing state.
    /// </summary>
    public interface IStateService
    {
        BloomState InitializeState(ProcessType processType);

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
