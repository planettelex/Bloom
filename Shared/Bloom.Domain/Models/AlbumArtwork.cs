using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents artwork for an album.
    /// </summary>
    [Table(Name = "album_artwork")]
    public class AlbumArtwork
    {
        /// <summary>
        /// Creates a new album artwork instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="filePath">The artwork file path.</param>
        /// <param name="priority">The order priority.</param>
        public static AlbumArtwork Create(Album album, string filePath, int priority)
        {
            return new AlbumArtwork
            {
                AlbumId = album.Id,
                FilePath = filePath,
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
        /// Gets or sets the artwork file path.
        /// </summary>
        [Column(Name = "file_path")]
        public string FilePath { get; set; }
    }
}
