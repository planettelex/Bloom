using System;
using Bloom.Domain.Models;

namespace Bloom.Analytics.Modules.PlaylistModule.ViewModels
{
    /// <summary>
    /// View model for PlaylistView.xaml
    /// </summary>
    public class PlaylistViewModel
    {
        public PlaylistViewModel(Playlist playlist, Guid tabId)
        {
            Playlist = playlist;
            TabId = tabId;
        }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// Gets or sets the playlist.
        /// </summary>
        public Playlist Playlist { get; set; }
    }
}
