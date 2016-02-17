using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a song and an artist collaborator.
    /// </summary>
    [Table(Name = "song_collaborator")]
    public class SongCollaborator
    {
        /// <summary>
        /// Creates a new song collaborator instance.
        /// </summary>
        /// <param name="song">A song.</param>
        /// <param name="artist">The collaborator artist.</param>
        public static SongCollaborator Create(Song song, Artist artist)
        {
            return new SongCollaborator
            {
                SongId = song.Id,
                ArtistId = artist.Id,
                Artist = artist
            };
        }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

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
