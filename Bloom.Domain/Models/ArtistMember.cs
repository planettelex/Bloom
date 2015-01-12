using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class ArtistMember
    {
        public Guid Id { get; set; }

        public Guid ArtistId { get; set; }

        public Guid PersonId { get; set; }

        public DateTime Started { get; set; }

        public DateTime? Ended { get; set; }

        public List<ArtistMemberRole> Roles { get; set; } 
    }
}
