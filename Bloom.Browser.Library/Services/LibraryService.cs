using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Browser.Common;
using Bloom.Browser.Controls;
using Bloom.Browser.Library.ViewModels;
using Bloom.Browser.Library.Views;
using Bloom.Browser.PubSubEvents;
using Bloom.Domain.Enums;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Library.Services
{
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public LibraryService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<LibraryTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<LibraryTab> _tabs;

        public void NewLibraryTab(object nothing)
        {
            NewLibraryTab();
        }

        public void NewLibraryTab()
        {
            var libraryViewModel = new LibraryViewModel(LibraryViewType.Grid)
            {
                TabId = Guid.NewGuid()
            };
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
            var libraryTab = new LibraryTab
            {
                Id = libraryViewModel.TabId,
                EntityType = EntityType.Filterset,
                Header = "Library",
                Content = libraryView,
                ShowViewMenu = true,
                ViewType = libraryViewModel.ViewType
            };

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void DuplicateLibraryTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var libraryViewModel = new LibraryViewModel(existingTab.ViewType)
            {
                TabId = Guid.NewGuid()
            };
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
            var libraryTab = new LibraryTab
            {
                Id = libraryViewModel.TabId,
                EntityType = EntityType.Filterset,
                Header = "Library",
                Content = libraryView,
                ShowViewMenu = true,
                ViewType = libraryViewModel.ViewType
            };

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void ChangeLibraryTabView(Tuple<Guid, LibraryViewType> libraryViewTuple)
        {
            ChangeLibraryTabView(libraryViewTuple.Item1, libraryViewTuple.Item2);
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
