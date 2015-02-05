using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a reference.
    /// </summary>
    public class SongReference
    {
        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        public Guid ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public Reference Reference { get; set; }
    }
}
