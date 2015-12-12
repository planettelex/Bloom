using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.Controls;
using Bloom.Browser.HomeModule.ViewModels;
using Bloom.Browser.HomeModule.Views;
using Bloom.Common;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.HomeModule.Services
{
    public class HomeService : IHomeService
    {
        public HomeService(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewHomeTabEvent>().Subscribe(NewHomeTab);
            _eventAggregator.GetEvent<RestoreHomeTabEvent>().Subscribe(RestoreHomeTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateHomeTab);

            State = (BrowserState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Creates a new home tab.
        /// </summary>
        public void NewHomeTab(object nothing)
        {
            NewHomeTab();
        }

        /// <summary>
        /// Creates a new home tab.
        /// </summary>
        public void NewHomeTab()
        {
            var tab = CreateNewTab();
            var homeViewModel = new HomeViewModel(tab.Id);
            var homeView = new HomeView(homeViewModel);
            var homeTab = new ViewMenuTab(tab, homeView);

            _tabs.Add(homeTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(homeTab);
        }

        /// <summary>
        /// Restores the home tab.
        /// </summary>
        /// <param name="tab">The home tab.</param>
        public void RestoreHomeTab(Tab tab)
        {
            var homeViewModel = new HomeViewModel(tab.Id);
            var homeView = new HomeView(homeViewModel);
            var homeTab = new ViewMenuTab(tab, homeView);

            _tabs.Add(homeTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(homeTab);
        }

        public void DuplicateHomeTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var tab = CreateNewTab();
            var homeViewModel = new HomeViewModel(tab.Id);
            var homeView = new HomeView(homeViewModel);
            var homeTab = new ViewMenuTab(tab, homeView);

            _tabs.Add(homeTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(homeTab);
        }

        private Tab CreateNewTab()
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                Order = State.GetNextTabOrder(),
                Type = TabType.Home,
                Header = "Home",
                Process = ProcessType.Browser
            };
        }
    }
}
