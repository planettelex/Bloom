using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a tag.
    /// </summary>
    public class SongTag
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
        /// Gets or sets the tag identifier.
        /// </summary>
        public Guid TagId { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        public Tag Tag { get; set; }
    }
}
