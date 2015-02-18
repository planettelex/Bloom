using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a reference.
    /// </summary>
    [Table(Name = "album_reference")]
    public class AlbumReference
    {
        /// <summary>
        /// Creates a new album reference instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="reference">The reference.</param>
        public static AlbumReference Create(Album album, Reference reference)
        {
            return new AlbumReference
            {
                AlbumId = album.Id,
                ReferenceId = reference.Id,
                Reference = reference
            };
        }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        [Column(Name = "reference_id", IsPrimaryKey = true)]
        public Guid ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public Reference Reference { get; set; }

    }
}
