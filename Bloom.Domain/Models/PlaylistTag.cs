using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with a tag.
    /// </summary>
    public class PlaylistTag
    {
        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the playlist.
        /// </summary>
        public Playlist Playlist { get; set; }

        /// <summary>
        /// Gets or sets the tag identifier.
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public Tag Tag { get; set; }
    }
}
