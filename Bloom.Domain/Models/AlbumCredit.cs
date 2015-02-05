using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an album with a person.
    /// </summary>
    public class AlbumCredit
    {
        /// <summary>
        /// Gets or sets the album credit identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the album identifier.
        /// </summary>
        public Guid AlbumId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        public Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the roles this person had on the album.
        /// </summary>
        public List<AlbumCreditRole> Roles { get; set; } 
    }
}
