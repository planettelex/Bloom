using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Controls;
using Bloom.Analytics.Modules.PersonModule.ViewModels;
using Bloom.Analytics.Modules.PersonModule.Views;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Enums;
using Bloom.State.Domain.Models;
using Prism.Events;
using Prism.Regions;

namespace Bloom.Analytics.Modules.PersonModule.Services
{
    /// <summary>
    /// Service for analytics person operations.
    /// </summary>
    /// <seealso cref="IPersonService" />
    public class PersonService : IPersonService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService" /> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        /// <param name="regionManager">The region manager.</param>
        public PersonService(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewPersonTabEvent>().Subscribe(NewPersonTab);
            _eventAggregator.GetEvent<RestorePersonTabEvent>().Subscribe(RestorePersonTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicatePersonTab);
            _eventAggregator.GetEvent<ApplicationLoadedEvent>().Subscribe(SetState);
            _eventAggregator.GetEvent<UserChangedEvent>().Subscribe(SetState);
        }
        private readonly IEventAggregator _eventAggregator;
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

        public void NewPersonTab(Buid personBuid)
        {
            const ViewType defaultViewType = ViewType.Stats;
            var person = new Person { Id = personBuid.EntityId }; // TODO: Make this data access call
            var tab = CreateNewTab(personBuid, defaultViewType);
            var personViewModel = new PersonViewModel(person, defaultViewType, tab.Id);
            var personView = new PersonView(personViewModel);
            var personTab = new ViewMenuTab(defaultViewType, tab, personView);

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }

        public void RestorePersonTab(Tab tab)
        {
            if (tab == null || tab.EntityId == null)
                return;

            var person = new Person { Id = tab.EntityId.Value }; // TODO: Make this data access call
            var viewType = (ViewType)Enum.Parse(typeof(ViewType), tab.View);
            var personViewModel = new PersonViewModel(person, viewType, tab.Id);
            var personView = new PersonView(personViewModel);
            var personTab = new ViewMenuTab(viewType, tab, personView);

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }

        public void DuplicatePersonTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(t => t.TabId == tabId);
            if (existingTab == null)
                return;

            var personId = existingTab.Tab.EntityId;
            var libraryId = existingTab.Tab.LibraryId;
            if (personId == null || libraryId == null)
                return;

            var person = new Person { Id = personId.Value }; // TODO: Make this data access call
            var tab = CreateNewTab(new Buid(libraryId.Value, BloomEntity.Artist, personId.Value), existingTab.ViewType);
            var personViewModel = new PersonViewModel(person, existingTab.ViewType, tabId);
            var personView = new PersonView(personViewModel);
            var personTab = new ViewMenuTab(personViewModel.ViewType, tab, personView);

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }

        private Tab CreateNewTab(Buid personBuid, ViewType viewType)
        {
            return Tab.Create(ProcessType.Analytics, State.User, personBuid, State.GetNextTabOrder(), TabType.Person, "Person", viewType.ToString());
        }
    }
}
