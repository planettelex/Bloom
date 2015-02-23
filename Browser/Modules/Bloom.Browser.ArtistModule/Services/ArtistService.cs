using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.ArtistModule.ViewModels;
using Bloom.Browser.ArtistModule.Views;
using Bloom.Browser.Common;
using Bloom.Browser.Controls;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.ArtistModule.Services
{
    public class ArtistService : IArtistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ArtistService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewArtistTabEvent>().Subscribe(NewArtistTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateArtistTab);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        public void NewArtistTab(object nothing)
        {
            NewArtistTab();
        }

        public void NewArtistTab()
        {
            var artistViewModel = new ArtistViewModel(ViewType.Grid);
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new ViewMenuTab
            {
                Id = artistViewModel.TabId,
                Type = TabType.Artist,
                Header = "Artist",
                Content = artistView,
                ShowViewMenu = true,
                ViewType = artistViewModel.ViewType
            };

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        public void DuplicateArtistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var artistViewModel = new ArtistViewModel(existingTab.ViewType);
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new ViewMenuTab
            {
                Id = artistViewModel.TabId,
                Type = TabType.Artist,
                Header = "Artist",
                Content = artistView,
                ShowViewMenu = true,
                ViewType = artistViewModel.ViewType
            };

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }
    }
}
