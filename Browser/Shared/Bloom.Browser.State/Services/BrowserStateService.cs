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
        /// <param name="tabRepository">The tab repository.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        public BrowserStateService(IDataSource stateDataSource, IBrowserStateRepository browserStateRepository, ITabRepository tabRepository, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _stateDataSource = stateDataSource;
            _browserStateRepository = browserStateRepository;
            _tabRepository = tabRepository;
        }
        private readonly IDataSource _stateDataSource;
        private readonly IBrowserStateRepository _browserStateRepository;
        private readonly ITabRepository _tabRepository;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Gets the state of the browser.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Connects the data source.
        /// </summary>
        public void ConnectDataSource()
        {
            if (string.IsNullOrEmpty(_stateDataSource.FilePath))
                throw new InvalidOperationException("The file path to the state database file has not been specified.");

            if (File.Exists(_stateDataSource.FilePath))
                _stateDataSource.Connect();
            else
                _stateDataSource.Create();
        }

        /// <summary>
        /// Initializes the browser application state.
        /// </summary>
        /// <param name="user">The user.</param>
        public BrowserState InitializeState(User user)
        { 
            State = _browserStateRepository.GetBrowserState(user) ?? AddNewState(user);

            if (State.User == null) 
                return State;

            State.User.LastLogin = DateTime.Now;
            if (State.Connections == null || State.Connections.Count <= 0) 
                return State;
            
            foreach (var libraryConnection in State.Connections)
                libraryConnection.Connect(State.User);

            return State;
        }

        /// <summary>
        /// Adds a tab to state.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <exception cref="System.InvalidOperationException">Tabs cannot be added until state is initialized.</exception>
        public void AddTab(Tab tab)
        {
            if (tab == null)
                throw new ArgumentNullException("tab");

            if (State == null)
                throw new InvalidOperationException("Tabs cannot be added until state is initialized.");

            if (State.Tabs == null)
                State.Tabs = new List<Tab>();

            tab.UserId = State.UserId;

            var stateTab = State.Tabs.SingleOrDefault(t => t.Id == tab.Id);
            if (stateTab == null)
                State.Tabs.Add(tab);

            _tabRepository.AddTab(tab);
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

            _tabRepository.RemoveTab(tab);
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
                        _tabRepository.RemoveTab(tab);

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
                _tabRepository.RemoveTab(tab);

            State.Tabs = new List<Tab>();
        }

        /// <summary>
        /// Restores the tabs from saved state.
        /// </summary>
        public void RestoreTabs()
        {
            if (State == null)
                return;

            if (State.User == null)
                _eventAggregator.GetEvent<NewGettingStartedTabEvent>().Publish(null);
            else if (State.Tabs == null || State.Tabs.Count == 0)
                _eventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
            else
            {
                foreach (var tab in State.Tabs)
                {
                    // Only open tabs for connected libraries, except home and getting started tabs which doesn't require one.
                    if (tab.Type == TabType.GettingStarted || tab.Type == TabType.Home || State.IsConnected(tab.LibraryId))
                    {
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
            if (State.User != null)
                _stateDataSource.Save();
        }

        private BrowserState AddNewState(User user)
        {
            var browserState = new BrowserState();
            browserState.SetUser(user);
            _browserStateRepository.AddBrowserState(browserState);
            return browserState;
        }
    }
}
