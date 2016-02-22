using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for analytics state data.
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
        /// Adds the analytics state.
        /// </summary>
        /// <param name="analyticsState">The analytics state.</param>
        void AddAnalyticsState(AnalyticsState analyticsState);
    }
}
