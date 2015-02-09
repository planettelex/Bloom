using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Common;
using Bloom.Analytics.Controls;
using Bloom.Analytics.Library.ViewModels;
using Bloom.Analytics.Library.Views;
using Bloom.Domain.Enums;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.Library.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<LibraryTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<LibraryTab> _tabs;

        public void NewLibraryTab(object nothing)
        {
            NewLibraryTab();
        }

        public void NewLibraryTab()
        {
            var libraryViewModel = new LibraryViewModel();
            var libraryView = new LibraryView(libraryViewModel);
            var libraryTab = new LibraryTab
            {
                Id = Guid.NewGuid(),
                EntityType = EntityType.Filterset,
                Header = "Library",
                Content = libraryView,
                ShowViewMenu = true,
                ViewType = LibraryViewType.Stats
            };

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void DuplicateLibraryTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var libraryViewModel = new LibraryViewModel();
            var libraryView = new LibraryView(libraryViewModel);
            var libraryTab = new LibraryTab
            {
                Id = Guid.NewGuid(),
                EntityType = EntityType.Filterset,
                Header = "Library",
                Content = libraryView
            };

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void ChangeLibraryTabView(Guid tabId, LibraryViewType viewType)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            existingTab.ViewType = viewType;
        }
    }
}
