using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an activity with an album.
    /// </summary>
    public class AlbumActivity
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
        /// Gets or sets the activity identifier.
        /// </summary>
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        public Activity Activity { get; set; }
    }
}
