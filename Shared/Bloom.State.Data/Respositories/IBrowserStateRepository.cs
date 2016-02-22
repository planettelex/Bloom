using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Access methods for browser state data.
    /// </summary>
    public interface IBrowserStateRepository
    {
        /// <summary>
        /// Determines whether the browser state exists.
        /// </summary>
        /// <param name="user">The user.</param>
        bool BrowserStateExists(User user);

        /// <summary>
        /// Gets the browser state.
        /// </summary>
        /// <param name="user">The user.</param>
        BrowserState GetBrowserState(User user);

        /// <summary>
        /// Adds the browser state.
        /// </summary>
        /// <param name="browserState">The browser state.</param>
        void AddBrowserState(BrowserState browserState);
    }
}
