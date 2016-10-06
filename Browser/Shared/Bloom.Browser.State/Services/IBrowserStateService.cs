using System;
using Bloom.Common;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    public interface IBrowserStateService
    {
        /// <summary>
        /// Connects the data source.
        /// </summary>
        void ConnectDataSource();        

        /// <summary>
        /// Initializes the browser application state for a given user.
        /// </summary>
        /// <param name="user">The user.</param>
        BrowserState InitializeState(User user);

        /// <summary>
        /// Removes the anonymous browser state if the current user isn't the anonymous user.
        /// </summary>
        void RemoveAnonymousState();

        /// <summary>
        /// Get the last process to access state.
        /// </summary>
        ProcessType LastProcessToAccessState();

        /// <summary>
        /// Changes the process using state.
        /// </summary>
        /// <param name="processType">The process type using state.</param>
        void ChangeStateProcess(ProcessType processType);

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
