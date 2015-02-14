using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents a musical artist.
    /// </summary>
    [Table(Name = "artist")]
    public class Artist
    {
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "id", IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the artist name.
        /// </summary>
        [Column(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date this artist started their career.
        /// </summary>
        [Column(Name = "started")]
        public DateTime? Started { get; set; }

        /// <summary>
        /// Gets or sets the date this artist ended their career.
        /// </summary>
        [Column(Name = "ended")]
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Gets or sets the artist's bio.
        /// </summary>
        [Column(Name = "bio")]
        public string Bio { get; set; }

        /// <summary>
        /// Gets or sets the artist's Twitter handle.
        /// </summary>
        [Column(Name = "twitter")]
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets a whether this is a solo artist.
        /// </summary>
        [Column(Name = "is_solo")]
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
