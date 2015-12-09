using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.PlaylistModule.ViewModels;
using Bloom.Browser.PlaylistModule.Views;
using Bloom.Browser.Controls;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
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
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Subscribe(NewPlaylistTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicatePlaylistTab);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        public void NewPlaylistTab(Guid playlistId)
        {
            var playlistViewModel = new PlaylistViewModel();
            var playlistView = new PlaylistView(playlistViewModel);
            var tab = new Tab
            {
                Id = playlistViewModel.TabId,
                Type = TabType.Playlist,
                Header = "Playlist"
            };
            var playlistTab = new ViewMenuTab(tab, playlistView);

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }

        public void DuplicatePlaylistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var playlistViewModel = new PlaylistViewModel();
            var playlistView = new PlaylistView(playlistViewModel);
            var tab = new Tab
            {
                Id = playlistViewModel.TabId,
                Type = TabType.Playlist,
                Header = "Playlist"
            };
            var playlistTab = new ViewMenuTab(tab, playlistView);

            _tabs.Add(playlistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }
    }
}
