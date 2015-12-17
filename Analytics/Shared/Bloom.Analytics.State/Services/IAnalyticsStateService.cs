using System;
using Bloom.State.Domain.Models;

namespace Bloom.Analytics.State.Services
{
    /// <summary>
    /// Service for managing the analytics application state.
    /// </summary>
    public interface IAnalyticsStateService
    {
        /// <summary>
        /// Connects the data source.
        /// </summary>
        void ConnectDataSource();

        /// <summary>
        /// Initializes the analytics application state.
        /// </summary>
        /// <returns>The analytics application state.</returns>
        AnalyticsState InitializeState(User user);

        /// <summary>
        /// Restores the tabs from saved state.
        /// </summary>
        void RestoreTabs();

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
