using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the analytics state.
    /// </summary>
    public interface IAnalyticsStateRepository
    {
        /// <summary>
        /// Gets the analytics state.
        /// </summary>
        AnalyticsState GetAnalyticsState();
    }
}
