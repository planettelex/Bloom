using System;
using Bloom.Domain.Enums;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a segment of a song.
    /// </summary>
    public class SongSegment
    {
        /// <summary>
        /// Gets or sets the song segment identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the song segment start time in miliseconds.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the song segment stop time in milliseconds.
        /// </summary>
        public int Stop { get; set; }

        /// <summary>
        /// Gets or sets the song segment musical key.
        /// </summary>
        public MusicalKeys Key { get; set; }

        /// <summary>
        /// Gets or sets the song segment time signature.
        /// </summary>
        public TimeSignature TimeSignature { get; set; }
    }
}