using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with an artist.
    /// </summary>
    public class LibraryArtist
    {
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the artist rating.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the notes on this artist.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the artist play count.
        /// </summary>
        public int PlayCount { get; set; }

        /// <summary>
        /// Gets or sets the artist skip count.
        /// </summary>
        public int SkipCount { get; set; }
    }
}
