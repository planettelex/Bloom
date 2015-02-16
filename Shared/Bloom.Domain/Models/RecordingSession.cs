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
        /// Gets or sets the date a recording session occurred on.
        /// </summary>
        [Column(Name = "occurred_on")]
        public DateTime OccurredOn { get; set; }
    }
}
