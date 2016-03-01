using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a recording session.
    /// </summary>
    [Table(Name = "recording_session")]
    public class RecordingSession
    {
        /// <summary>
        /// Creates a new recording session instance.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="occurredOn">The date the session occurred on.</param>
        public static RecordingSession Create(Song song, DateTime occurredOn)
        {
            return new RecordingSession
            {
                Id = Guid.NewGuid(),
                SongId = song.Id,
                OccurredOn = occurredOn
            };
        }

        /// <summary>
        /// Gets or sets the recording session identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        [Column(Name = "song_id")]
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the recording session notes.
        /// </summary>
        [Column(Name = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the date a recording session occurred on.
        /// </summary>
        [Column(Name = "occurred_on")]
        public DateTime OccurredOn { get; set; }
    }
}
