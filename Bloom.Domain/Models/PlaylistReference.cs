using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a playlist with a reference.
    /// </summary>
    public class PlaylistReference
    {
        /// <summary>
        /// Gets or sets the playlist identifier.
        /// </summary>
        public Guid PlaylistId { get; set; }

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
