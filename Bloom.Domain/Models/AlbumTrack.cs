using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a song.
    /// </summary>
    [Table(Name = "album_track")]
    public class AlbumTrack
    {
        /// <summary>
        /// Gets or sets the album track identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id")]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id")]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the disc number.
        /// </summary>
        [Column(Name = "disc_number")]
        public int DiscNumber { get; set; }

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        [Column(Name = "track_number")]
        public int TrackNumber { get; set; }

        /// <summary>
        /// Gets or sets the start time of this track in milliseconds (for single track albums).
        /// </summary>
        [Column(Name = "start_time")]
        public int? StartTime { get; set; }

        /// <summary>
        /// Gets or sets the stop time of this track in milliseconds (for single track albums).
        /// </summary>
        [Column(Name = "stop_time")]
        public int? StopTime { get; set; }
    }
}
