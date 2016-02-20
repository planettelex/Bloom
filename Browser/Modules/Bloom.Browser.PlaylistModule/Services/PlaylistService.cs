using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.PlaylistModule.ViewModels;
using Bloom.Browser.PlaylistModule.Views;
using Bloom.Browser.Controls;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.PlaylistModule.Services
{
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

        public void RestorePlaylistTab(Tab tab)
        {
            var playlist = new Playlist { Id = tab.EntityId }; // TODO: Make this data access call
            var playlistViewModel = new PlaylistViewModel(playlist, tab.Id);
            var playlistView = new PlaylistView(playlistViewModel);
            var playlistTab = new ViewMenuTab(tab, playlistView);

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }

        public void DuplicatePlaylistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var playlistId = existingTab.Tab.EntityId;
            var playlist = new Playlist { Id = playlistId }; // TODO: Make this data access call
            var playlistBuid = new Buid(existingTab.Tab.LibraryId, BloomEntity.Playlist, playlistId);
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
