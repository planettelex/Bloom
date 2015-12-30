using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Common;
using Bloom.Analytics.Controls;
using Bloom.Analytics.PersonModule.ViewModels;
using Bloom.Analytics.PersonModule.Views;
using Bloom.Common;
using Bloom.Domain.Models;
using Bloom.PubSubEvents;
using Bloom.State.Domain.Models;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;

namespace Bloom.Analytics.PersonModule.Services
{
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
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewPersonTabEvent>().Subscribe(NewPersonTab);
            _eventAggregator.GetEvent<RestorePersonTabEvent>().Subscribe(RestorePersonTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicatePersonTab);

            State = (AnalyticsState) regionManager.Regions["DocumentRegion"].Context;
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        /// <summary>
        /// Gets the state.
        /// </summary>
        public AnalyticsState State { get; private set; }

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
            var person = new Person { Id = tab.EntityId }; // TODO: Make this data access call
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
            var person = new Person { Id = personId }; // TODO: Make this data access call
            var tab = CreateNewTab(new Buid(existingTab.Tab.LibraryId, BloomEntity.Artist, personId), existingTab.ViewType);
            var personViewModel = new PersonViewModel(person, existingTab.ViewType, tabId);
            var personView = new PersonView(personViewModel);
            var personTab = new ViewMenuTab(personViewModel.ViewType, tab, personView);

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }

        private Tab CreateNewTab(Buid personBuid, ViewType viewType)
        {
            return new Tab
            {
                Id = Guid.NewGuid(),
                Order = State.GetNextTabOrder(),
                Type = TabType.Person,
                Header = "Person",
                Process = ProcessType.Analytics,
                LibraryId = personBuid.LibraryId,
                EntityId = personBuid.EntityId,
                View = viewType.ToString()
            };
        }
    }
}
