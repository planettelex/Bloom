using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.ArtistModule.ViewModels;
using Bloom.Browser.ArtistModule.Views;
using Bloom.Browser.Common;
using Bloom.Browser.Controls;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.ArtistModule.Services
{
    public class ArtistService : IArtistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        public ArtistService(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewArtistTabEvent>().Subscribe(NewArtistTab);
            _eventAggregator.GetEvent<RestoreArtistTabEvent>().Subscribe(RestoreArtistTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateArtistTab);
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

        public void NewArtistTab(Buid artistBuid)
        {
            const ViewType defaultViewType = ViewType.Grid;
            var artist = new Artist { Id = artistBuid.EntityId }; // TODO: Make this data access call
            var tab = CreateNewTab(artistBuid, defaultViewType);
            var artistViewModel = new ArtistViewModel(artist, defaultViewType, tab.Id);
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new ViewMenuTab(defaultViewType, tab, artistView);

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        public void RestoreArtistTab(Tab tab)
        {
            if (tab == null || tab.EntityId == null)
                return;

            var artist = new Artist { Id = tab.EntityId.Value }; // TODO: Make this data access call
            var viewType = (ViewType)Enum.Parse(typeof(ViewType), tab.View);
            var artistViewModel = new ArtistViewModel(artist, viewType, tab.Id);
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new ViewMenuTab(viewType, tab, artistView);

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        public void DuplicateArtistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var artistId = existingTab.Tab.EntityId;
            var libraryId = existingTab.Tab.LibraryId;
            if (artistId == null || libraryId == null)
                return;

            var artist = new Artist { Id = artistId.Value }; // TODO: Make this data access call
            var tab = CreateNewTab(new Buid(libraryId.Value, BloomEntity.Artist, artistId.Value), existingTab.ViewType);
            var artistViewModel = new ArtistViewModel(artist, existingTab.ViewType, tabId);
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new ViewMenuTab(artistViewModel.ViewType, tab, artistView);

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        private Tab CreateNewTab(Buid artistBuid, ViewType viewType)
        {
            return Tab.Create(ProcessType.Browser, State.User, artistBuid, State.GetNextTabOrder(), TabType.Artist, "Artist", viewType.ToString());
        }
    }
}
