using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with a song.
    /// </summary>
    [Table(Name = "library_song")]
    public class LibrarySong
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the song rating.
        /// </summary>
        [Column(Name = "rating")]
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets notes on this song.
        /// </summary>
        [Column(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the song's play count.
        /// </summary>
        [Column(Name = "play_count")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Gets or sets the song's skip count.
        /// </summary>
        [Column(Name = "skip_count")]
        public int SkipCount { get; set; }

        /// <summary>
        /// Gets or sets the song's remove count.
        /// </summary>
        [Column(Name = "remove_count")]
        public int RemoveCount { get; set; }

        /// <summary>
        /// Gets or sets the date this song was added on.
        /// </summary>
        [Column(Name = "added_on")]
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this song was rated on.
        /// </summary>
        [Column(Name = "rated_on")]
        public DateTime RatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this song was last played.
        /// </summary>
        [Column(Name = "last_played")]
        public DateTime LastPlayed { get; set; }

        /// <summary>
        /// Gets or sets identifier of the library owner this song was received from.
        /// </summary>
        [Column(Name = "received_from_id")]
        public Guid ReceivedFromId { get; set; }

        /// <summary>
        /// Gets or sets the library owner this song was received from.
        /// </summary>
        public Person ReceivedFrom { get; set; }

        /// <summary>
        /// Gets or sets the library song media.
        /// </summary>
        public List<LibrarySongMedia> Media { get; set; } 
    }
}
