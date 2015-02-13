using System;
using System.Data.Linq.Mapping;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Associates an artist member with a role.
    /// </summary>
    [Table(Name = "artist_member_role")]
    public class ArtistMemberRole
    {
        /// <summary>
        /// Gets or sets the artist member identifier.
        /// </summary>
        [Column(Name = "artist_member_id", IsPrimaryKey = true)]
        public Guid ArtistMemberId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        [Column(Name = "role_id", IsPrimaryKey = true)]
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public Role Role { get; set; }
    }
}
