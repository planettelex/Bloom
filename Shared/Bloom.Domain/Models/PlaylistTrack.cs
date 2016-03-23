using System;
using System.Data.Linq.Mapping;
using System.Globalization;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a song track in a playlist.
    /// </summary>
    [Table(Name = "playlist_track")]
    public class PlaylistTrack
    {
        /// <summary>
        /// Creates a new playlist track instance.
        /// </summary>
        /// <param name="playlist">A playlist.</param>
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
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

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
        /// Gets or sets the track number.
        /// </summary>
        [Column(Name = "track_number", IsPrimaryKey = true)]
        public int TrackNumber { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            var label = TrackNumber.ToString(CultureInfo.InvariantCulture);
            if (Song != null)
                label += ": " + Song.Name;

            return label;
        }
    }
}
