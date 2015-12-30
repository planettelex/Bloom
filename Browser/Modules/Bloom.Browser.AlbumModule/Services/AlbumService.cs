using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.AlbumModule.ViewModels;
using Bloom.Browser.AlbumModule.Views;
using Bloom.Browser.Controls;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.AlbumModule.Services
{
    public class AlbumService : IAlbumService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        public AlbumService(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewAlbumTabEvent>().Subscribe(NewAlbumTab);
            _eventAggregator.GetEvent<RestoreAlbumTabEvent>().Subscribe(RestoreAlbumTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateAlbumTab);

            State = (BrowserState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Creates a new album tab.
        /// </summary>
        /// <param name="albumBuid">The album Bloom identifier.</param>
        public void NewAlbumTab(Buid albumBuid)
        {
            var album = new Album { Id = albumBuid.EntityId }; // TODO: Make this data access call
            var tab = CreateNewTab(albumBuid);
            var albumViewModel = new AlbumViewModel(album, tab.Id);
            var albumView = new AlbumView(albumViewModel);
            var albumTab = new ViewMenuTab(tab, albumView);

            _tabs.Add(albumTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(albumTab);
        }

        /// <summary>
        /// Restores the album tab.
        /// </summary>
        /// <param name="tab">The album tab.</param>
        public void RestoreAlbumTab(Tab tab)
        {
            var album = new Album { Id = tab.EntityId }; // TODO: Make this data access call
            var albumViewModel = new AlbumViewModel(album, tab.Id);
            var albumView = new AlbumView(albumViewModel);
            var albumTab = new ViewMenuTab(tab, albumView);

            _tabs.Add(albumTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(albumTab);
        }

        /// <summary>
        /// Duplicates an album tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        public void DuplicateAlbumTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var albumId = existingTab.Tab.EntityId;
            var album = new Album { Id = albumId }; // TODO: Make this data access call
            var tab = CreateNewTab(new Buid(existingTab.Tab.LibraryId, BloomEntity.Album, albumId));
            var albumViewModel = new AlbumViewModel(album, tab.Id);
            var albumView = new AlbumView(albumViewModel);
            var albumTab = new ViewMenuTab(tab, albumView);

            _tabs.Add(albumTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(albumTab);
        }

        private Tab CreateNewTab(Buid albumBuid)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                Order = State.GetNextTabOrder(),
                Type = TabType.Album,
                Header = "Album",
                Process = ProcessType.Browser,
                LibraryId = albumBuid.LibraryId,
                EntityId = albumBuid.EntityId
            };
        }
    }
}
