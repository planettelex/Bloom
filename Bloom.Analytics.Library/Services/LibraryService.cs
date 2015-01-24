using Bloom.Analytics.Library.ViewModels;
using Bloom.Analytics.Library.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Analytics.Library.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Subscribe to events
            _eventAggregator.GetEvent<NewLibraryTabEvent>().Subscribe(NewLibraryTab);
        }
        private readonly IEventAggregator _eventAggregator;

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

        public void NewLibraryTab(object nothing)
        {
            NewLibraryTab();
        }
    }
}
