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
