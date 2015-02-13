using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Bloom.Browser.Common;
using Bloom.Browser.Controls;
using Bloom.Browser.LibraryModule.ViewModels;
using Bloom.Browser.LibraryModule.Views;
using Bloom.Browser.LibraryModule.WindowModels;
using Bloom.Browser.LibraryModule.Windows;
using Bloom.Browser.PubSubEvents;
using Bloom.Data;
using Bloom.Domain.Enums;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Bloom.State.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.LibraryModule.Services
{
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="stateService">The state service.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        public LibraryService(IEventAggregator eventAggregator, IRegionManager regionManager, IStateService stateService, ILibraryConnectionRepository libraryConnectionRepository)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _stateService = stateService;
            _tabs = new List<LibraryTab>();
            _libraryConnectionRepository = libraryConnectionRepository;

            // Subscribe to events
            _eventAggregator.GetEvent<ShowCreateNewLibraryModalEvent>().Subscribe(ShowCreateNewLibraryModal);
            _eventAggregator.GetEvent<CreateNewLibraryEvent>().Subscribe(CreateNewLibrary);
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);

            State = (BrowserState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IStateService _stateService;
        private readonly List<LibraryTab> _tabs;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;

        /// <summary>
        /// Gets the browser application state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Shows the create new library modal window.
        /// </summary>
        public void ShowCreateNewLibraryModal(object nothing)
        {
            ShowCreateNewLibraryModal();
        }

        /// <summary>
        /// Shows the create new library modal window.
        /// </summary>
        public void ShowCreateNewLibraryModal()
        {
            var newLibraryWindowModel = new NewLibraryWindowModel(_regionManager);
            var newLibraryWindow = new NewLibraryWindow(newLibraryWindowModel, _eventAggregator)
            {
                Owner = Application.Current.MainWindow
            };

            newLibraryWindow.ShowDialog();
        }

        /// <summary>
        /// Creates a new library.
        /// </summary>
        public void CreateNewLibrary(Library library)
        {
            var dataSource = new LibraryDataSource();
            dataSource.Create(library.FilePath);
            var libraryConnection = LibraryConnection.Create(library);
            State.Connections.AddLibraryConnection(libraryConnection);
            _libraryConnectionRepository.AddLibraryConnection(libraryConnection);
            _stateService.SaveState();
        }

        /// <summary>
        /// Creates a new library tab.
        /// </summary>
        public void NewLibraryTab(object nothing)
        {
            NewLibraryTab();
        }

        /// <summary>
        /// Creates a new library tab.
        /// </summary>
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

        /// <summary>
        /// Duplicates a library tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
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

        /// <summary>
        /// Changes a library tab view.
        /// </summary>
        /// <param name="libraryViewTuple">The library view tab identifier and view type tuple.</param>
        public void ChangeLibraryTabView(Tuple<Guid, LibraryViewType> libraryViewTuple)
        {
            ChangeLibraryTabView(libraryViewTuple.Item1, libraryViewTuple.Item2);
        }

        /// <summary>
        /// Changes a library tab view.
        /// </summary>
        /// <param name="tabId">The tab identifier of the view.</param>
        /// <param name="viewType">The view type to change to.</param>
        public void ChangeLibraryTabView(Guid tabId, LibraryViewType viewType)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            existingTab.ViewType = viewType;
        }
    }
}
