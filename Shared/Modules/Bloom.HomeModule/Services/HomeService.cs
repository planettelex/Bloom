using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bloom.Controls;
using Bloom.HomeModule.ViewModels;
using Bloom.HomeModule.Views;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.HomeModule.Services
{
    public class HomeService : IHomeService
    {
        public HomeService(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;

            // Subscribe to events
            _eventAggregator.GetEvent<NewHomeTabEvent>().Subscribe(NewHomeTab);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;

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
            var homeView = new HomeView(homeViewModel);
            var homeTab = new Tab
            {
                Id = homeViewModel.TabId,
                Type = TabType.Home,
                Header = "Home",
                Content = homeView
            };

            _eventAggregator.GetEvent<AddTabEvent>().Publish(homeTab);
        }
    }
}
