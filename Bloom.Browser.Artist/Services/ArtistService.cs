using Bloom.Browser.Artist.ViewModels;
using Bloom.Browser.Artist.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Artist.Services
{
    public class ArtistService : IArtistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public ArtistService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Subscribe to events
            _eventAggregator.GetEvent<NewArtistTabEvent>().Subscribe(NewArtistTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewArtistTab(object nothing)
        {
            NewArtistTab();
        }

        public void NewArtistTab()
        {
            var artistViewModel = new ArtistViewModel();
            var artistView = new ArtistView(artistViewModel);
            var artistTab = new Tab
            {
                Header = "Artist",
                Content = artistView
            };

            _eventAggregator.GetEvent<AddTabEvent>().Publish(artistTab);
        }
    }
}
