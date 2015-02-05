using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a reference.
    /// </summary>
    public class AlbumReference
    {
        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

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
