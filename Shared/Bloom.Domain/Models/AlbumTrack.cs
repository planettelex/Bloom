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
        /// Creates a new album track instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="song">The song.</param>
        /// <param name="trackNumber">The track number.</param>
        public static AlbumTrack Create(Album album, Song song, int trackNumber)
        {
            return new AlbumTrack
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                SongId = song.Id,
                Song = song,
                DiscNumber = 1,
                TrackNumber = trackNumber
            };
        }

        /// <summary>
        /// Creates a new album track instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="song">The song.</param>
        /// <param name="trackNumber">The track number.</param>
        /// <param name="discNumber">The disc number.</param>
        public static AlbumTrack Create(Album album, Song song, int trackNumber, int discNumber)
        {
            return new AlbumTrack
            {
                Id = Guid.NewGuid(),
                AlbumId = album.Id,
                SongId = song.Id,
                Song = song,
                DiscNumber = discNumber,
                TrackNumber = trackNumber
            };
        }

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
