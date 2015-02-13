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
        /// Gets or sets the playlist identifier.
        /// </summary>
        [Column(Name = "playlist_id", IsPrimaryKey = true)]
        public Guid PlaylistId { get; set; }

        /// <summary>
        /// Gets or sets the artwork order.
        /// </summary>
        [Column(Name = "order", IsPrimaryKey = true)]
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the artwork URL.
        /// </summary>
        [Column(Name = "url")]
        public string Url { get; set; }
    }
}
