using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.Controls;
using Bloom.Browser.HomeModule.ViewModels;
using Bloom.Browser.HomeModule.Views;
using Bloom.Common;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Enums;
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
            _regionManager = regionManager;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewHomeTabEvent>().Subscribe(NewHomeTab);
            _eventAggregator.GetEvent<RestoreHomeTabEvent>().Subscribe(RestoreHomeTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateHomeTab);
            _eventAggregator.GetEvent<NewGettingStartedTabEvent>().Subscribe(NewGettingStartedTab);
            _eventAggregator.GetEvent<RestoreGettingStartedTabEvent>().Subscribe(RestoreGettingStartedTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateGettingStartedTab);
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
            var tab = CreateNewHomeTab();
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
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var tab = CreateNewHomeTab();
            var homeViewModel = new HomeViewModel(tab.Id);
            var homeView = new HomeView(homeViewModel);
            var homeTab = new ViewMenuTab(tab, homeView);

            _tabs.Add(homeTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(homeTab);
        }

        /// <summary>
        /// Creates a getting started tab.
        /// </summary>
        public void NewGettingStartedTab(object nothing)
        {
            NewGettingStartedTab();
        }

        public void NewGettingStartedTab()
        {
            var tab = CreateNewGettingStartedTab();
            var gettingStartedViewModel = new GettingStartedViewModel(tab.Id);
            var gettingStartedView = new GettingStartedView(gettingStartedViewModel);
            var gettingStartedTab = new ViewMenuTab(tab, gettingStartedView);

            _tabs.Add(gettingStartedTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(gettingStartedTab);
        }

        public void RestoreGettingStartedTab(Tab tab)
        {
            var gettingStartedViewModel = new GettingStartedViewModel(tab.Id);
            var gettingStartedView = new GettingStartedView(gettingStartedViewModel);
            var gettingStartedTab = new ViewMenuTab(tab, gettingStartedView);

            _tabs.Add(gettingStartedTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(gettingStartedTab);
        }

        public void DuplicateGettingStartedTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var tab = CreateNewGettingStartedTab();
            var gettingStartedViewModel = new GettingStartedViewModel(tab.Id);
            var gettingStartedView = new GettingStartedView(gettingStartedViewModel);
            var gettingStartedTab = new ViewMenuTab(tab, gettingStartedView);

            _tabs.Add(gettingStartedTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(gettingStartedTab);
        }

        private Tab CreateNewHomeTab()
        {
            var libraryIds = State.Connections.Select(libraryConnection => libraryConnection.LibraryId).ToList();
            return Tab.Create(ProcessType.Browser, State.User, libraryIds, State.GetNextTabOrder(), TabType.Home, "Home");
        }

        private Tab CreateNewGettingStartedTab()
        {
            return Tab.Create(ProcessType.Browser, State.User, Buid.Empty, State.GetNextTabOrder(), TabType.GettingStarted, "Getting Started");
        }
    }
}
