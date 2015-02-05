using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song with a person.
    /// </summary>
    public class SongCredit
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets whether this person has a "featured" credit.
        /// </summary>
        public bool IsFeatured { get; set; }

        /// <summary>
        /// Gets or sets the person's roles on the song.
        /// </summary>
        public List<SongCreditRole> Roles { get; set; } 
    }
}
