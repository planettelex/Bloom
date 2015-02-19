using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist with a reference.
    /// </summary>
    [Table(Name = "artist_reference")]
    public class ArtistReference
    {
        /// <summary>
        /// Creates a new artist reference instance.
        /// </summary>
        /// <param name="artist">The artist.</param>
        /// <param name="reference">The artist reference.</param>
        public static ArtistReference Create(Artist artist, Reference reference)
        {
            return new ArtistReference
            {
                ArtistId = artist.Id,
                ReferenceId = reference.Id,
                Reference = reference
            };
        }
        /// <summary>
        /// Gets or sets the artist identifier.
        /// </summary>
        [Column(Name = "artist_id", IsPrimaryKey = true)]
        public Guid ArtistId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        [Column(Name = "reference_id", IsPrimaryKey = true)]
        public Guid ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public Reference Reference { get; set; }
    }
}
