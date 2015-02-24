using System;
using System.Collections.Generic;
using System.Linq;
using Bloom.Analytics.Common;
using Bloom.Analytics.Controls;
using Bloom.Analytics.PersonModule.ViewModels;
using Bloom.Analytics.PersonModule.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.PersonModule.Services
{
    public class PersonService : IPersonService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PersonService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _tabs = new List<ViewMenuTab>();

            // Subscribe to events
            _eventAggregator.GetEvent<NewPersonTabEvent>().Subscribe(NewPersonTab);
            _eventAggregator.GetEvent<DuplicateTabEvent>().Subscribe(DuplicatePersonTab);
        }
        private readonly IEventAggregator _eventAggregator;
        private readonly List<ViewMenuTab> _tabs;

        public void NewPersonTab(object nothing)
        {
            NewPersonTab();
        }

        public void NewPersonTab()
        {
            var personViewModel = new PersonViewModel(ViewType.Stats);
            var personView = new PersonView(personViewModel);
            var personTab = new ViewMenuTab
            {
                Id = personViewModel.TabId,
                Type = TabType.Person,
                Header = "Person",
                Content = personView,
                ShowViewMenu = true,
                ViewType = personViewModel.ViewType
            };

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }

        public void DuplicatePersonTab(Guid tabId)
        {
            var existingTab = _tabs.FirstOrDefault(tab => tab.Id == tabId);
            if (existingTab == null)
                return;

            var personViewModel = new PersonViewModel(existingTab.ViewType);
            var personView = new PersonView(personViewModel);
            var personTab = new ViewMenuTab
            {
                Id = personViewModel.TabId,
                Type = TabType.Person,
                Header = "Person",
                Content = personView,
                ShowViewMenu = true,
                ViewType = personViewModel.ViewType
            };

            _tabs.Add(personTab);
            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }
    }
}
