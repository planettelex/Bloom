using System;
using Bloom.Analytics.Common;
using Bloom.Domain.Models;

namespace Bloom.Analytics.Modules.ArtistModule.ViewModels
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
        /// <param name="viewType">An view type.</param>
        /// <param name="tabId">The tab identifier.</param>
        public ArtistViewModel(Artist artist, ViewType viewType, Guid tabId)
        {
            ViewType = viewType;
            Artist = artist;
            TabId = tabId;
        }

        /// <summary>
        /// The tab identifier.
        /// </summary>
        public Guid TabId { get; set; }

        /// <summary>
        /// The artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// The view type.
        /// </summary>
        public ViewType ViewType { get; set; }
    }
}
