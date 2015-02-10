using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a review.
    /// </summary>
    [Table(Name = "album_review")]
    public class AlbumReview
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the review identifier.
        /// </summary>
        [Column(Name = "review_id", IsPrimaryKey = true)]
        public Guid ReviewId { get; set; }

        /// <summary>
        /// Gets or sets the review.
        /// </summary>
        public Review Review { get; set; }
    }
}
