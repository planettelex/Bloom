using System;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates a song credit with a role.
    /// </summary>
    public class SongCreditRole
    {
        /// <summary>
        /// Gets or sets the song credit identifier.
        /// </summary>
        public Guid SongCreditId { get; set; }

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
