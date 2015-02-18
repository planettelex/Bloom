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
        /// Creates a new album artwork instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="url">The artwork URL.</param>
        /// <param name="priority">The order priority.</param>
        public static AlbumArtwork Create(Album album, string url, int priority)
        {
            return new AlbumArtwork
            {
                AlbumId = album.Id,
                Url = url,
                Priority = priority
            };
        }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the order priority for this album artwork.
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
