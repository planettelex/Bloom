using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a song and an activity.
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
                ActivityId = activity.Id
            };
        }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        [Column(Name = "activity_id", IsPrimaryKey = true)]
        public Guid ActivityId { get; set; }
    }
}
