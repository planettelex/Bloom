using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with an activity.
    /// </summary>
    [Table(Name = "playlist_activity")]
    public class PlaylistActivity
    {
        /// <summary>
        /// Creates a new playlist activity instance.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <param name="activity">The activity.</param>
        public static PlaylistActivity Create(Playlist playlist, Activity activity)
        {
            return new PlaylistActivity
            {
                PlaylistId = playlist.Id,
                Playlist = playlist,
                ActivityId = activity.Id,
                Activity = activity
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
        /// Gets or sets the activity identifier.
        /// </summary>
        [Column(Name = "activity_id", IsPrimaryKey = true)]
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the playlist activity.
        /// </summary>
        public Activity Activity { get; set; }
    }
}
