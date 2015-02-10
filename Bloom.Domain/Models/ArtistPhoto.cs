using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a photo.
    /// </summary>
    [Table(Name = "artist_photo")]
    public class ArtistPhoto
    {
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "artist_id", IsPrimaryKey = true)]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the photo identifier.
        /// </summary>
        [Column(Name = "photo_id", IsPrimaryKey = true)]
        public Guid PhotoId { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// Gets or sets the order of this artist photo.
        /// </summary>
        [Column(Name = "order")]
        public int Order { get; set; }
    }
}
