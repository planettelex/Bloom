using System;
using System.Collections.Generic;

namespace Bloom.Domain.Models
{
    public class Artist
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Started { get; set; }

        public DateTime? Ended { get; set; }

        public string Bio { get; set; }

        public string Twitter { get; set; }

        public bool IsSolo { get; set; }

        public List<ArtistPhoto> Photos { get; set; } 

        public List<ArtistMember> Members { get; set; }

        public List<ArtistReference> References { get; set; } 
    }
}
