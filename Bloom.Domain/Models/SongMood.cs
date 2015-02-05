using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a mood.
    /// </summary>
    public class SongMood
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
        /// Gets or sets the mood identifier.
        /// </summary>
        public Guid MoodId { get; set; }

        /// <summary>
        /// Gets or sets the mood.
        /// </summary>
        public Mood Mood { get; set; }
    }
}
