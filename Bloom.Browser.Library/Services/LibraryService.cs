using Bloom.Browser.Library.ViewModels;
using Bloom.Browser.Library.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Library.Services
{
    public class LibraryService : ILibraryService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public LibraryService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Subscribe to events
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewLibraryTab(object nothing)
        {
            NewLibraryTab();
        }

        public void NewLibraryTab()
        {
            var libraryViewModel = new LibraryViewModel();
            var libraryView = new LibraryView(libraryViewModel);
            var libraryTab = new Tab
            {
                Header = "Library",
                Content = libraryView
            };

            _eventAggregator.GetEvent<AddTabEvent>().Publish(libraryTab);
        }
    }
}
