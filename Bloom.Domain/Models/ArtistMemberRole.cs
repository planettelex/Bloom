using System;

namespace Bloom.Domain.Models
{
    public class ArtistMemberRole
    {
        public Guid ArtistMemberId { get; set; }

        public Guid RoleId { get; set; }

        public Role Role { get; set; }
    }
}
