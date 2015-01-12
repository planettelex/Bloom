using System;

namespace Bloom.Domain.Models
{
    public class SongCreditRole
    {
        public Guid SongCreditId { get; set; }

        public Guid RoleId { get; set; }

        public Role Role { get; set; }
    }
}
