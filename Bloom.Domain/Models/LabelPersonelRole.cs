using System;

namespace Bloom.Domain.Models
{
    public class LabelPersonelRole
    {
        public Guid LabelPersonelId { get; set; }

        public Guid RoleId { get; set; }

        public Role Role { get; set; }
    }
}
