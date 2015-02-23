using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.ArtistModule.ViewModels;
using Bloom.Analytics.ArtistModule.Views;
using Bloom.Controls;
using Bloom.Domain.Enums;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.ArtistModule.Services
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
            _tabs = new List<Tab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewArtistTabEvent>().Subscribe(NewArtistTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateArtistTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewArtistTab(object nothing)
        {
            NewArtistTab();
        }

        public void NewArtistTab()
        {
            var artistViewModel = new ArtistViewModel();
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new Tab
            {
                Id = Guid.NewGuid(),
                Type = TabType.Artist,
                Header = "Artist",
                Content = artistView
            };

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        public void DuplicateArtistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var artistViewModel = new ArtistViewModel();
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new Tab
            {
                Id = Guid.NewGuid(),
                Type = TabType.Artist,
                Header = "Artist",
                Content = artistView
            };

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        private readonly List<Tab> _tabs;
    }
}
