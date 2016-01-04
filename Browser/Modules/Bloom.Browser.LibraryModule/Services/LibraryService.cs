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
using Bloom.Browser.State.Services;
using Bloom.Common;
using Bloom.Data;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.Services;
using Bloom.State.Data.Respositories;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace Bloom.Browser.LibraryModule.Services
{
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="stateService">The state service.</param>
        /// <param name="libraryService">The library service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="libraryConnectionRepository">The library connection repository.</param>
        /// <param name="libraryRepository">The library repository.</param>
        /// <param name="personRepository">The person repository.</param>
        public LibraryService(IUnityContainer container, IEventAggregator eventAggregator, IRegionManager regionManager, IBrowserStateService stateService, Bloom.Services.ILibraryService libraryService, IUserService userService, ILibraryConnectionRepository libraryConnectionRepository, ILibraryRepository libraryRepository, IPersonRepository personRepository)
        {
            _container = container;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _stateService = stateService;
            _libraryService = libraryService;
            _userService = userService;
            _tabs = new List<ViewMenuTab>();
            _libraryConnectionRepository = libraryConnectionRepository;
            _personRepository = personRepository;
            _libraryRepository = libraryRepository;

            // Subscribe to events
            _eventAggregator.GetEvent<ShowCreateNewLibraryModalEvent>().Subscribe(ShowCreateNewLibraryModal);
            _eventAggregator.GetEvent<CreateNewLibraryEvent>().Subscribe(CreateNewLibrary);
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<RestoreLibraryTabEvent>().Subscribe(RestoreLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);

            State = (BrowserState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IBrowserStateService _stateService;
        private readonly Bloom.Services.ILibraryService _libraryService;
        private readonly IUserService _userService;
        private readonly List<ViewMenuTab> _tabs;
        private readonly ILibraryConnectionRepository _libraryConnectionRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Gets the state.
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
            var newLibraryWindowModel = new NewLibraryWindowModel(_regionManager, _userService);
            var newLibraryWindow = new NewLibraryWindow(newLibraryWindowModel, _eventAggregator)
            {
                Owner = Application.Current.MainWindow
            };
            newLibraryWindow.ShowDialog();
        }

        public void CreateNewLibrary(Library library)
        {
            CreateNewLibrary(library, State.User);
        }

        /// <summary>
        /// Creates a new library.
        /// </summary>
        /// <param name="library">The library.</param>
        /// <param name="owner">The owner.</param>
        /// <exception cref="System.ArgumentNullException">library</exception>
        public void CreateNewLibrary(Library library, User owner)
        {
            if (library == null)
                throw new ArgumentNullException("library");

            if (owner == null)
                throw new ArgumentNullException("owner");

            library.Owner = owner.AsPerson();
            var dataSource = new LibraryDataSource(_container);
            dataSource.Create(library.FilePath);
            var libraryConnection = LibraryConnection.Create(library);
            State.Connections.Add(libraryConnection);
            _libraryService.ConnectLibrary(libraryConnection, owner);
            _libraryConnectionRepository.AddLibraryConnection(libraryConnection);
            _personRepository.AddPerson(libraryConnection.DataSource, library.Owner);
            _libraryRepository.AddLibrary(libraryConnection.DataSource, library);
            _stateService.SaveState();
            libraryConnection.SaveChanges();

            _eventAggregator.GetEvent<ConnectionAddedEvent>().Publish(libraryConnection);
        }

        /// <summary>
        /// Creates a new library tab.
        /// </summary>
        public void NewLibraryTab(Guid libraryId)
        {
            const ViewType defaultViewType = ViewType.Grid;
            var library = new Library { Id = libraryId }; // TODO: Make this data access call
            var tab = CreateNewTab(libraryId, defaultViewType);
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
            var library = new Library { Id = tab.EntityId }; // TODO: Make this data access call
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

            var libraryId = existingTab.Tab.EntityId;
            var library = new Library { Id = libraryId }; // TODO: Make this data access call
            var tab = CreateNewTab(libraryId, existingTab.ViewType);
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

        private Tab CreateNewTab(Guid libraryId, ViewType viewType)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                Order = State.GetNextTabOrder(),
                Type = TabType.Library,
                Header = "Library",
                Process = ProcessType.Browser,
                LibraryId = libraryId,
                EntityId = libraryId,
                View = viewType.ToString()
            };
        }
    }
}
