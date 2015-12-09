using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    public class BrowserStateService : IBrowserStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserStateService"/> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="browserStateRepository">The browser state repository.</param>
        public BrowserStateService(IDataSource stateDataSource, IBrowserStateRepository browserStateRepository)
        {
            _stateDataSource = stateDataSource;
            _browserStateRepository = browserStateRepository;
        }
        private readonly IDataSource _stateDataSource;
        private readonly IBrowserStateRepository _browserStateRepository;

        /// <summary>
        /// Gets the state of the browser.
        /// </summary>
        public BrowserState BrowserState { get; private set; }

        /// <summary>
        /// Initializes the browser application state.
        /// </summary>
        /// <returns>
        /// The browser application state.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The file path to the state database file has not been specified.</exception>
        public BrowserState InitializeState()
        {
            if (string.IsNullOrEmpty(_stateDataSource.FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            if (File.Exists(_stateDataSource.FilePath))
            {
                _stateDataSource.Connect();
                BrowserState = _browserStateRepository.GetBrowserState() ?? AddNewBrowserState();
            }
            else
            {
                _stateDataSource.Create();
                BrowserState = AddNewBrowserState();
            }
            return BrowserState;
        }

        /// <summary>
        /// Adds a tab to state.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <exception cref="System.InvalidOperationException">Tabs cannot be added until state is initialized.</exception>
        public void AddTab(Tab tab)
        {
            if (BrowserState == null)
                throw new InvalidOperationException("Tabs cannot be added until state is initialized.");

            if (BrowserState.Tabs == null)
                BrowserState.Tabs = new List<Tab>();

            var stateTab = BrowserState.Tabs.SingleOrDefault(t => t.Id == tab.Id);
            if (stateTab == null)
                BrowserState.Tabs.Add(tab);

            BrowserState.SelectedTabId = tab.Id;
            _browserStateRepository.AddBrowserTab(tab);
        }

        /// <summary>
        /// Removes a tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public void RemoveTab(Guid tabId)
        {
            if (BrowserState == null || BrowserState.Tabs == null || BrowserState.Tabs.Count == 0)
                return;

            var tab = BrowserState.Tabs.SingleOrDefault(t => t.Id == tabId);
            if (tab == null)
                return;

            BrowserState.Tabs.Remove(tab);
            BrowserState.CondenseTabOrders();
            _browserStateRepository.RemoveBrowserTab(tab);
        }

        /// <summary>
        /// Removes all tabs except the specified tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public void RemoveAllTabsExcept(Guid tabId)
        {
            if (BrowserState == null || BrowserState.Tabs == null || BrowserState.Tabs.Count == 0)
                return;

            var exemptTab = BrowserState.Tabs.SingleOrDefault(t => t.Id == tabId);
            if (exemptTab == null)
                RemoveAllTabs();
            else
            {
                foreach (var tab in BrowserState.Tabs)
                    if (tab.Id != exemptTab.Id)
                        _browserStateRepository.RemoveBrowserTab(tab);

                exemptTab.Order = 1;
                BrowserState.Tabs = new List<Tab> { exemptTab };
            }
        }

        /// <summary>
        /// Removes all tabs from state.
        /// </summary>
        public void RemoveAllTabs()
        {
            if (BrowserState == null || BrowserState.Tabs == null || BrowserState.Tabs.Count == 0)
                return;

            foreach (var tab in BrowserState.Tabs)
                _browserStateRepository.RemoveBrowserTab(tab);

            BrowserState.Tabs = new List<Tab>();
        }

        private BrowserState AddNewBrowserState()
        {
            var browserState = new BrowserState();
            _browserStateRepository.AddBrowserState(browserState);
            _stateDataSource.Save();
            return browserState;
        }

        /// <summary>
        /// Saves the browser application state.
        /// </summary>
        public void SaveState()
        {
            _stateDataSource.Save();
        }
    }
}
