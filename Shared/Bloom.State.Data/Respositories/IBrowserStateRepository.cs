using System;
using Bloom.State.Domain.Models;

namespace Bloom.State.Data.Respositories
{
    /// <summary>
    /// Repository for the browser state.
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
        /// Adds the state of the browser.
        /// </summary>
        /// <param name="browserState">State of the browser.</param>
        void AddBrowserState(BrowserState browserState);
    }
}
