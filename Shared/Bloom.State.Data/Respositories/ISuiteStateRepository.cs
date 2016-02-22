using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for suite state data.
    /// </summary>
    public interface ISuiteStateRepository
    {
        /// <summary>
        /// Determines whether a suite state exists.
        /// </summary>
        bool SuiteStateExists();

        /// <summary>
        /// Gets the suite state.
        /// </summary>
        SuiteState GetSuiteState();

        /// <summary>
        /// Adds the suite state.
        /// </summary>
        /// <param name="suiteState">The suite state.</param>
        void AddSuiteState(SuiteState suiteState);
    }
}
