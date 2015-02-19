using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with a mood.
    /// </summary>
    [Table(Name = "playlist_mood")]
    public class PlaylistMood
    {
        /// <summary>
        /// Creates a new playlist mood instance.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <param name="mood">The mood.</param>
        public static PlaylistMood Create(Playlist playlist, Mood mood)
        {
            return new PlaylistMood
            {
                PlaylistId = playlist.Id,
                Playlist = playlist,
                MoodId = mood.Id,
                Mood = mood
            };
        }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the playlist.
        /// </summary>
        public Playlist Playlist { get; set; }

        /// <summary>
        /// Gets or sets the mood identifier.
        /// </summary>
        [Column(Name = "mood_id", IsPrimaryKey = true)]
        public Guid MoodId { get; set; }

        /// <summary>
        /// Gets or sets the mood.
        /// </summary>
        public Mood Mood { get; set; }
    }
}
