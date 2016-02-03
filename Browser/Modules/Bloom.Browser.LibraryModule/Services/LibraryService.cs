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
using Bloom.Common;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Domain.Models;
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
        /// <param name="userService">The user service.</param>
        /// <param name="libraryRepository">The library repository.</param>
        public LibraryService(IEventAggregator eventAggregator, IRegionManager regionManager, IUserBaseService userService, ILibraryRepository libraryRepository)
        {
            _eventAggregator = eventAggregator;
            _libraryRepository = libraryRepository;
            _regionManager = regionManager;
            _userService = userService;
            _tabs = new List<ViewMenuTab>();
            
            // Subscribe to events
            _eventAggregator.GetEvent<ShowCreateNewLibraryModalEvent>().Subscribe(ShowCreateNewLibraryModal);
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<RestoreLibraryTabEvent>().Subscribe(RestoreLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IRegionManager _regionManager;
        private readonly IUserBaseService _userService;
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
            var newLibraryWindowModel = new NewLibraryWindowModel(_regionManager, _userService, _eventAggregator);
            var newLibraryWindow = new NewLibraryWindow(newLibraryWindowModel)
            {
                Owner = Application.Current.MainWindow
            };
            newLibraryWindow.ShowDialog();
        }

        /// <summary>
        /// Creates a new library tab.
        /// </summary>
        public void NewLibraryTab(Guid libraryId)
        {
            const ViewType defaultViewType = ViewType.Grid;
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

        /// <summary>
        /// Restores the library tab.
        /// </summary>
        /// <param name="tab">The library tab.</param>
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

        /// <summary>
        /// Duplicates a library tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
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

        /// <summary>
        /// Changes a library tab view.
        /// </summary>
        /// <param name="libraryViewTuple">The library view tab identifier and view type tuple.</param>
        public void ChangeLibraryTabView(Tuple<Guid, ViewType> libraryViewTuple)
        {
            ChangeLibraryTabView(libraryViewTuple.Item1, libraryViewTuple.Item2);
        }

        /// <summary>
        /// Changes a library tab view.
        /// </summary>
        /// <param name="tabId">The tab identifier of the view.</param>
        /// <param name="viewType">The view type to change to.</param>
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
                Process = ProcessType.Browser,
                LibraryId = library.Id,
                EntityId = library.Id,
                View = viewType.ToString()
            };
        }
    }
}
