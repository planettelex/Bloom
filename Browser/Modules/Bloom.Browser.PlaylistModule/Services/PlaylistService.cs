using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.PlaylistModule.ViewModels;
using Bloom.Browser.PlaylistModule.Views;
using Bloom.Controls;
using Bloom.Domain.Enums;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.PlaylistModule.Services
{
    public class PlaylistService : IPlaylistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PlaylistService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<Tab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Subscribe(NewPlaylistTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicatePlaylistTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewPlaylistTab(object nothing)
        {
            NewPlaylistTab();
        }

        public void NewPlaylistTab()
        {
            var playlistViewModel = new PlaylistViewModel();
            var playlistView = new PlaylistView(playlistViewModel);
            var playlistTab = new Tab
            {
                Id = Guid.NewGuid(),
                Type = TabType.Playlist,
                Header = "Playlist",
                Content = playlistView
            };

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }

        public void DuplicatePlaylistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var playlistViewModel = new PlaylistViewModel();
            var playlistView = new PlaylistView(playlistViewModel);
            var playlistTab = new Tab
            {
                Id = Guid.NewGuid(),
                Type = TabType.Playlist,
                Header = "Playlist",
                Content = playlistView
            };

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }

        private readonly List<Tab> _tabs;
    }
}
