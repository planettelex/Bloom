using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Controls;
using Bloom.Analytics.Modules.SongModule.ViewModels;
using Bloom.Analytics.Modules.SongModule.Views;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Analytics.Modules.SongModule.Services
{
    /// <summary>
    /// Service for analytics song operations.
    /// </summary>
    /// <seealso cref="ISongService" />
    public class SongService : ISongService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        public SongService(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewSongTabEvent>().Subscribe(NewSongTab);
            _eventAggregator.GetEvent<RestoreSongTabEvent>().Subscribe(RestoreSongTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateSongTab);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public AnalyticsState State { get; private set; }

        private void SetState(object nothing)
        {
            State = (AnalyticsState) _regionManager.Regions[Settings.DocumentRegion].Context;
        }

        public void NewSongTab(Buid songBuid)
        {
            var song = new Song { Id = songBuid.EntityId }; // TODO: Make this data access call
            var tab = CreateNewTab(songBuid);
            var songViewModel = new SongViewModel(song, tab.Id);
            var songView = new SongView(songViewModel);
            var songTab = new ViewMenuTab(tab, songView);

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        public void RestoreSongTab(Tab tab)
        {
            if (tab == null || tab.EntityId == null)
                return;

            var song = new Song { Id = tab.EntityId.Value }; // TODO: Make this data access call
            var songViewModel = new SongViewModel(song, tab.Id);
            var songView = new SongView(songViewModel);
            var songTab = new ViewMenuTab(tab, songView);

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        public void DuplicateSongTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var songId = existingTab.Tab.EntityId;
            var libraryId = existingTab.Tab.LibraryId;
            if (songId == null || libraryId == null)
                return;

            var song = new Song { Id = songId.Value }; // TODO: Make this data access call
            var tab = CreateNewTab(new Buid(libraryId.Value, BloomEntity.Song, songId.Value));
            var songViewModel = new SongViewModel(song, tab.Id);
            var songView = new SongView(songViewModel);
            var songTab = new ViewMenuTab(tab, songView);

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        private Tab CreateNewTab(Buid songBuid)
        {
            return Tab.Create(ProcessType.Analytics, State.User, songBuid, State.GetNextTabOrder(), TabType.Song, "Song");
        }
    }
}
