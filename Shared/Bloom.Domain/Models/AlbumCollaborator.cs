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
        /// Creates a new album collaborator instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="artist">The collaborator artist.</param>
        public static AlbumCollaborator Create(Album album, Artist artist)
        {
            return new AlbumCollaborator
            {
                AlbumId = album.Id,
                ArtistId = artist.Id,
                Artist = artist
            };
        }

        /// <summary>
        /// Creates a new album collaborator instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="artist">The collaborator artist.</param>
        /// <param name="isFeatured">Whether this collaborator is featured.</param>
        public static AlbumCollaborator Create(Album album, Artist artist, bool isFeatured)
        {
            return new AlbumCollaborator
            {
                AlbumId = album.Id,
                ArtistId = artist.Id,
                Artist = artist,
                IsFeatured = isFeatured
            };
        }

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
