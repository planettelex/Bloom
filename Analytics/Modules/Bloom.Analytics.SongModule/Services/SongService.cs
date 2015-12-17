using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Controls;
using Bloom.Analytics.SongModule.ViewModels;
using Bloom.Analytics.SongModule.Views;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Analytics.SongModule.Services
{
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
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewSongTabEvent>().Subscribe(NewSongTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateSongTab);

            State = (AnalyticsState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public AnalyticsState State { get; private set; }

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
            var song = new Song { Id = tab.EntityId }; // TODO: Make this data access call
            var songViewModel = new SongViewModel(song, tab.Id);
            var songView = new SongView(songViewModel);
            var songTab = new ViewMenuTab(tab, songView);

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        public void DuplicateSongTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var songId = existingTab.Tab.EntityId;
            var song = new Song { Id = songId }; // TODO: Make this data access call
            var tab = CreateNewTab(new Buid(existingTab.Tab.LibraryId, BloomEntity.Song, songId));
            var songViewModel = new SongViewModel(song, tab.Id);
            var songView = new SongView(songViewModel);
            var songTab = new ViewMenuTab(tab, songView);

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        private Tab CreateNewTab(Buid songBuid)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                Order = State.GetNextTabOrder(),
                Type = TabType.Song,
                Header = "Song",
                Process = ProcessType.Analytics,
                LibraryId = songBuid.LibraryId,
                EntityId = songBuid.EntityId
            };
        }
    }
}
