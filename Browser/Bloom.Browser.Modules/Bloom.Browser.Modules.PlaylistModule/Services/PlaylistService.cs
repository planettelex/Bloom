using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.Controls;
using Bloom.Browser.Modules.PlaylistModule.ViewModels;
using Bloom.Browser.Modules.PlaylistModule.Views;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Prism.Events;
using Prism.Regions;

namespace Bloom.Browser.Modules.PlaylistModule.Services
{
    /// <summary>
    /// Service for browser playlist operations.
    /// </summary>
    /// <seealso cref="IPlaylistService" />
    public class PlaylistService : IPlaylistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        public PlaylistService(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Subscribe(NewPlaylistTab);
            _eventAggregator.GetEvent<RestorePlaylistTabEvent>().Subscribe(RestorePlaylistTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicatePlaylistTab);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        private void SetState(object nothing)
        {
            State = (BrowserState) _regionManager.Regions[Settings.DocumentRegion].Context;
        }

        /// <summary>
        /// Creates a new playlist tab.
        /// </summary>
        /// <param name="playlistBuid">The playlist Bloom identifier.</param>
        public void NewPlaylistTab(Buid playlistBuid)
        {
            var playlist = new Playlist { Id = playlistBuid.EntityId };
            var tab = CreateNewTab(playlistBuid);
            var playlistViewModel = new PlaylistViewModel(playlist, tab.Id);
            var playlistView = new PlaylistView(playlistViewModel);
            var playlistTab = new ViewMenuTab(tab, playlistView);

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }

        /// <summary>
        /// Restores the playlist tab.
        /// </summary>
        /// <param name="tab">The playlist tab.</param>
        public void RestorePlaylistTab(Tab tab)
        {
            if (tab?.EntityId == null)
                return;

            var playlist = new Playlist { Id = tab.EntityId.Value }; // TODO: Make this data access call
            var playlistViewModel = new PlaylistViewModel(playlist, tab.Id);
            var playlistView = new PlaylistView(playlistViewModel);
            var playlistTab = new ViewMenuTab(tab, playlistView);

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }

        /// <summary>
        /// Duplicates an playlist tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        public void DuplicatePlaylistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var playlistId = existingTab.Tab.EntityId;
            var libraryId = existingTab.Tab.LibraryId;
            if (playlistId == null || libraryId == null)
                return;

            var playlist = new Playlist { Id = playlistId.Value }; // TODO: Make this data access call
            var playlistBuid = new Buid(libraryId.Value, BloomEntity.Playlist, playlistId.Value);
            var tab = CreateNewTab(playlistBuid);
            var playlistViewModel = new PlaylistViewModel(playlist, tab.Id);
            var playlistView = new PlaylistView(playlistViewModel);
            var playlistTab = new ViewMenuTab(tab, playlistView);

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }

        private Tab CreateNewTab(Buid playlistBuid)
        {
            return Tab.Create(ProcessType.Browser, State.User, playlistBuid, State.GetNextTabOrder(), TabType.Playlist, "Playlist");
        }
    }
}
