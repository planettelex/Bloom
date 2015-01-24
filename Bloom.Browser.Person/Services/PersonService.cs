using Bloom.Browser.Person.ViewModels;
using Bloom.Browser.Person.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Person.Services
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

            // Subscribe to events
            _eventAggregator.GetEvent<NewPersonTabEvent>().Subscribe(NewPersonTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewPersonTab(object nothing)
        {
            NewPersonTab();
        }

        public void NewPersonTab()
        {
            var personViewModel = new PersonViewModel();
            var personView = new PersonView(personViewModel);
            var personTab = new Tab
            {
                Header = "Person",
                Content = personView
            };

            _eventAggregator.GetEvent<AddTabEvent>().Publish(personTab);
        }
    }
}
