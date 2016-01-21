using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Bloom.Analytics.Common;
using Bloom.Analytics.Controls;
using Bloom.Analytics.LibraryModule.ViewModels;
using Bloom.Analytics.LibraryModule.Views;
using Bloom.Analytics.PubSubEvents;
using Bloom.Common;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.LibraryModule.WindowModels;
using Bloom.LibraryModule.Windows;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Analytics.LibraryModule.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(IEventAggregator eventAggregator, IRegionManager regionManager, ISharedLibraryService sharedLibraryService, ILibraryRepository libraryRepository)
        {
            _eventAggregator = eventAggregator;
            _libraryRepository = libraryRepository;
            _regionManager = regionManager;
            _sharedLibraryService = sharedLibraryService;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<ShowConnectedLibrariesModalEvent>().Subscribe(ShowConnectedLibrariesModal);
            _eventAggregator.GetEvent<RestoreLibraryTabEvent>().Subscribe(RestoreLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly ILibraryRepository _libraryRepository;
        private readonly ISharedLibraryService _sharedLibraryService;
        private readonly IRegionManager _regionManager;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public AnalyticsState State { get; private set; }

        private void SetState(object nothing)
        {
            State = (AnalyticsState) _regionManager.Regions[Settings.DocumentRegion].Context;
        }

        public void ShowConnectedLibrariesModal(object nothing)
        {
            ShowConnectedLibrariesModal();
        }

        public void ShowConnectedLibrariesModal()
        {
            var connectedLibrariesWindowModel = new ConnectedLibrariesWindowModel(_regionManager);
            connectedLibrariesWindowModel.LibraryConnections = new ObservableCollection<LibraryConnection>();
            var allConnections = _sharedLibraryService.ListLibraryConnections();
            foreach (var connection in allConnections)
            {
                if (connection.IsConnected)
                {
                    var connectedLibrary = State.Connections.SingleOrDefault(c => c.LibraryId == connection.LibraryId);
                    if (connectedLibrary == null)
                        connection.IsConnected = false;
                    else
                        connection.DataSource = connectedLibrary.DataSource;
                }
                connection.SetButtonVisibilities();
            }
            connectedLibrariesWindowModel.LibraryConnections.AddRange(allConnections);
            var connectedLibrariesWindow = new ConnectedLibrariesWindow(connectedLibrariesWindowModel, _eventAggregator, _sharedLibraryService)
            {
                Owner = Application.Current.MainWindow
            };
            connectedLibrariesWindow.ShowDialog();
        }

        public void ShowLibraryPropertiesModal(Library library)
        {

        }

        public void NewLibraryTab(Guid libraryId)
        {
            const ViewType defaultViewType = ViewType.Stats;
            var datasource = State.GetConnectionData(libraryId);
            if (datasource == null)
                throw new NullReferenceException("Library data source cannot be null.");

            var library = _libraryRepository.GetLibrary(datasource);
            var tab = CreateNewTab(library, defaultViewType);
            var libraryViewModel = new LibraryViewModel(library, defaultViewType, tab.Id);
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
            var libraryTab = new ViewMenuTab(defaultViewType, tab, libraryView);

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void RestoreLibraryTab(Tab tab)
        {
            var datasource = State.GetConnectionData(tab.EntityId);
            if (datasource == null)
                throw new NullReferenceException("Library datasource cannot be null.");

            var library = _libraryRepository.GetLibrary(datasource);
            var viewType = (ViewType) Enum.Parse(typeof (ViewType), tab.View);
            var libraryViewModel = new LibraryViewModel(library, viewType, tab.Id);
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
            var libraryTab = new ViewMenuTab(viewType, tab, libraryView);

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        public void DuplicateLibraryTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;


            var datasource = State.GetConnectionData(existingTab.Tab.EntityId);
            if (datasource == null)
                throw new NullReferenceException("Library datasource cannot be null.");

            var library = _libraryRepository.GetLibrary(datasource);
            var tab = CreateNewTab(library, existingTab.ViewType);
            var libraryViewModel = new LibraryViewModel(library, existingTab.ViewType, tab.Id);
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
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
            var libraryTab = _tabs.SingleOrDefault(tab => tab.TabId == tabId);
            if (libraryTab != null)
                libraryTab.ViewType = viewType;

            var stateTab = State.Tabs.SingleOrDefault(tab => tab.Id == tabId);
            if (stateTab != null)
                stateTab.View = viewType.ToString();
        }

        private Tab CreateNewTab(Library library, ViewType viewType)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                Order = State.GetNextTabOrder(),
                Type = TabType.Library,
                Header = library.Name,
                Process = ProcessType.Analytics,
                LibraryId = library.Id,
                EntityId = library.Id,
                View = viewType.ToString()
            };
        }
    }
}
