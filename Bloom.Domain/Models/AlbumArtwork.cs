using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with artwork.
    /// </summary>
    public class AlbumArtwork
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the order for this album artwork.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the artwork URL.
        /// </summary>
        public string Url { get; set; }
    }
}
