using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with a song.
    /// </summary>
    public class PlaylistTrack
    {
        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        public int TrackNumber { get; set; }
    }
}
