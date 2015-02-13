using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates song with a review
    /// </summary>
    [Table(Name = "song_review")]
    public class SongReview
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song.
        /// </summary>
        public Song Song { get; set; }

        /// <summary>
        /// Gets or sets the song review identifier.
        /// </summary>
        [Column(Name = "review_id", IsPrimaryKey = true)]
        public Guid ReviewId { get; set; }

        /// <summary>
        /// Gets or sets the song review.
        /// </summary>
        public Review Review { get; set; }
    }
}
