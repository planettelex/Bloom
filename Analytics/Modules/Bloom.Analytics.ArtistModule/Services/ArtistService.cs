using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.ArtistModule.ViewModels;
using Bloom.Analytics.ArtistModule.Views;
using Bloom.Analytics.Common;
using Bloom.Analytics.Controls;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Analytics.ArtistModule.Services
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
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewArtistTabEvent>().Subscribe(NewArtistTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateArtistTab);

            State = (AnalyticsState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public AnalyticsState State { get; private set; }

        public void NewArtistTab(Buid artistBuid)
        {
            const ViewType defaultViewType = ViewType.Stats;
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
            var artist = new Artist { Id = tab.EntityId }; // TODO: Make this data access call
            var viewType = (ViewType)Enum.Parse(typeof(ViewType), tab.View);
            var artistViewModel = new ArtistViewModel(artist, viewType, tab.Id);
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new ViewMenuTab(viewType, tab, artistView);

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        public void DuplicateArtistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var artistId = existingTab.Tab.EntityId;
            var artist = new Artist { Id = artistId }; // TODO: Make this data access call
            var tab = CreateNewTab(new Buid(existingTab.Tab.LibraryId, BloomEntity.Artist, artistId), existingTab.ViewType);
            var artistViewModel = new ArtistViewModel(artist, existingTab.ViewType, tabId);
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new ViewMenuTab(artistViewModel.ViewType, tab, artistView);

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        private Tab CreateNewTab(Buid artistBuid, ViewType viewType)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                Order = State.GetNextTabOrder(),
                Type = TabType.Artist,
                Header = "Artist",
                Process = ProcessType.Analytics,
                LibraryId = artistBuid.LibraryId,
                EntityId = artistBuid.EntityId,
                View = viewType.ToString()
            };
        }
    }
}
