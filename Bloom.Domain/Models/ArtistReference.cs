using System;

namespace Bloom.Domain.Models
{
    public class ArtistReference
    {
        public Guid ArtistId { get; set; }

        public Guid ReferenceId { get; set; }
    }
}
