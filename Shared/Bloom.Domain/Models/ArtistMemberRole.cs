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
        /// Creates a new album credit role instance.
        /// </summary>
        /// <param name="artistMember">The artist member.</param>
        /// <param name="role">The artist member role.</param>
        public static ArtistMemberRole Create(ArtistMember artistMember, Role role)
        {
            return new ArtistMemberRole
            {
                ArtistMemberId = artistMember.Id,
                RoleId = role.Id
            };
        }

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
    }
}
