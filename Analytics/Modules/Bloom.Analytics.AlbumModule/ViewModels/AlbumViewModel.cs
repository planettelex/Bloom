using System;
using Bloom.Domain.Models;

namespace Bloom.Analytics.Modules.AlbumModule.ViewModels
{
    /// <summary>
    /// View model for AlbumView.xaml
    /// </summary>
    public class AlbumViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumViewModel"/> class.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="tabId">The tab identifier.</param>
        public AlbumViewModel(Album album, Guid tabId)
        {
            Album = album;
            TabId = tabId;
        }

        /// <summary>
        /// The tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// The album.
        /// </summary>
        public Album Album { get; set; }
    }
}
