using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Controls;
using Bloom.Analytics.Events;
using Bloom.Analytics.Modules.LibraryModule.ViewModels;
using Bloom.Common;
using Bloom.Data.Repositories;
using Bloom.Domain.Models;
using Bloom.Events;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Prism.Events;
using Prism.Regions;
using LibraryView = Bloom.Analytics.Modules.LibraryModule.Views.LibraryView;

namespace Bloom.Analytics.Modules.LibraryModule.Services
{
    /// <summary>
    /// Service for analytics library operations.
    /// </summary>
    /// <seealso cref="ILibraryService" />
    public class LibraryService : ILibraryService
    {
        public LibraryService(IEventAggregator eventAggregator, IRegionManager regionManager, ILibraryRepository libraryRepository)
        {
            _eventAggregator = eventAggregator;
            _libraryRepository = libraryRepository;
            _regionManager = regionManager;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
            _eventAggregator.GetEvent<RestoreLibraryTabEvent>().Subscribe(RestoreLibraryTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicateLibraryTab);
            _eventAggregator.GetEvent<ChangeLibraryTabViewEvent>().Subscribe(ChangeLibraryTabView);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly ILibraryRepository _libraryRepository;
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

        /// <summary>
        /// Creates a new library tab.
        /// </summary>
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

        /// <summary>
        /// Restores the library tab.
        /// </summary>
        /// <param name="tab">The library tab.</param>
        public void RestoreLibraryTab(Tab tab)
        {
            if (tab?.EntityId == null)
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
        public void DuplicateLibraryTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab?.Tab?.EntityId == null)
                return;

            var datasource = State.GetConnectionData(existingTab.Tab.EntityId.Value);
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
        /// Changes a tab view.
        /// </summary>
        /// <param name="libraryViewTuple">A tab identifier and view type tuple.</param>
        public void ChangeLibraryTabView(Tuple<Guid, ViewType> libraryViewTuple)
        {
            ChangeTabView(libraryViewTuple.Item1, libraryViewTuple.Item2);
        }

        /// <summary>
        /// Changes a tab view.
        /// </summary>
        /// <param name="tabId">The tab identifier of the view.</param>
        /// <param name="viewType">The view type to change to.</param>
        public void ChangeTabView(Guid tabId, ViewType viewType)
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
            var libraryBuid = new Buid(library.Id, BloomEntity.Library, library.Id);
            return Tab.Create(ProcessType.Analytics, State.User, libraryBuid, State.GetNextTabOrder(), TabType.Library, library.Name, viewType.ToString());
        }
    }
}
