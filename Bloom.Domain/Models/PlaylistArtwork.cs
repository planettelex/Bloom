using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with an activity.
    /// </summary>
    public class PlaylistArtwork
    {
        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the artwork order.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the artwork URL.
        /// </summary>
        public string Url { get; set; }
    }
}
