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
        /// <param name="user">The user.</param>
        bool AnalyticsStateExists(User user);

        /// <summary>
        /// Gets the analytics state.
        /// </summary>
        /// <param name="user">The user.</param>
        AnalyticsState GetAnalyticsState(User user);

        /// <summary>
        /// Adds the state of the analytics.
        /// </summary>
        /// <param name="analyticsState">State of the analytics.</param>
        void AddAnalyticsState(AnalyticsState analyticsState);
    }
}
