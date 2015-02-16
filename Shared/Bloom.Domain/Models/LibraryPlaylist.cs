using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a library with a playlist.
    /// </summary>
    [Table(Name = "library_playlist")]
    public class LibraryPlaylist
    {
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
        /// Gets or sets the playlist rating.
        /// </summary>
        [Column(Name = "rating")]
        public int? Rating { get; set; }
    }
}
