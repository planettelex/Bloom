using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist member with a role.
    /// </summary>
    public class ArtistMemberRole
    {
        /// <summary>
        /// Gets or sets the artist member identifier.
        /// </summary>
        public Guid ArtistMemberId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }
    }
}
