using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with a playlist.
    /// </summary>
    public class LibraryPlaylist
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
        /// Gets or sets the playlist rating.
        /// </summary>
        public int Rating { get; set; }
    }
}
