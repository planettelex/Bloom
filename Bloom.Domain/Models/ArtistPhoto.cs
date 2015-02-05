using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a photo.
    /// </summary>
    public class ArtistPhoto
    {
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        public Guid PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// Gets or sets the order of this artist photo.
        /// </summary>
        public int Order { get; set; }
    }
}
