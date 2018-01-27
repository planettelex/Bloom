using System;
using Bloom.Domain.Models;

namespace Bloom.Browser.Modules.SongModule.ViewModels
{
    /// <summary>
    /// View model for SongView.xaml
    /// </summary>
    public class SongViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongViewModel"/> class.
        /// </summary>
        /// <param name="song">A song.</param>
        /// <param name="tabId">The tab identifier.</param>
        public SongViewModel(Song song, Guid tabId)
        {
            Song = song;
            TabId = tabId;
        }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }
    }
}
