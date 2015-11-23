using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Common;
using Bloom.Analytics.Controls;
using Bloom.Analytics.LibraryModule.ViewModels;
using Bloom.Analytics.LibraryModule.Views;
using Bloom.Analytics.PubSubEvents;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.LibraryModule.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        public void NewLibraryTab(object nothing)
        {
            NewLibraryTab();
        }

        public void NewLibraryTab()
        {
            var libraryViewModel = new LibraryViewModel(ViewType.Stats);
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
            var tab = new Tab
            {
                Id = libraryViewModel.TabId,
                Type = TabType.Album,
                Header = "Album"
            };
            var libraryTab = new ViewMenuTab(tab, libraryView);

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void DuplicateLibraryTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.Id == tabId);
            if (existingTab == null)
                return;

            var libraryViewModel = new LibraryViewModel(existingTab.ViewType);
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
            var tab = new Tab
            {
                Id = libraryViewModel.TabId,
                Type = TabType.Album,
                Header = "Album"
            };
            var libraryTab = new ViewMenuTab(tab, libraryView);

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void ChangeLibraryTabView(Tuple<Guid, ViewType> libraryViewTuple)
        {
            ChangeLibraryTabView(libraryViewTuple.Item1, libraryViewTuple.Item2);
        }

        public void ChangeLibraryTabView(Guid tabId, ViewType viewType)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            existingTab.ViewType = viewType;
        }
    }
}
