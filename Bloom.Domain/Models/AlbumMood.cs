using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a mood.
    /// </summary>
    public class AlbumMood
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the mood identifier.
        /// </summary>
        public Guid MoodId { get; set; }

        /// <summary>
        /// Gets or sets the mood.
        /// </summary>
        public Mood Mood { get; set; }
    }
}
