using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with a song.
    /// </summary>
    [Table(Name = "playlist_track")]
    public class PlaylistTrack
    {
        /// <summary>
        /// Creates a new playlist track instance.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <param name="song">The song.</param>
        /// <param name="trackNumber">The track number.</param>
        public static PlaylistTrack Create(Playlist playlist, Song song, int trackNumber)
        {
            return new PlaylistTrack
            {
                PlaylistId = playlist.Id,
                SongId = song.Id,
                Song = song,
                TrackNumber = trackNumber
            };
        }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id")]
        public Guid PlaylistId { get; set; }

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
        /// Gets or sets the track number.
        /// </summary>
        [Column(Name = "track_number")]
        public int TrackNumber { get; set; }
    }
}
