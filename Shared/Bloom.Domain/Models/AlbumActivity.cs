using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an activity with an album.
    /// </summary>
    [Table(Name = "album_activity")]
    public class AlbumActivity
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
        /// Gets or sets the activity identifier.
        /// </summary>
        [Column(Name = "activity_id", IsPrimaryKey = true)]
        public Guid ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        public Activity Activity { get; set; }
    }
}
