using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a playlist and artwork.
    /// </summary>
    [Table(Name = "playlist_artwork")]
    public class PlaylistArtwork
    {
        /// <summary>
        /// Creates a new playlist artwork instance.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        /// <param name="filePath">The artwork file path.</param>
        /// <param name="priority">The order priority.</param>
        public static PlaylistArtwork Create(Playlist playlist, string filePath, int priority)
        {
            return new PlaylistArtwork
            {
                PlaylistId = playlist.Id,
                FilePath = filePath,
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
        /// Gets or sets the artwork file path.
        /// </summary>
        [Column(Name = "file_path")]
        public string FilePath { get; set; }
    }
}
