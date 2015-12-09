using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.AlbumModule.ViewModels;
using Bloom.Analytics.AlbumModule.Views;
using Bloom.Analytics.Controls;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.AlbumModule.Services
{
    public class AlbumService : IAlbumService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public AlbumService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewAlbumTabEvent>().Subscribe(NewAlbumTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateAlbumTab);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        public void NewAlbumTab(Guid albumId)
        {
            var albumViewModel = new AlbumViewModel();
            var albumView = new AlbumView(albumViewModel);
            var tab = new Tab
            {
                Id = albumViewModel.TabId,
                Type = TabType.Album,
                Header = "Album"
            };
            var albumTab = new ViewMenuTab(tab, albumView);

            _tabs.Add(albumTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(albumTab);
        }

        public void DuplicateAlbumTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var albumViewModel = new AlbumViewModel();
            var albumView = new AlbumView(albumViewModel);
            var tab = new Tab
            {
                Id = albumViewModel.TabId,
                Type = TabType.Album,
                Header = "Album"
            };
            var albumTab = new ViewMenuTab(tab, albumView);

            _tabs.Add(albumTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(albumTab);
        }
    }
}
