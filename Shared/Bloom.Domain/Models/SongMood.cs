using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an association between a song and a mood.
    /// </summary>
    [Table(Name = "song_mood")]
    public class SongMood
    {
        /// <summary>
        /// Creates a new song mood instance.
        /// </summary>
        /// <param name="song">A song.</param>
        /// <param name="mood">The mood.</param>
        public static SongMood Create(Song song, Mood mood)
        {
            return new SongMood
            {
                SongId = song.Id,
                MoodId = mood.Id
            };
        }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id", IsPrimaryKey = true)]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the mood identifier.
        /// </summary>
        [Column(Name = "mood_id", IsPrimaryKey = true)]
        public Guid MoodId { get; set; }
    }
}
