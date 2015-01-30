using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the analytics state.
    /// </summary>
    public interface IAnalyticsStateRepository
    {
        /// <summary>
        /// Determines whether the analytics state exists.
        /// </summary>
        /// <returns></returns>
        bool AnalyticsStateExists();

        /// <summary>
        /// Gets the analytics state.
        /// </summary>
        AnalyticsState GetAnalyticsState();

        /// <summary>
        /// Adds the state of the analytics.
        /// </summary>
        /// <param name="analyticsState">State of the analytics.</param>
        void AddAnalyticsState(AnalyticsState analyticsState);
    }
}
