using Bloom.Browser.Playlist.ViewModels;
using Bloom.Browser.Playlist.Views;
using Bloom.Controls;
using Bloom.PubSubEvents;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Bloom.Browser.Playlist.Services
{
    public class PlaylistService : IPlaylistService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistService"/> class.
        /// </summary>
        /// <param name="eventAggregator">The event aggregator.</param>
        public PlaylistService(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            // Subscribe to events
            _eventAggregator.GetEvent<NewPlaylistTabEvent>().Subscribe(NewPlaylistTab);
        }
        private readonly IEventAggregator _eventAggregator;

        public void NewPlaylistTab(object nothing)
        {
            NewPlaylistTab();
        }

        public void NewPlaylistTab()
        {
            var playlistViewModel = new PlaylistViewModel();
            var playlistView = new PlaylistView(playlistViewModel);
            var playlistTab = new Tab
            {
                Header = "Playlist",
                Content = playlistView
            };

            _eventAggregator.GetEvent<AddTabEvent>().Publish(playlistTab);
        }
    }
}
