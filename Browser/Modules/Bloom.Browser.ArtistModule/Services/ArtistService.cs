﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.ArtistModule.ViewModels;
using Bloom.Browser.ArtistModule.Views;
using Bloom.Browser.Common;
using Bloom.Browser.Controls;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
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
            var tab = new Tab
            {
                Id = artistViewModel.TabId,
                Type = TabType.Artist,
                Header = "Artist"
            };
            var artistTab = new ViewMenuTab(artistViewModel.ViewType, tab, artistView);

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }

        public void DuplicateArtistTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var artistViewModel = new ArtistViewModel(existingTab.ViewType);
            var artistView = new ArtistView(artistViewModel);
            var tab = new Tab
            {
                Id = artistViewModel.TabId,
                Type = TabType.Artist,
                Header = "Artist",
            };
            var artistTab = new ViewMenuTab(artistViewModel.ViewType, tab, artistView);

            _tabs.Add(artistTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }
    }
}
