using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between an album and a reference.
    /// </summary>
    [Table(Name = "album_reference")]
    public class AlbumReference
    {
        /// <summary>
        /// Creates a new album reference instance.
        /// </summary>
        /// <param name="album">An album.</param>
        /// <param name="reference">The reference.</param>
        public static AlbumReference Create(Album album, Reference reference)
        {
            return new AlbumReference
            {
                AlbumId = album.Id,
                ReferenceId = reference.Id
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
    }
}
