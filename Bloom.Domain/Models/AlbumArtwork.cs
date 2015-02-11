using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with artwork.
    /// </summary>
    [Table(Name = "album_artwork")]
    public class AlbumArtwork
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the order for this album artwork.
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
