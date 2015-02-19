using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with an activity.
    /// </summary>
    [Table(Name = "playlist_artwork")]
    public class PlaylistArtwork
    {
        /// <summary>
        /// Creates a new playlist artwork instance.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <param name="url">The artwork URL.</param>
        /// <param name="priority">The order priority.</param>
        public static PlaylistArtwork Create(Playlist playlist, string url, int priority)
        {
            return new PlaylistArtwork
            {
                PlaylistId = playlist.Id,
                Url = url,
                Priority = priority
            };
        }

        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the artwork order priority.
        /// </summary>
        [Column(Name = "priority", IsPrimaryKey = true)]
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the artwork URL.
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }
    }
}
