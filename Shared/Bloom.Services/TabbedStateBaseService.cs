using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.PubSubEvents;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;

namespace Bloom.Services
{
    /// <summary>
    /// Base class for all tabbed application state services.
    /// </summary>
    /// <seealso cref="Bloom.Services.StateBaseService" />
    public abstract class TabbedStateBaseService : StateBaseService
    {
        /// <summary>
        /// Gets or sets the tab repository.
        /// </summary>
        protected ITabRepository TabRepository { get; set; }

        /// <summary>
        /// Gets or sets the tabbed application state.
        /// </summary>
        protected new TabbedApplicationState State
        {
            get { return _state; }
            set
            {
                base.State = value;
                _state = value;
            }
        }
        private TabbedApplicationState _state;

        /// <summary>
        /// Adds a tab to state.
        /// </summary>
        /// <param name="tab">The tab.</param>
        /// <exception cref="System.ArgumentNullException">tab</exception>
        /// <exception cref="System.InvalidOperationException">Tabs cannot be added until state is initialized.</exception>
        public void AddTab(Tab tab)
        {
            if (tab == null)
                throw new ArgumentNullException("tab");

            if (State == null)
                throw new InvalidOperationException("Tabs cannot be added until state is initialized.");

            if (State.Tabs == null)
                State.Tabs = new List<Tab>();

            var stateTab = State.Tabs.SingleOrDefault(t => t.Id == tab.Id);
            if (stateTab == null)
                State.Tabs.Add(tab);

            TabRepository.AddTab(tab);
        }

        /// <summary>
        /// Removes a tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public void RemoveTab(Guid tabId)
        {
            if (State == null || !State.HasTabs())
                return;

            var tab = State.Tabs.SingleOrDefault(t => t.Id == tabId);
            if (tab == null)
                return;

            State.Tabs.Remove(tab);
            State.CondenseTabOrders();

            TabRepository.DeleteTab(tab);
        }

        /// <summary>
        /// Removes all tabs from state.
        /// </summary>
        public void RemoveAllTabs()
        {
            if (State == null || !State.HasTabs())
                return;

            foreach (var tab in State.Tabs)
                TabRepository.DeleteTab(tab);

            State.Tabs = new List<Tab>();
        }

        /// <summary>
        /// Removes all tabs except the specified tab from state.
        /// </summary>
        /// <param name="tabId">The tab identifier.</param>
        public void RemoveAllTabsExcept(Guid tabId)
        {
            if (State == null || !State.HasTabs())
                return;

            var exemptTab = State.Tabs.SingleOrDefault(t => t.Id == tabId);
            if (exemptTab == null)
                RemoveAllTabs();
            else
            {
                foreach (var tab in State.Tabs)
                    if (tab.Id != exemptTab.Id)
                        TabRepository.DeleteTab(tab);

                exemptTab.Order = 1;
                State.Tabs = new List<Tab> { exemptTab };
            }
        }

        /// <summary>
        /// Closes all tabs for a given library.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        public void CloseLibraryTabs(Guid libraryId)
        {
            if (State == null || !State.HasTabs())
                return;

            var libraryTabs = State.Tabs.Where(tab => tab.LibraryId == libraryId || 
                (tab.Libraries != null && tab.Libraries.Exists(tabLibrary => tabLibrary.LibraryId == libraryId))).ToList();

            foreach (var tab in libraryTabs)
                EventAggregator.GetEvent<CloseTabEvent>().Publish(tab.Id);
        }

        /// <summary>
        /// Restores the tabs from saved state.
        /// </summary>
        public void RestoreTabs()
        {
            if (State == null)
                return;

            if (State.User == null)
            {
                State.User = User.Anonymous;
                EventAggregator.GetEvent<NewGettingStartedTabEvent>().Publish(null);
            }
            else if (!State.HasTabs())
            {
                if (State.HasConnections())
                    EventAggregator.GetEvent<NewHomeTabEvent>().Publish(null);
                else
                    EventAggregator.GetEvent<NewGettingStartedTabEvent>().Publish(null);
            }
            else // Restore state tabs
            {
                foreach (var tab in State.Tabs)
                {
                    // Tabs that don't require any connections.
                    if (tab.Type == TabType.GettingStarted)
                    {
                        switch (tab.Type)
                        {
                            case TabType.GettingStarted:
                                EventAggregator.GetEvent<RestoreGettingStartedTabEvent>().Publish(tab);
                                break;
                        }
                    }
                    // Tabs that require any connection. 
                    else if ((tab.Type == TabType.Home || tab.Type == TabType.NewMusic) 
                        && State.HasConnections()) 
                    {
                        switch (tab.Type)
                        {
                            case TabType.Home:
                                EventAggregator.GetEvent<RestoreHomeTabEvent>().Publish(tab);
                                break;
                            case TabType.NewMusic:
                                EventAggregator.GetEvent<RestoreAddMusicTabEvent>().Publish(tab);
                                break;
                        }
                    }
                    // Tabs that reference a library entity.
                    else if (tab.LibraryId != null && State.IsConnected(tab.LibraryId.Value))
                    {
                        switch (tab.Type)
                        {
                            case TabType.Album:
                                EventAggregator.GetEvent<RestoreAlbumTabEvent>().Publish(tab);
                                break;
                            case TabType.Artist:
                                EventAggregator.GetEvent<RestoreArtistTabEvent>().Publish(tab);
                                break;
                            case TabType.Library:
                                EventAggregator.GetEvent<RestoreLibraryTabEvent>().Publish(tab);
                                break;
                            case TabType.Person:
                                EventAggregator.GetEvent<RestorePersonTabEvent>().Publish(tab);
                                break;
                            case TabType.Playlist:
                                EventAggregator.GetEvent<RestorePlaylistTabEvent>().Publish(tab);
                                break;
                            case TabType.Song:
                                EventAggregator.GetEvent<RestoreSongTabEvent>().Publish(tab);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the application state.
        /// </summary>
        public override void SaveState()
        {
            StateDataSource.Save();
        }
    }
}
