using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a role with an album credit.
    /// </summary>
    public class AlbumCreditRole
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Gets or sets the album credit identifier.
        /// </summary>
        public Guid AlbumCreditId { get; set; }
    }
}
