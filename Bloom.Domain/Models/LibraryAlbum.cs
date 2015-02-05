using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associcates a library with an album.
    /// </summary>
    public class LibraryAlbum
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the review.
        /// </summary>
        public string Review { get; set; }

        /// <summary>
        /// Gets or sets the album media included in this library.
        /// </summary>
        public List<LibraryAlbumMedia> Media { get; set; }
    }
}
