using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with a song.
    /// </summary>
    public class LibrarySong
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the song rating.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets notes on this song.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the song's play count.
        /// </summary>
        public int PlayCount { get; set; }

        /// <summary>
        /// Gets or sets the song's skip count.
        /// </summary>
        public int SkipCount { get; set; }

        /// <summary>
        /// Gets or sets the song's remove count.
        /// </summary>
        public int RemoveCount { get; set; }

        /// <summary>
        /// Gets or sets the date this song was added on.
        /// </summary>
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this song was rated on.
        /// </summary>
        public DateTime RatedOn { get; set; }

        /// <summary>
        /// Gets or sets the date this song was last played.
        /// </summary>
        public DateTime LastPlayed { get; set; }

        /// <summary>
        /// Gets or sets identifier of the library owner this song was received from.
        /// </summary>
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
