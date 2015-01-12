using System;

namespace Bloom.Domain.Models
{
    public class AlbumCreditRole
    {
        public Guid RoleId { get; set; }

        public Role Role { get; set; }

        public Guid AlbumCreditId { get; set; }
    }
}
