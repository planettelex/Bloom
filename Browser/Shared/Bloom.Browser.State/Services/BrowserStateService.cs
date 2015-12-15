using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bloom.Data.Interfaces;
using Bloom.PubSubEvents;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.State.Services
{
    /// <summary>
    /// Service for managing the browser application state.
    /// </summary>
    public class BrowserStateService : IBrowserStateService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrowserStateService" /> class.
        /// </summary>
        /// <param name="stateDataSource">The state data source.</param>
        /// <param name="browserStateRepository">The browser state repository.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BrowserStateService(IDataSource stateDataSource, IBrowserStateRepository browserStateRepository, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _stateDataSource = stateDataSource;
            _browserStateRepository = browserStateRepository;
        }
        private readonly IDataSource _stateDataSource;
        private readonly IBrowserStateRepository _browserStateRepository;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Gets the state of the browser.
        /// </summary>
        public BrowserState State { get; private set; }

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
                State = _browserStateRepository.GetBrowserState() ?? AddNewState();
            }
            else
            {
                _stateDataSource.Create();
                State = AddNewState();
            }

            if (State.CurrentUser != null)
            {
                State.CurrentUser.LastLogin = DateTime.Now;
                if (State.Connections != null && State.Connections.Count > 0)
                    foreach (var libraryConnection in State.Connections)
                        libraryConnection.Connect(State.CurrentUser);
            }
                
            return State;
        }

        /// <summary>
        /// Adds a tab to state.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <exception cref="System.InvalidOperationException">Tabs cannot be added until state is initialized.</exception>
        public void AddTab(Tab tab)
        {
            if (State == null)
                throw new InvalidOperationException("Tabs cannot be added until state is initialized.");

            if (State.Tabs == null)
                State.Tabs = new List<Tab>();

            var stateTab = State.Tabs.SingleOrDefault(t => t.Id == tab.Id);
            if (stateTab == null)
                State.Tabs.Add(tab);

            _browserStateRepository.AddBrowserTab(tab);
        }

        /// <summary>
        /// Removes a tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public void RemoveTab(Guid tabId)
        {
            if (State == null || State.Tabs == null || State.Tabs.Count == 0)
                return;

            var tab = State.Tabs.SingleOrDefault(t => t.Id == tabId);
            if (tab == null)
                return;

            State.Tabs.Remove(tab);
            State.CondenseTabOrders();
            _browserStateRepository.RemoveBrowserTab(tab);
        }

        /// <summary>
        /// Removes all tabs except the specified tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public void RemoveAllTabsExcept(Guid tabId)
        {
            if (State == null || State.Tabs == null || State.Tabs.Count == 0)
                return;

            var exemptTab = State.Tabs.SingleOrDefault(t => t.Id == tabId);
            if (exemptTab == null)
                RemoveAllTabs();
            else
            {
                foreach (var tab in State.Tabs)
                    if (tab.Id != exemptTab.Id)
                        _browserStateRepository.RemoveBrowserTab(tab);

                exemptTab.Order = 1;
                State.Tabs = new List<Tab> { exemptTab };
            }
        }

        /// <summary>
        /// Removes all tabs from state.
        /// </summary>
        public void RemoveAllTabs()
        {
            if (State == null || State.Tabs == null || State.Tabs.Count == 0)
                return;

            foreach (var tab in State.Tabs)
                _browserStateRepository.RemoveBrowserTab(tab);

            State.Tabs = new List<Tab>();
        }

        /// <summary>
        /// Restores the tabs from saved state.
        /// </summary>
        public void RestoreTabs()
        {
            if (State == null || State.Tabs == null || State.Tabs.Count == 0)
                _eventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
            else
            {
                foreach (var tab in State.Tabs)
                {
                    // Only open tabs for connected libraries, except home which doesn't require one.
                    //if (tab.Type == TabType.GettingStarted || tab.Type == TabType.Home || State.IsConnected(tab.LibraryId))
                    { //TODO: Uncomment when connection management is set-up.
                        switch (tab.Type)
                        {
                            case TabType.Album:
                                _eventAggregator.GetEvent<RestoreAlbumTabEvent>().Publish(tab);
                                break;
                            case TabType.Artist:
                                _eventAggregator.GetEvent<RestoreArtistTabEvent>().Publish(tab);
                                break;
                            case TabType.Home:
                                _eventAggregator.GetEvent<RestoreHomeTabEvent>().Publish(tab);
                                break;
                            case TabType.Library:
                                _eventAggregator.GetEvent<RestoreLibraryTabEvent>().Publish(tab);
                                break;
                            case TabType.Person:
                                _eventAggregator.GetEvent<RestorePersonTabEvent>().Publish(tab);
                                break;
                            case TabType.Playlist:
                                _eventAggregator.GetEvent<RestorePlaylistTabEvent>().Publish(tab);
                                break;
                            case TabType.Song:
                                _eventAggregator.GetEvent<RestoreSongTabEvent>().Publish(tab);
                                break;
                            case TabType.GettingStarted:
                                _eventAggregator.GetEvent<RestoreGettingStartedTabEvent>().Publish(tab);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the browser application state.
        /// </summary>
        public void SaveState()
        {
            _stateDataSource.Save();
        }

        private BrowserState AddNewState()
        {
            var browserState = new BrowserState();
            _browserStateRepository.AddBrowserState(browserState);
            _stateDataSource.Save();
            return browserState;
        }
    }
}
