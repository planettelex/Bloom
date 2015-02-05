using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a musical artist.
    /// </summary>
    public class Artist
    {
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date this artist started their career.
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// Gets or sets the date this artist ended their career.
        /// </summary>
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Gets or sets the artist's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the artist's Twitter handle.
        /// </summary>
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets a whether this is a solo artist.
        /// </summary>
        public bool IsSolo { get; set; }

        /// <summary>
        /// Gets or sets the artist photos.
        /// </summary>
        public List<ArtistPhoto> Photos { get; set; }

        /// <summary>
        /// Gets or sets the artist members.
        /// </summary>
        public List<ArtistMember> Members { get; set; }

        /// <summary>
        /// Gets or sets the artist references.
        /// </summary>
        public List<ArtistReference> References { get; set; } 
    }
}
