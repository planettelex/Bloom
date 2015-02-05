using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a song.
    /// </summary>
    public class AlbumTrack
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the disc number.
        /// </summary>
        public int DiscNumber { get; set; }

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        public int TrackNumber { get; set; }

        /// <summary>
        /// Gets or sets the start time of this track in milliseconds (for single track albums).
        /// </summary>
        public int? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the stop time of this track in milliseconds (for single track albums).
        /// </summary>
        public int? StopTime { get; set; }
    }
}
