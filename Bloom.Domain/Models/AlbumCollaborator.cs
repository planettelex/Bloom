using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with an artist collaborator.
    /// </summary>
    public class AlbumCollaborator
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets whether this collaborator is featured.
        /// </summary>
        public bool IsFeatured { get; set; }
    }
}
