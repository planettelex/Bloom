using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a collaborator.
    /// </summary>
    public class SongCollaborator
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

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
