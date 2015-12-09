using System;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    public interface IBrowserStateService
    {
        /// <summary>
        /// Initializes the browser application state.
        /// </summary>
        /// <returns>The browser application state.</returns>
        BrowserState InitializeState();

        /// <summary>
        /// Adds a tab to state.
        /// </summary>
        /// <param name="tab">The tab.</param>
        void AddTab(Tab tab);

        /// <summary>
        /// Removes a tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        void RemoveTab(Guid tabId);

        /// <summary>
        /// Removes all tabs except the specified tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        void RemoveAllTabsExcept(Guid tabId);

        /// <summary>
        /// Removes all tabs from state.
        /// </summary>
        void RemoveAllTabs();

        /// <summary>
        /// Saves the state.
        /// </summary>
        void SaveState();
    }
}
