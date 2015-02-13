using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with an activity.
    /// </summary>
    [Table(Name = "song_activity")]
    public class SongActivity
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
