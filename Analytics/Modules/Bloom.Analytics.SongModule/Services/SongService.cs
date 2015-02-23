using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.SongModule.ViewModels;
using Bloom.Analytics.SongModule.Views;
using Bloom.Controls;
using Bloom.Domain.Enums;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.SongModule.Services
{
    public class SongService : ISongService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public SongService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<Tab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewSongTabEvent>().Subscribe(NewSongTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateSongTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewSongTab(object nothing)
        {
            NewSongTab();
        }

        public void NewSongTab()
        {
            var songViewModel = new SongViewModel();
            var songView = new SongView(songViewModel);
            var songTab = new Tab
            {
                Id = Guid.NewGuid(),
                Type = TabType.Song,
                Header = "Song",
                Content = songView
            };

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        public void DuplicateSongTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var songViewModel = new SongViewModel();
            var songView = new SongView(songViewModel);
            var songTab = new Tab
            {
                Id = Guid.NewGuid(),
                Type = TabType.Song,
                Header = "Song",
                Content = songView
            };

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        private readonly List<Tab> _tabs;
    }
}
