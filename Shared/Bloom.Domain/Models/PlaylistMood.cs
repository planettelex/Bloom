using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a playlist and a mood.
    /// </summary>
    [Table(Name = "playlist_mood")]
    public class PlaylistMood
    {
        /// <summary>
        /// Creates a new playlist mood instance.
        /// </summary>
        /// <param name="playlist">A playlist.</param>
        /// <param name="mood">The mood.</param>
        public static PlaylistMood Create(Playlist playlist, Mood mood)
        {
            return new PlaylistMood
            {
                PlaylistId = playlist.Id,
                MoodId = mood.Id
            };
        }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the mood identifier.
        /// </summary>
        [Column(Name = "mood_id", IsPrimaryKey = true)]
        public Guid MoodId { get; set; }
    }
}
