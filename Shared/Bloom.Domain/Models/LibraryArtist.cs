using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with an artist.
    /// </summary>
    [Table(Name = "library_artist")]
    public class LibraryArtist
    {
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
        /// Gets or sets the artist rating.
        /// </summary>
        [Column(Name = "rating")]
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the notes on this artist.
        /// </summary>
        [Column(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the artist play count.
        /// </summary>
        [Column(Name = "play_count")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Gets or sets the artist skip count.
        /// </summary>
        [Column(Name = "skip_count")]
        public int SkipCount { get; set; }

        /// <summary>
        /// Gets or sets the artist's remove count.
        /// </summary>
        [Column(Name = "remove_count")]
        public int RemoveCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this library follows this artist.
        /// </summary>
        [Column(Name = "follow")]
        public bool Follow { get; set; }
    }
}
