using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a person.
    /// </summary>
    public class ArtistMember
    {
        /// <summary>
        /// Gets or sets the artist member identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the date this artist member started with this artist.
        /// </summary>
        public DateTime Started { get; set; }

        /// <summary>
        /// Gets or sets the date this artist memeber ended with this artist.
        /// </summary>
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Gets or sets the artist member roles.
        /// </summary>
        public List<ArtistMemberRole> Roles { get; set; } 
    }
}
