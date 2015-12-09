using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.AlbumModule.ViewModels;
using Bloom.Browser.AlbumModule.Views;
using Bloom.Browser.Common;
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
        /// <param name="albumId">The album identifier.</param>
        public void NewAlbumTab(Guid albumId)
        {
            const ViewType defaultViewType = ViewType.Grid;
            var album = new Album { Id = albumId }; // TODO: Make this data access call
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

        public void RestoreAlbumTab(Tab tab)
        {
            
        }

        /// <summary>
        /// Duplicates an album tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
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
