using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between an album and an artist collaborator.
    /// </summary>
    [Table(Name = "album_collaborator")]
    public class AlbumCollaborator
    {
        /// <summary>
        /// Creates a new album collaborator instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="artist">The collaborator artist.</param>
        public static AlbumCollaborator Create(Album album, Artist artist)
        {
            return new AlbumCollaborator
            {
                AlbumId = album.Id,
                Artist = artist
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
        public Artist Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                ArtistId = _artist?.Id ?? Guid.Empty;
            }
        }
        private Artist _artist;

        /// <summary>
        /// Gets or sets whether this collaborator is featured.
        /// </summary>
        [Column(Name = "is_featured")]
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            var collaborator = Artist != null ? Artist.Name : ArtistId.ToString();
            if (IsFeatured)
                collaborator += " (Featured)";

            return collaborator;
        }
    }
}
