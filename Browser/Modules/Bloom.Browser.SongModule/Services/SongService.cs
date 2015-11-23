using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.SongModule.ViewModels;
using Bloom.Browser.SongModule.Views;
using Bloom.Browser.Controls;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.SongModule.Services
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
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewSongTabEvent>().Subscribe(NewSongTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateSongTab);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        public void NewSongTab(object nothing)
        {
            NewSongTab();
        }

        public void NewSongTab()
        {
            var songViewModel = new SongViewModel();
            var songView = new SongView(songViewModel);
            var tab = new Tab
            {
                Id = songViewModel.TabId,
                Type = TabType.Song,
                Header = "Song"
            };
            var songTab = new ViewMenuTab(tab, songView);

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }

        public void DuplicateSongTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var songViewModel = new SongViewModel();
            var songView = new SongView(songViewModel);
            var tab = new Tab
            {
                Id = songViewModel.TabId,
                Type = TabType.Song,
                Header = "Song"
            };
            var songTab = new ViewMenuTab(tab, songView);

            _tabs.Add(songTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }
    }
}
