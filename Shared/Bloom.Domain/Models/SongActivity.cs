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
        /// Creates a new song activity instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="activity">The activity.</param>
        public static SongActivity Create(Song song, Activity activity)
        {
            return new SongActivity
            {
                SongId = song.Id,
                Song = song,
                ActivityId = activity.Id,
                Activity = activity
            };
        }

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
