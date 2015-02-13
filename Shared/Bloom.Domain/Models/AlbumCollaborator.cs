using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with an artist collaborator.
    /// </summary>
    [Table(Name = "album_collaborator")]
    public class AlbumCollaborator
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "artist_id", IsPrimaryKey = true)]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets whether this collaborator is featured.
        /// </summary>
        [Column(Name = "is_featured")]
        public bool IsFeatured { get; set; }
    }
}
