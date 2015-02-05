using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a reference.
    /// </summary>
    public class ArtistReference
    {
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        public Guid ArtistId { get; set; }

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
