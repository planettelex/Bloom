using Bloom.Browser.Song.ViewModels;
using Bloom.Browser.Song.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Song.Services
{
    public class SongService : ISongService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public SongService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Subscribe to events
            _eventAggregator.GetEvent<NewSongTabEvent>().Subscribe(NewSongTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewSongTab(object nothing)
        {
            NewSongTab();
        }

        public void NewSongTab()
        {
            var songViewModel = new SongViewModel();
            var songView = new SongView(songViewModel);
            var songTab = new Tab
            {
                Header = "Song",
                Content = songView
            };

            _eventAggregator.GetEvent<AddTabEvent>().Publish(songTab);
        }
    }
}
