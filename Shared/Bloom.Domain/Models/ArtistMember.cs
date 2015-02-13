using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a person.
    /// </summary>
    [Table(Name = "artist_member")]
    public class ArtistMember
    {
        /// <summary>
        /// Gets or sets the artist member identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "artist_id")]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        public Artist Artist { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        [Column(Name = "person_id")]
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the date this artist member started with this artist.
        /// </summary>
        [Column(Name = "started")]
        public DateTime Started { get; set; }

        /// <summary>
        /// Gets or sets the date this artist memeber ended with this artist.
        /// </summary>
        [Column(Name = "ended")]
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Gets or sets the artist member roles.
        /// </summary>
        public List<ArtistMemberRole> Roles { get; set; } 
    }
}
