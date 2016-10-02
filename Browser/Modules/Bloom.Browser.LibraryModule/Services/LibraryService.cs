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
using Bloom.Browser.PubSubEvents.EventModels;
using Bloom.Common;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Bloom.UserModule.Services;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Browser.LibraryModule.Services
{
    /// <summary>
    /// Service for browser library operations.
    /// </summary>
    /// <seealso cref="Bloom.Browser.LibraryModule.Services.ILibraryService" />
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="sharedUserService">The user service.</param>
        /// <param name="libraryRepository">The library repository.</param>
        public LibraryService(IEventAggregator eventAggregator, IRegionManager regionManager, ISharedUserService sharedUserService, ILibraryRepository libraryRepository)
        {
            _eventAggregator = eventAggregator;
            _libraryRepository = libraryRepository;
            _regionManager = regionManager;
            _sharedUserService = sharedUserService;
            _tabs = new List<ViewMenuTab>();
            
            // Subscribe to events
            _eventAggregator.GetEvent<ShowCreateNewLibraryModalEvent>().Subscribe(ShowCreateNewLibraryModal);
            _eventAggregator.GetEvent<ShowAddMusicModalEvent>().Subscribe(ShowAddMusicModal);
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<RestoreLibraryTabEvent>().Subscribe(RestoreLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeTabView);
            _eventAggregator.GetEvent<NewAddMusicTabEvent>().Subscribe(NewAddMusicTab);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IRegionManager _regionManager;
        private readonly ISharedUserService _sharedUserService;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public BrowserState State { get; private set; }

        /// <summary>
        /// Sets the browser state.
        /// </summary>
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
            var newLibraryWindowModel = new NewLibraryWindowModel(_regionManager, _eventAggregator, _sharedUserService);
            var newLibraryWindow = new NewLibraryWindow(newLibraryWindowModel)
            {
                Owner = Application.Current.MainWindow
            };
            newLibraryWindow.ShowDialog();
        }

        /// <summary>
        /// Shows the add music modal window.
        /// </summary>
        public void ShowAddMusicModal(object nothing)
        {
            ShowAddMusicModal();
        }

        /// <summary>
        /// Shows the add music modal window.
        /// </summary>
        public void ShowAddMusicModal()
        {
            var addMusicWindowModel = new AddMusicWindowModel(_regionManager, _eventAggregator);
            var addMusicWindow = new AddMusicWindow(addMusicWindowModel)
            {
                Owner = Application.Current.MainWindow
            };
            addMusicWindow.ShowDialog();
        }

        /// <summary>
        /// Creates a new library tab.
        /// </summary>
        /// <param name="libraryId">The library identifier.</param>
        /// <exception cref="System.NullReferenceException">Library data source cannot be null.</exception>
        public void NewLibraryTab(Guid libraryId)
        {
            const ViewType defaultViewType = ViewType.Grid;
            var datasource = State.GetConnectionData(libraryId);
            if (datasource == null)
                throw new NullReferenceException("Library data source cannot be null.");

            var library = _libraryRepository.GetLibrary(datasource);
            var tab = CreateNewLibraryTab(library, defaultViewType);
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
        /// <exception cref="System.NullReferenceException">Library datasource cannot be null.</exception>
        public void RestoreLibraryTab(Tab tab)
        {
            if (tab == null || tab.EntityId == null)
                return;

            var datasource = State.GetConnectionData(tab.EntityId.Value);
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
        /// <exception cref="System.NullReferenceException">Library datasource cannot be null.</exception>
        public void DuplicateLibraryTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null || existingTab.Tab == null || existingTab.Tab.EntityId == null)
                return;

            var datasource = State.GetConnectionData(existingTab.Tab.EntityId.Value);
            if (datasource == null)
                throw new NullReferenceException("Library datasource cannot be null.");

            var library = _libraryRepository.GetLibrary(datasource);
            var tab = CreateNewLibraryTab(library, existingTab.ViewType);
            var libraryViewModel = new LibraryViewModel(library, existingTab.ViewType, tab.Id);
            var libraryView = new LibraryView(libraryViewModel, _eventAggregator);
            var libraryTab = new ViewMenuTab(tab, libraryView);

            _tabs.Add(libraryTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }

        /// <summary>
        /// Creates a new add music tab.
        /// </summary>
        /// <param name="eventModel">The event model.</param>
        /// <exception cref="System.NullReferenceException">Library data source cannot be null.</exception>
        public void NewAddMusicTab(AddMusicEventModel eventModel)
        {
            const ViewType defaultViewType = ViewType.Grid;
            var libraries = new List<Library>();
            foreach (var libraryId in eventModel.LibraryIds)
            {
                var datasource = State.GetConnectionData(libraryId);
                if (datasource == null)
                    throw new NullReferenceException("Library data source cannot be null.");

                var library = _libraryRepository.GetLibrary(datasource);
                libraries.Add(library);
            }

            var tab = CreateNewAddMusicTab(libraries, defaultViewType);
            var newMusicViewModel = new NewMusicViewModel(_eventAggregator, State, tab.Id, defaultViewType, libraries, eventModel.Source, eventModel.Path, eventModel.CopyFiles);
            var newMusicView = new NewMusicView(newMusicViewModel);
            var newMusicTab = new ViewMenuTab(defaultViewType, tab, newMusicView);

            _tabs.Add(newMusicTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(newMusicTab);
        }

        /// <summary>
        /// Restores the add music tab.
        /// </summary>
        /// <param name="tab">The add music tab.</param>
        /// <exception cref="System.NullReferenceException">Library datasource cannot be null.</exception>
        public void RestoreAddMusicTab(Tab tab)
        {
            if (tab == null || tab.Libraries == null)
                return;

            var libraries = new List<Library>();
            foreach (var tabLibrary in tab.Libraries)
            {
                var datasource = State.GetConnectionData(tabLibrary.LibraryId);
                if (datasource == null)
                    throw new NullReferenceException("Library datasource cannot be null.");

                var library = _libraryRepository.GetLibrary(datasource);
                libraries.Add(library);
            }
            var viewType = (ViewType) Enum.Parse(typeof(ViewType), tab.View);
            var newMusicViewModel = new NewMusicViewModel(_eventAggregator, State, tab.Id, viewType, libraries);
            var newMusicView = new NewMusicView(newMusicViewModel);
            var newMusicTab = new ViewMenuTab(viewType, tab, newMusicView);

            _tabs.Add(newMusicTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(newMusicTab);
        }

        /// <summary>
        /// Duplicates an add music tab.
        /// </summary>
        /// <param name="tabId">The tab identifier to duplicate.</param>
        /// <exception cref="System.NullReferenceException">Library datasource cannot be null.</exception>
        public void DuplicateAddMusicTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null || existingTab.Tab == null || existingTab.Tab.EntityId == null)
                return;

            var libraries = new List<Library>();
            foreach (var tabLibrary in existingTab.Tab.Libraries)
            {
                var datasource = State.GetConnectionData(tabLibrary.LibraryId);
                if (datasource == null)
                    throw new NullReferenceException("Library datasource cannot be null.");

                var library = _libraryRepository.GetLibrary(datasource);
                libraries.Add(library);
            }
            var tab = CreateNewAddMusicTab(libraries, existingTab.ViewType);
            var newMusicViewModel = new NewMusicViewModel(_eventAggregator, State, tab.Id, existingTab.ViewType, libraries);
            var newMusicView = new NewMusicView(newMusicViewModel);
            var newMusicTab = new ViewMenuTab(tab, newMusicView);

            _tabs.Add(newMusicTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(newMusicTab);
        }

        /// <summary>
        /// Changes a tab view.
        /// </summary>
        /// <param name="viewTuple">The view tab identifier and view type tuple.</param>
        public void ChangeTabView(Tuple<Guid, ViewType> viewTuple)
        {
            ChangeTabView(viewTuple.Item1, viewTuple.Item2);
        }

        /// <summary>
        /// Changes a tab view.
        /// </summary>
        /// <param name="tabId">The tab identifier of the view.</param>
        /// <param name="viewType">The view type to change to.</param>
        public void ChangeTabView(Guid tabId, ViewType viewType)
        {
            var serviceTab = _tabs.SingleOrDefault(tab => tab.TabId == tabId);
            if (serviceTab != null)
                serviceTab.ViewType = viewType;

            var stateTab = State.Tabs.SingleOrDefault(tab => tab.Id == tabId);
            if (stateTab != null)
                stateTab.View = viewType.ToString();
        }

        /// <summary>
        /// Creates a new library tab.
        /// </summary>
        /// <param name="library">The library the tab pertains to.</param>
        /// <param name="viewType">The view type.</param>
        private Tab CreateNewLibraryTab(Library library, ViewType viewType)
        {
            var libraryBuid = new Buid(library.Id, BloomEntity.Library, library.Id);
            return Tab.Create(ProcessType.Browser, State.User, libraryBuid, State.GetNextTabOrder(), TabType.Library, library.Name, viewType.ToString());
        }

        /// <summary>
        /// Creates a new add music tab.
        /// </summary>
        /// <param name="libraries">The libraries the tab pertains to.</param>
        /// <param name="viewType">The view type.</param>
        private Tab CreateNewAddMusicTab(IEnumerable<Library> libraries, ViewType viewType)
        {
            var libraryIds = libraries.Select(library => library.Id).ToList();
            return Tab.Create(ProcessType.Browser, State.User, libraryIds, State.GetNextTabOrder(), TabType.NewMusic, "New Music", viewType.ToString());
        }
    }
}
