using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates song with a review
    /// </summary>
    public class SongReview
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the song review identifier.
        /// </summary>
        public Guid ReviewId { get; set; }

        /// <summary>
        /// Gets or sets the song review.
        /// </summary>
        public Review Review { get; set; }
    }
}
