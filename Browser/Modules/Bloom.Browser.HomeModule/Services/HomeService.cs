using System;
using Bloom.Browser.HomeModule.ViewModels;
using Bloom.Browser.HomeModule.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.HomeModule.Services
{
    public class HomeService : IHomeService
    {
        public HomeService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Subscribe to events
            _eventAggregator.GetEvent<NewHomeTabEvent>().Subscribe(NewHomeTab);
        }
        private readonly IEventAggregator _eventAggregator;

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
            var homeViewModel = new HomeViewModel { TabId = Guid.NewGuid() };
            var homeTab = new Tab
            {
                Id = homeViewModel.TabId,
                Type = TabType.Home,
                Header = "Home",
            };
            var homeView = new HomeView(homeViewModel);
            var homeTabControl = new TabControl(homeTab, homeView);

            _eventAggregator.GetEvent<AddTabEvent>().Publish(homeTabControl);
        }
    }
}
