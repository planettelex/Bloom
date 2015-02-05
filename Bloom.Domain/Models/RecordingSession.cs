using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a recording session.
    /// </summary>
    public class RecordingSession
    {
        /// <summary>
        /// Gets or sets the recording session identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the date of a recording session.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
