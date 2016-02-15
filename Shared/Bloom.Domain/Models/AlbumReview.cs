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
        /// Creates a new album review instance.
        /// </summary>
        /// <param name="album">The album.</param>
        /// <param name="review">The review.</param>
        public static AlbumReview Create(Album album, Review review)
        {
            return new AlbumReview
            {
                AlbumId = album.Id,
                ReviewId = review.Id
            };
        }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        [Column(Name = "album_id", IsPrimaryKey = true)]
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the review identifier.
        /// </summary>
        [Column(Name = "review_id", IsPrimaryKey = true)]
        public Guid ReviewId { get; set; }
    }
}
