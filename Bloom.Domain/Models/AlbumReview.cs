using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a review.
    /// </summary>
    public class AlbumReview
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the album.
        /// </summary>
        public Album Album { get; set; }

        /// <summary>
        /// Gets or sets the review identifier.
        /// </summary>
        public Guid ReviewId { get; set; }

        /// <summary>
        /// Gets or sets the review.
        /// </summary>
        public Review Review { get; set; }
    }
}
