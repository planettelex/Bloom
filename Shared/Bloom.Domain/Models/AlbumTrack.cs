using System;
using System.Data.Linq.Mapping;
using Microsoft.Practices.Prism.Mvvm;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a song track on an album.
    /// </summary>
    [Table(Name = "album_track")]
    public class AlbumTrack : BindableBase
    {
        /// <summary>
        /// Creates a new album track instance.
        /// </summary>
        /// <param name="album">An album.</param>
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
        /// <param name="album">An album.</param>
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
        public int DiscNumber
        {
            get { return _discNumber; }
            set { SetProperty(ref _discNumber, value); }
        }
        private int _discNumber;

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        [Column(Name = "track_number")]
        public int TrackNumber
        {
            get { return _trackNumber; }
            set { SetProperty(ref _trackNumber, value); }
        }
        private int _trackNumber;

        /// <summary>
        /// Gets or sets the start time of this track in milliseconds (for single track albums).
        /// </summary>
        [Column(Name = "start_time")]
        public int? StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }
        private int? _startTime;

        /// <summary>
        /// Gets or sets the stop time of this track in milliseconds (for single track albums).
        /// </summary>
        [Column(Name = "stop_time")]
        public int? StopTime
        {
            get { return _stopTime; }
            set { SetProperty(ref _stopTime, value); }
        }
        private int? _stopTime;
    }
}
