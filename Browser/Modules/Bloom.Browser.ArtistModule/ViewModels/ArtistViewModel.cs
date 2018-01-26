using System;
using Bloom.Browser.Common;
using Bloom.Domain.Models;

namespace Bloom.Browser.ArtistModule.ViewModels
{
    /// <summary>
    /// View model for ArtistView.xaml
    /// </summary>
    public class ArtistViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistViewModel"/> class.
        /// </summary>
        /// <param name="artist">An artist.</param>
        /// <param name="viewType">The view type.</param>
        /// <param name="tabId">The tab identifier.</param>
        public ArtistViewModel(Artist artist, ViewType viewType, Guid tabId)
        {
            ViewType = viewType;
            Artist = artist;
            TabId = tabId;
        }

        /// <summary>
        /// Gets or sets the tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the view type.
        /// </summary>
        public ViewType ViewType { get; set; }
    }
}
